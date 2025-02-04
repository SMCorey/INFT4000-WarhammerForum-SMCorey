using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarhammerForum.Data;
using WarhammerForum.Models;

namespace WarhammerForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly WarhammerForumContext _context;

        // Constructor
        public HomeController(WarhammerForumContext content)
        {
            _context = content;
        }

        public async Task<IActionResult> Index()
        {
            // get all discussions
            var discussions = await _context.Discussion
                .Include(d => d.Comments)
                .OrderByDescending(d => d.CreateDate)
                .ToListAsync();
            return View(discussions); // pass discussions to list view
        }
        public async Task<IActionResult> GetDiscussion(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            discussion.Comments = discussion.Comments?
                .OrderByDescending(c => c.CreateDate)
                .ToList();

            return View(discussion);
        }

    }
}
