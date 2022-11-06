using System.ComponentModel.DataAnnotations;

namespace EmpiteIMS.Models.DTO
{
    public class UserCreationModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Minimum length 6 and must contain  1 Uppercase,1 lowercase, 1 special character and 1 digit")]
        public string Password { get; set; }

        [Required]
        public string ? Role { get; set; }

        [Required]
        public bool UserStatus { get; set; }
    }
}
