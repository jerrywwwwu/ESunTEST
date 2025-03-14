using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VoteOnline.Models;
using System.Collections.Generic;

namespace VoteOnline.Controllers
{

    public class HomeController : Controller
    {
        private readonly VoteOnlineContext _context;
        private readonly RepositoryAdapter _homeRepository;
        //public HomeController(VoteOnlineContext context)
        //{
        //    _context = context;
        //}
        public HomeController(RepositoryAdapter homeRepository, VoteOnlineContext context)
        {
            _homeRepository = homeRepository;
            _context = context;
        }

        public ActionResult Index()
        {
            string message;
            var users = _homeRepository.GetUsersFromSP(out message);

            ViewBag.Users = users.UserNames;
            ViewBag.VoteCount = users.VoteItemCounts;
            ViewBag.Message = message;

            //var Users = _context.Users.ToList();
            //var VoteRecord = _context.VoteRecords.ToList();
            //var VoteItem = _context.VoteItems.ToList();
            //var votecount = _context.VoteItemCounts.ToList();
            //ViewBag.VoteItem = VoteItem;
            //ViewBag.User = Users;
            //ViewBag.VoteRecord = VoteRecord;
            return View(/*VoteRecord*/);
        }


    }
}