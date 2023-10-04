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
    public class VoteRecordsController : Controller
    {
        private readonly VoteOnlineContext _context;

        public VoteRecordsController(VoteOnlineContext context)
        {
            _context = context;
        }

        // GET: VoteRecords
        public async Task<IActionResult> Index()
        {
            var voteOnlineContext = _context.VoteRecords.Include(v => v.VoteItem);
            return View(await voteOnlineContext.ToListAsync());
        }

        // GET: VoteRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VoteRecords == null)
            {
                return NotFound();
            }

            var voteRecord = await _context.VoteRecords
                .Include(v => v.VoteItem)
                .FirstOrDefaultAsync(m => m.VoteId == id);
            if (voteRecord == null)
            {
                return NotFound();
            }

            return View(voteRecord);
        }

        // GET: VoteRecords/Create
        public IActionResult Create()
        {
            ViewData["VoteItemId"] = new SelectList(_context.VoteItems, "VoteItemId", "ItemName");
            return View();
        }

        // POST: VoteRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoteId,UserName,VoteItemId")] VoteRecord voteRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voteRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VoteItemId"] = new SelectList(_context.VoteItems, "VoteItemId", "ItemName", voteRecord.VoteItemId);
            return View(voteRecord);
        }

        // GET: VoteRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VoteRecords == null)
            {
                return NotFound();
            }

            var voteRecord = await _context.VoteRecords.FindAsync(id);
            if (voteRecord == null)
            {
                return NotFound();
            }
            ViewData["VoteItemId"] = new SelectList(_context.VoteItems, "VoteItemId", "ItemName", voteRecord.VoteItemId);
            return View(voteRecord);
        }

        // POST: VoteRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoteId,UserName,VoteItemId")] VoteRecord voteRecord)
        {
            if (id != voteRecord.VoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voteRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteRecordExists(voteRecord.VoteId))
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
            ViewData["VoteItemId"] = new SelectList(_context.VoteItems, "VoteItemId", "ItemName", voteRecord.VoteItemId);
            return View(voteRecord);
        }

        // GET: VoteRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VoteRecords == null)
            {
                return NotFound();
            }

            var voteRecord = await _context.VoteRecords
                .Include(v => v.VoteItem)
                .FirstOrDefaultAsync(m => m.VoteId == id);
            if (voteRecord == null)
            {
                return NotFound();
            }

            return View(voteRecord);
        }

        // POST: VoteRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VoteRecords == null)
            {
                return Problem("Entity set 'VoteOnlineContext.VoteRecords'  is null.");
            }
            var voteRecord = await _context.VoteRecords.FindAsync(id);
            if (voteRecord != null)
            {
                _context.VoteRecords.Remove(voteRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoteRecordExists(int id)
        {
          return (_context.VoteRecords?.Any(e => e.VoteId == id)).GetValueOrDefault();
        }
    }
}
