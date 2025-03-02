using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarhammerForum.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Location")]
        public string? Location { get; set; }

        [Display(Name = "Profile Picture")]
        public string? ImageFilename { get; set; }

        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile? ImageFile { get; set; }
    }
}
