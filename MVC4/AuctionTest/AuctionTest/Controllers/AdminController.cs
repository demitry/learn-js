using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuctionTest.Models; // Я не думаю, что это лучшее решение.
using System.Diagnostics;

namespace AuctionTest.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        IMembersRepository membersRepository;
        public AdminController(IMembersRepository repositoryParam)
        {
            membersRepository = repositoryParam;
        }
        public ActionResult ChangeLoginName(string oldLoginParam, string newLoginParam)
        {
            Member member = membersRepository.FetchByLoginName(oldLoginParam);
            member.LoginName = newLoginParam;
            membersRepository.SubmitChanges();
            // ... теперь будет показано представление
            return View();
        }
    }
}
