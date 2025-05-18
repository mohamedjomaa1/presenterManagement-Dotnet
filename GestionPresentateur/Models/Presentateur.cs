using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestionPresentateur.Models;

namespace GestionPresentateur.Models
{
    public class Presentateur
    {
        [Key]
        public int CodeP { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string NomP { get; set; }

        [ForeignKey("Role")]
        public int CodeR { get; set; }
        public Role Role { get; set; }
    }
}