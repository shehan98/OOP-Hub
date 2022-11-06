using System.ComponentModel.DataAnnotations;

namespace EmpiteIMS.Models.DTO
{
    public class UserListModel
    {
        [Required]
        public string Id { get; set; } = null!;

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string? Role { get; set; } = null!;

        public bool UserStatus { get; set; }

    }
}
