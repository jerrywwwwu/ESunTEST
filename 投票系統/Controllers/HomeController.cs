using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VoteOnline.Models;
using System.Collections.Generic;

namespace VoteOnline.Controllers {

        public class HomeController : Controller 
        {
        private readonly VoteOnlineContext _context;
        public HomeController(VoteOnlineContext context) {
            _context = context;
        }

        public ActionResult Index () {
            var Users = _context.Users.ToList();
            var VoteRecord = _context.VoteRecords.ToList();
            var VoteItem = _context.VoteItems.ToList();
            var votecount = _context.VoteItemCounts.ToList();
            ViewBag.User = Users;
            ViewBag.VoteCount = votecount;
            ViewBag.voteitem = VoteItem;
            ViewBag.VoteRecord = VoteRecord;
                return View (VoteRecord);
            }

            
        }
    }