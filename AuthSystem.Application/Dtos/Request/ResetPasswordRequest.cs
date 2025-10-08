using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Application.Dtos.Request
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Compare(nameof(NewPassword))]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
