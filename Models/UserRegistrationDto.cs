using System.ComponentModel.DataAnnotations;
namespace Rollcall.Models
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "User Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "User Password is required")]
        public string Password { get; set; }
    }
}