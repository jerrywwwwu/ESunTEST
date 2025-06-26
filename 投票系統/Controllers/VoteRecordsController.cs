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
        private readonly IRepositoryAdapter _homeRepository;
        public VoteRecordsController(IRepositoryAdapter homeRepository, VoteOnlineContext context)
        {
            _homeRepository = homeRepository;
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
            return RedirectToAction ("Index","Home");
        }

        // POST: VoteRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(string UserName, List<int> VoteItemId)
        //{
        //    if (string.IsNullOrEmpty(UserName) || VoteItemId == null || !VoteItemId.Any())
        //    {
        //        ModelState.AddModelError("", "請選擇使用者與至少一個投票項目！");
        //        return View();
        //    }
        //    string message;
        //    _homeRepository.SubmitVote(UserName, VoteItemId, out message);
        //    TempData["Message"] = message; // 使用 TempData 來存儲訊息


        //    //    foreach (var itemId in VoteItemId)
        //    //{
        //    //    var voteRecord = new VoteRecord
        //    //    {
        //    //        UserName = UserName,
        //    //        VoteItemId = itemId
        //    //    };
        //    //    _context.VoteRecords.Add(voteRecord);
        //    //}

        //    //await _context.SaveChangesAsync();
        //    return RedirectToAction("Index", "Home");
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(string UserName, List<VoteItemDto> VoteItems)
		{
            if (string.IsNullOrEmpty(UserName) || VoteItems == null || !VoteItems.Any())
            {
                return Json(new { success = false, message = "請選擇使用者與至少一個投票項目！" });
            }


			string message = await _homeRepository.SubmitVote(UserName, VoteItems);
            

			var VotePageData = await _homeRepository.GetUsersFromSP();
			string message2 = VotePageData.Message;

            if (!string.IsNullOrEmpty(message2))
            {
                message = message2;
			}
            var updatedVoteCount = VotePageData.VoteItemCounts.Select(v => new
            {
                v.VoteItemId,
                ItemName = v.ItemName,
                VoteCount = v.VoteCount
            }).ToList();

            return Json(new { success = true, message, updatedVoteCount });
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
        //[ValidateAntiForgeryToken]
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
