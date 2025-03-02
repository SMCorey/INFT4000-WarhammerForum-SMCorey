using WarhammerForum.Data;

namespace WarhammerForum.Models
{
    public class Comment
    {
        // Primary key
        public int CommentId { get; set; }

        public string Content {  get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }
        
        // Foreign key (Discussion table)
        public int DiscussionId { get; set; }
        // Navigation Property
        public Discussion? Discussion { get; set; }

        //Foreign key(AspNetUsers table)
        public string ApplicationUserId { get; set; } = string.Empty;

        // Navigation Property
        public ApplicationUser? ApplicationUser { get; set; } // NULLABLE
    }
}
