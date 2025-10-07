using AuthenticationSystem.PolicyAuthorization;
using AuthSystem.Application.Constants;
using AuthSystem.Application.Implementations;
using AuthSystem.Application.Interfaces;
using AuthSystem.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;


namespace AuthenticationSystem.Extensions
{
    public static class ServiceExtention
    {
        public static void AddAPIServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(AuthSchemes.User, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["AppSettings:JwtAudience"],
                    ValidIssuer = configuration["AppSettings:JWTIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:JwtUserSecret"]))
                };
            })
            .AddJwtBearer(AuthSchemes.Admin, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["AppSettings:JwtAudience"],
                    ValidIssuer = configuration["AppSettings:JWTIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:JwtAdminSecret"]))
                };
            });


            services.AddControllers(configure: option =>
            {
                option.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                option.MaxModelValidationErrors = 1;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy(name: "CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicies.User, policy =>
                {
                    policy.AuthenticationSchemes.Add(AuthSchemes.User);
                    policy.RequireAuthenticatedUser();
                });

                options.AddPolicy(AuthPolicies.Admin, policy =>
                {
                    policy.AuthenticationSchemes.Add(AuthSchemes.Admin);
                    policy.RequireAuthenticatedUser();
                });
            });
            services.AddHttpContextAccessor();
            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.AddSingleton<IAuthorizationHandler, PolicyRequirementHandler>();
        }

        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtAuthenticator, JwtAuthenticator>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddAutoMapper(Assembly.Load("AuthSystem.Application"));
        }

        public static void RegisterDbContext(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("AuthSystem.Infrastructure");
                    sqlOptions.EnableRetryOnFailure(3);
                });
            });
        }
    }
}
