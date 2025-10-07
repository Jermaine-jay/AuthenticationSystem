using AuthSystem.Application.Common.Exceptions;
using AuthSystem.Application.Constants;
using AuthSystem.Application.Dtos.Request;
using AuthSystem.Application.Helpers;
using AuthSystem.Application.Interfaces;
using AuthSystem.Domain.Entities;
using AuthSystem.Domain.Enum;
using AutoMapper;
using Kwlc.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace AuthSystem.Application.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly AppSettings _appSettings;
        public readonly IMapper _mapper;
        public readonly IJwtAuthenticator _jwtAuthenticator;
        public IGenericQueryRepository<User> _userQueryRepository;
        public IGenericQueryRepository<Role> _roleQueryRepository;
        public IGenericCommandRepository<User> _userCommandRepository;
        public IGenericQueryRepository<UserInRole> _userInRoleQueryRepository;
        public IGenericQueryRepository<Permission> _permissionQueryRepository;
        public IGenericCommandRepository<UserInRole> _userInRoleCommandRepository;
        public IGenericQueryRepository<UserPermission> _userPermissionQueryRepository;
        public IGenericQueryRepository<RolePermission> _rolePermissionQueryRepository;
        public IGenericCommandRepository<UserPermission> _userPermissionCommandRepository;
        public AuthenticationService(
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IJwtAuthenticator jwtAuthenticator,
            IGenericQueryRepository<User> userQueryRepository,
            IGenericQueryRepository<Role> roleQueryRepository,
            IGenericCommandRepository<User> userCommandRepository,
            IGenericQueryRepository<UserInRole> userInRoleQueryRepository,
            IGenericQueryRepository<Permission> permissionQueryRepository,
            IGenericCommandRepository<UserInRole> userInRoleCommandRepository,
            IGenericQueryRepository<RolePermission> rolePermissionQueryRepository,
            IGenericQueryRepository<UserPermission> userPermissionQueryRepository,
            IGenericCommandRepository<UserPermission> userPermissionCommandRepository)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _jwtAuthenticator = jwtAuthenticator;
            _roleQueryRepository = roleQueryRepository;
            _userQueryRepository = userQueryRepository;
            _userCommandRepository = userCommandRepository;
            _userInRoleQueryRepository = userInRoleQueryRepository;
            _permissionQueryRepository = permissionQueryRepository;
            _userInRoleCommandRepository = userInRoleCommandRepository;
            _rolePermissionQueryRepository = rolePermissionQueryRepository;
            _userPermissionQueryRepository = userPermissionQueryRepository;
            _userPermissionCommandRepository = userPermissionCommandRepository;
        }

        public async Task<UserViewModel> GetUser(string UserId)
        {
            var user = await _userQueryRepository.GetSingleAsync(x => x.Id == UserId);
            if (user == null)
                throw new NotFoundException("user does not exist");

            return _mapper.Map<UserViewModel>(user);
        }
        public async Task BanUser(BanUserViewModel model)
        {
            var user = await _userQueryRepository.GetSingleAsync(x => x.Id == model.UserId);
            if (user == null)
                throw new NotFoundException("user does not exist");

            if (!model.Action && !user.IsBanned)
                throw new NotFoundException("user is banned");

            if (model.Action && user.IsBanned)
                throw new NotFoundException("user is banned");

            user.IsBanned = model.Action;
            await _userCommandRepository.UpdateAsync(user);
        }
        public async Task<JwtToken> Login(LoginRequestView login)
        {
            var user = (await _userQueryRepository.GetAllAsync(x => x.Email.ToLower() == login.Username.ToLower())).FirstOrDefault();
            if (user == null)
                throw new NotFoundException("Invalid Username or password");

            if (user.IsBanned || (user.LoginFailedCount > _appSettings.LoginFailedAttempt && DateTime.UtcNow < user.LastModified.AddMinutes(_appSettings.LockOutTime)))
                throw new NotFoundException("user is banned");

            var hashedPassword = HashingUtility.HashPassword(login.Password, _appSettings, user.Salt);

            if (hashedPassword.HashedValue != user.Password)
            {
                user.LoginFailedCount++;
                if (user.LoginFailedCount > _appSettings.LoginFailedAttempt)
                {
                    user.LastModified = DateTime.UtcNow;
                }
                await _userCommandRepository.UpdateAsync(user);
            }

            user.LoginFailedCount = 0;
            var roleIds = (await _userInRoleQueryRepository.GetAllAsync(x => x.UserId == user.Id)).Select(x => x.RoleId).ToList();
            var rolePermissionIds = (await _rolePermissionQueryRepository.GetAllAsync(x => roleIds.Contains(x.RoleId))).Select(x => x.PermissionId);
            var roles = (await _roleQueryRepository.GetAllAsync(x => roleIds.Contains(x.Id))).Select(x => x.Name);
            var permission = (await _permissionQueryRepository.GetAllAsync(x => rolePermissionIds.Contains(x.Id))).Select(x => x.Name);

            var generateOTP = await _jwtAuthenticator.GenerateJwtToken(user, roles.ToArray(), permission.ToArray());
            await _userCommandRepository.UpdateAsync(user);
            generateOTP.Expires = generateOTP.Expires;
            generateOTP.UserId = user.Id;
            return generateOTP;
        }
        public async Task<JwtToken> Register(RegistrationRequest register)
        {
            register.UserTypeId = register.UserTypeId == null ? UserType.User : register.UserTypeId;

            var userCheck = await _userQueryRepository.AnyAsync(x => x.Email.ToLower() == register.Email.ToLower());
            if (userCheck)
                throw new NotFoundException("user already exist");

            var hashdetails = HashingUtility.HashPassword(register.Password, _appSettings);
            var user = _mapper.Map<User>(register);
            user.Password = hashdetails.HashedValue;
            user.Salt = hashdetails.Salt;
            user.DateCreated = DateTime.UtcNow;

            var userRecord = await _userCommandRepository.InsertAndRetrieveIdAsync(user);

            var userRole = new UserInRole
            {
                UserId = userRecord.Id,
                DateCreated = DateTime.UtcNow,
                IsDeleted = false,
            };

            var role = await _roleQueryRepository.GetSingleAsync(x => x.Name == register.UserTypeId.Description());
            if (role == null)
            {
                throw new NotFoundException("Role does not exist");
            }

            userRole.RoleId = role.Id;

            var userInRole = await _userInRoleCommandRepository.InsertAndRetrieveIdAsync(userRole);
            var roleIds = (await _userInRoleQueryRepository.GetAllAsync(x => x.UserId == user.Id)).Select(x => x.RoleId).ToList();

            var rolePermissionIds = (await _rolePermissionQueryRepository.GetAllAsync(x => roleIds.Contains(x.RoleId))).Select(x => x.PermissionId);
            var roles = (await _roleQueryRepository.GetAllAsync(x => roleIds.Contains(x.Id))).Select(x => x.Name);

            var permission = (await _permissionQueryRepository.GetAllAsync(x => rolePermissionIds.Contains(x.Id))).Select(x => x.Name);
            var generateOTP = await _jwtAuthenticator.GenerateJwtToken(user, roles.ToArray(), permission.ToArray());

            return new JwtToken
            {
                UserId = userRecord.Id,
                Token = generateOTP.Token,
                Email = generateOTP.Email,
                Issued = generateOTP.Issued,
                Expires = generateOTP.Expires,
                Permissions = generateOTP.Permissions,
            };
        }
        public async Task UpdateUser(UpdateUserRequest request)
        {
            var userCheck = await _userQueryRepository.GetSingleAsync(x => x.Id == request.Id);
            if (userCheck == null)
                throw new NotFoundException("user does not exist");

            using var transaction = await _userCommandRepository.BeginTransactionAsync();
            try
            {
                _mapper.Map(request, userCheck);
                userCheck.LastModified = DateTime.UtcNow;
                await _userCommandRepository.UpdateAsync(userCheck);

                var userRole = await _userInRoleQueryRepository.GetSingleAsync(x => x.UserId == userCheck.Id);
                var roleId = await _roleQueryRepository.GetSingleAsync(x => x.Name == request.UserTypeId.Description());
                if (roleId == null)
                    throw new NotFoundException("Role does not exist");

                if (userRole != null)
                {
                    userRole.RoleId = roleId.Id;
                    await _userInRoleCommandRepository.UpdateAsync(userRole);
                }
                else
                {
                    var newUserRole = new UserInRole
                    {
                        RoleId = roleId.Id,
                        IsDeleted = false,
                        UserId = userCheck.Id,
                        DateCreated = DateTime.UtcNow,
                    };
                    await _userInRoleCommandRepository.InsertAsync(newUserRole);
                }
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("Filed to update user");
            }
        }
        public async Task<string> ForgotPassword(ForgotPasswordRequest request)
        {
            var existingUser = await _userQueryRepository.GetSingleAsync(x => x.Email == request.Email);
            if (existingUser == null)
                throw new NotFoundException("user does not exist");

            if (existingUser.IsBanned)
                throw new NotFoundException("user is banned");

            //await _emailService.ResetPasswordMail(_mapper.Map<UserViewModel>(existingUser));

            return existingUser.Email;
        }
        public async Task<string> ResetPassword(ResetPasswordRequest request)
        {
            var userId = GeneralUtility.DecodeUniqueToken(request.Token);
            var user = await _userQueryRepository.GetSingleAsync(x => x.Id.ToString() == userId);
            if (user == null)
                throw new NotFoundException("user does not exist");

            if (user.IsBanned)
                throw new NotFoundException("user is banned");

            var hashdetails = HashingUtility.HashPassword(request.NewPassword, _appSettings);
            user.Salt = hashdetails.Salt;
            user.Password = hashdetails.HashedValue;
            await _userCommandRepository.UpdateAsync(user);

            return "Successful";
        }
        public async Task<string> ChangePassword(ChangePasswordRequest request)
        {
            var user = await _userQueryRepository.GetSingleAsync(x => x.Id == request.UserId);
            if (user == null)
                throw new NotFoundException("user does not exist");

            var hashdetails = HashingUtility.HashPassword(request.NewPassword, _appSettings);
            user.Salt = hashdetails.Salt;
            user.Password = hashdetails.HashedValue;
            await _userCommandRepository.UpdateAsync(user);

            return user.Email;
        }
    }
}
