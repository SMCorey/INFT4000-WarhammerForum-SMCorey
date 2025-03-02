using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarhammerForum.Data;
using WarhammerForum.Models;
using Microsoft.AspNetCore.Identity;

namespace WarhammerForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly WarhammerForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // Constructor
        public HomeController(WarhammerForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // get all discussions with their authors
            var discussions = await _context.Discussion
                .Include(d => d.Comments)
                .Include(d => d.ApplicationUser)
                .OrderByDescending(d => d.CreateDate)
                .ToListAsync();
            return View(discussions); // pass discussions to list view
        }

        public async Task<IActionResult> GetDiscussion(int id)
        {
            var discussion = await _context.Discussion
                .Include(d => d.Comments)
                    .ThenInclude(c => c.ApplicationUser)
                .Include(d => d.ApplicationUser)
                .FirstOrDefaultAsync(d => d.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            // Sort the comments from oldest to newest as specified
            discussion.Comments = discussion.Comments?
                .OrderBy(c => c.CreateDate)
                .ToList();

            return View(discussion);
        }

        // Profile page
        public async Task<IActionResult> Profile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Get user's discussions
            var discussions = await _context.Discussion
                .Where(d => d.ApplicationUserId == id)
                .Include(d => d.Comments)
                .OrderByDescending(d => d.CreateDate)
                .ToListAsync();

            // Pass the user directly as the model
            ViewData["UserDiscussions"] = discussions;

            return View(user);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}