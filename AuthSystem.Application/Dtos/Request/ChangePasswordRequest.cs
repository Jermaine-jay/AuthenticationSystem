using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Application.Dtos.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public string UserId { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password), Compare(nameof(CurrentPassword))]
        [Required]
        public string? NewPassword { get; set; }
    }

}
