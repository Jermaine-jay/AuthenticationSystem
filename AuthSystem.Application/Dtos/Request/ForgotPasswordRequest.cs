using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Application.Dtos.Request
{
    public class ForgotPasswordRequest
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string? Email { get; set; }
    }
}
