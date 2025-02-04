using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarhammerForum.Models
{
    public class Discussion
    {
        public int DiscussionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageFilename { get; set; } = string.Empty ;
        public DateTime CreateDate { get; set; }

        // Navigation
        public List<Comment>? Comments { get; set; }

        // Property for file upload, not mapped in EF
        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; } // nullable!!!
    }
}
