using System.ComponentModel.DataAnnotations;

namespace GestionPresentateur.Models
{
    public class Compte
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Login is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Login must be between 3 and 50 characters")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [RegularExpression("Admin|User", ErrorMessage = "Role must be Admin or User")]
        public string Role { get; set; }
    }
}