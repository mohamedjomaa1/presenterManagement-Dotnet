using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestionPresentateur.Models;

namespace GestionPresentateur.Models
{
    public class Numero
    {
        [Key]
        public int CodeN { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be positive")]
        public int Duree { get; set; }

        [ForeignKey("Presentateur")]
        public int CodeP { get; set; }
        public Presentateur Presentateur { get; set; }
    }
}