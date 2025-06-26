using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VoteOnline.Models;
using System.Collections.Generic;

namespace VoteOnline.Controllers
{

    public class HomeController : Controller
    {
        private readonly VoteOnlineContext _context;
        private readonly IRepositoryAdapter _homeRepository;
        private readonly ITEST _test;//介面注入測試
        private readonly TEST2 _test2;//class注入測試
        //public HomeController(VoteOnlineContext context)
        //{
        //    _context = context;
        //}
        public HomeController(IRepositoryAdapter homeRepository, VoteOnlineContext context, ITEST test/*介面注入測試*/, TEST2 test2/*class注入測試*/)

		{
            _homeRepository = homeRepository;
            _context = context;
			_test = test;//介面注入測試
            _test2 = test2;//class注入測試

		}

        public ActionResult Index()
        {
			//string message;
            var users = _homeRepository.GetUsersFromSP();

            ViewBag.Users = users.Result.UserNames;
            ViewBag.VoteCount = users.Result.VoteItemCounts;
            ViewBag.Message = users.Result.Message;

            //var Users = _context.Users.ToList();
            //var VoteRecord = _context.VoteRecords.ToList();
            //var VoteItem = _context.VoteItems.ToList();
            //var votecount = _context.VoteItemCounts.ToList();
            //ViewBag.VoteItem = VoteItem;
            //ViewBag.User = Users;
            //ViewBag.VoteRecord = VoteRecord;
            return View(/*VoteRecord*/);
        }


		/*若class TEST跟TEST2改變，
		 * 非注入:private readonly、註冊以及相關程式全部跳紅字(高耦合)
		 * class注入:private readonly跟註冊跳紅字(低耦合)
		 * interface注入:只有註冊頁跳紅字(耦合最低)*/
		public void test1()
        {
            _test.TESTFun(); //interface注入測試

            _test2.TESTFun2();//class注入測試

            TEST test = new();//非注入
            test.TESTFun();

            TEST2 test2 = new();//非注入
			test2.TESTFun2();
        }

		public void test2()
		{
			_test.TESTFun(); //interface注入測試
			_test2.TESTFun2();//class注入測試

            TEST test = new();//非注入
            test.TESTFun();

            TEST2 test2 = new();//非注入
			test2.TESTFun2();
		}

		public void test3()
		{
			_test.TESTFun();    //interface注入測試
			_test2.TESTFun2();  //class注入測試

            TEST test = new();  //非注入
            test.TESTFun();   

			TEST2 test2 = new();//非注入
			test2.TESTFun2();
		}


	}
}