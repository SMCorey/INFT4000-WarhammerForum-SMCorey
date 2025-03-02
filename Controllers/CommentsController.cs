using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WarhammerForum.Data;
using WarhammerForum.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WarhammerForum.Controllers
{
    [Authorize] // Require authentication for all actions
    public class CommentsController : Controller
    {
        private readonly WarhammerForumContext _context;

        public CommentsController(WarhammerForumContext context)
        {
            _context = context;
        }

        // GET: Comments/Create
        public IActionResult Create(int discussionId)
        {
            var discussion = _context.Discussion.FirstOrDefault(d => d.DiscussionId == discussionId);
            ViewData["DiscussionId"] = discussionId;
            ViewData["DiscussionTitle"] = discussion?.Title ?? "Unknown Discussion";
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,DiscussionId")] Comment comment)
        {
            // Set current date and user ID
            comment.CreateDate = DateTime.Now;
            comment.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
            }

            ViewData["DiscussionId"] = new SelectList(_context.Discussion, "DiscussionId", "DiscussionId", comment.DiscussionId);
            return View(comment);
        }
    }
}