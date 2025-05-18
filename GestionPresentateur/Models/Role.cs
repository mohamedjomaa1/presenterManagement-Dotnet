using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using GestionPresentateur.Models;

namespace GestionPresentateur.Models
{
    public class Role
    {
        [Key]
        public int CodeR { get; set; }

        [Required(ErrorMessage = "Label is required")]
        [StringLength(50, ErrorMessage = "Label cannot exceed 50 characters")]
        public string Libelle { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative")]
        public decimal Prix { get; set; }

        public List<Presentateur> Presentateurs { get; set; }
    }
}