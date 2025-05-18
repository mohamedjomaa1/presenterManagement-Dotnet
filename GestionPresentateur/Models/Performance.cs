using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestionPresentateur.Models;
using Microsoft.AspNetCore.Identity;

namespace GestionPresentateur.Models
{
    public class Performance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CodeN { get; set; }
        public Numero Numero { get; set; }

        [Required]
        public string CompteId { get; set; } // Matches IdentityUser.Id (string)
        public IdentityUser Compte { get; set; }

        [Required]
        public DateTime PerformanceDate { get; set; }
    }
}