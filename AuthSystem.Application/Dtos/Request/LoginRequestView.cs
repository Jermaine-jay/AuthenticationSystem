using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Application.Dtos.Request
{
    public class LoginRequestView
    {
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
