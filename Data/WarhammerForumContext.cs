using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WarhammerForum.Models;

namespace WarhammerForum.Data
{
    public class WarhammerForumContext : DbContext
    {
        public WarhammerForumContext (DbContextOptions<WarhammerForumContext> options)
            : base(options)
        {
        }

        public DbSet<WarhammerForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<WarhammerForum.Models.Comment> Comment { get; set; } = default!;
    }
}
