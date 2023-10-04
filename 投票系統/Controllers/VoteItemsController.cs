using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VoteOnline.Models;

namespace VoteOnline.Controllers
{
    public class VoteItemsController : Controller
    {
        private readonly VoteOnlineContext _context;

        public VoteItemsController(VoteOnlineContext context)
        {
            _context = context;
        }

        // GET: VoteItems
        public async Task<IActionResult> Index()
        {
              return _context.VoteItems != null ? 
                          View(await _context.VoteItems.ToListAsync()) :
                          Problem("Entity set 'VoteOnlineContext.VoteItems'  is null.");
        }

        // GET: VoteItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.VoteItems == null)
            {
                return NotFound();
            }

            var voteItem = await _context.VoteItems
                .FirstOrDefaultAsync(m => m.VoteItemId == id);
            if (voteItem == null)
            {
                return NotFound();
            }

            return View(voteItem);
        }

        // GET: VoteItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VoteItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoteItemId,ItemName")] VoteItem voteItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voteItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(voteItem);
        }

        // GET: VoteItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.VoteItems == null)
            {
                return NotFound();
            }

            var voteItem = await _context.VoteItems.FindAsync(id);
            if (voteItem == null)
            {
                return NotFound();
            }
            return View(voteItem);
        }

        // POST: VoteItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VoteItemId,ItemName")] VoteItem voteItem)
        {
            if (id != voteItem.VoteItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voteItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteItemExists(voteItem.VoteItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(voteItem);
        }

        // GET: VoteItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.VoteItems == null)
            {
                return NotFound();
            }

            var voteItem = await _context.VoteItems
                .FirstOrDefaultAsync(m => m.VoteItemId == id);
            if (voteItem == null)
            {
                return NotFound();
            }

            return View(voteItem);
        }

        // POST: VoteItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.VoteItems == null)
            {
                return Problem("Entity set 'VoteOnlineContext.VoteItems'  is null.");
            }
            var voteItem = await _context.VoteItems.FindAsync(id);
            if (voteItem != null)
            {
                _context.VoteItems.Remove(voteItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoteItemExists(string id)
        {
          return (_context.VoteItems?.Any(e => e.VoteItemId == id)).GetValueOrDefault();
        }
    }
}
