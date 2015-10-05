using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        
        //public string Index()
        //{
        //    return "Hello, World!";
        //}

        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning!" : "Good Afternoon!";
            return View();
        }

        //public ActionResult RsvpForm()
        //{
        //    return View();
        //}

        /*
                 * Метод, который отвечает на HTTP GET запросы: GET запрос является тем, с чем браузер
        имеет дело после каждого клика по ссылке. Этот вариант действий будет отвечать за
        отображение начальной пустой формы, когда кто-то первый раз посетит /Home/RsvpForm.

                 * Метод, который отвечает на HTTP POST запросы: По умолчанию, формы,
        обрабатываемые при помощи Html.BeginForm() , отправляются браузером как POST
        запросы. Этот вариант действий будет отвечать за получение отправленных данные и
        решать, что с ними делать.
         */

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                return View("Thanks", guestResponse);
            }
            else
            {
                //Validation Error
                return View();
            }
        }
    }
}
