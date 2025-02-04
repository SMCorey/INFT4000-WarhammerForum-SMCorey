using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WarhammerForum.Data;
using WarhammerForum.Models;

namespace WarhammerForum.Controllers
{
    public class CommentsController : Controller
    {
        private readonly WarhammerForumContext _context;

        public CommentsController(WarhammerForumContext context)
        {
            _context = context;
        }

        // GET: Comments

        // GET: Comments/Details/5

        // GET: Comments/Create
        public IActionResult Create(int discussionId)
        {
            var discussion = _context.Discussion.FirstOrDefault(d => d.DiscussionId == discussionId);
            ViewData["DiscussionId"] = discussionId;
            ViewData["DiscussionTitle"] = discussion?.Title ?? "Unknown Discussion";
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,DiscussionId")] Comment comment)
        {
            // initialize the datetime property
            comment.CreateDate = DateTime.Now;
            var tempID = comment.DiscussionId;

            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                // Redirect to the GetDiscussion action in HomeController with the discussion ID
                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
            }
            ViewData["DiscussionId"] = new SelectList(_context.Discussion, "DiscussionId", "DiscussionId", comment.DiscussionId);
            return View(comment);
        }

        // GET: Comments/Edit/5

        // POST: Comments/Edit/5

        // GET: Comments/Delete/5

        // POST: Comments/Delete/5

    }
}
