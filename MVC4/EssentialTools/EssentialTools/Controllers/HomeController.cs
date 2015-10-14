using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;
using  Ninject;



namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private Product[] products =
        {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
            new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };

        public ActionResult Index()
        {
            //WAS:   LinqValueCalculator calc = new LinqValueCalculator();
            
            /* C# требует от нас указать класс реализации для
интерфейса при создании экземпляра, что достаточно справедливо, ведь ему необходимо знать,
какой именно класс реализации мы хотим использовать. Это обозначает, что у нас до сих пор есть
проблема*/

            /*Мы хотим использовать Ninject, чтобы достичь того этапа, когда мы указываем, что мы хотим
создать экземпляр реализации интерфейса IValueCalculator, но детали о том, какая требуется
реализация, не являлись бы частью кода в контроллере Home*/



            /* Первый этап заключается в подготовке Ninject для использования. Нам нужно создать экземпляр
                Ninject ядра – это объект, который мы будем использовать для связи с Ninject и запросом реализаций
                интерфейса. Вот выражение из листинга, которое создает ядро:        */
            IKernel ninjectKernel = new StandardKernel();

            /* Нам нужно создать реализацию интерфейса Ninject.IKernel, что мы делаем, создав новый
                экземпляр класса StandardKernel. Это позволит нам выполнить второй этап, который заключается в
                создании связи между интерфейсами в нашем приложении и реализациями классов, с которыми мы
                хотим работать. Вот выражение из листинга, которое это делает:      */
            ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();

            /* Последний этап заключается в реальном использовании Ninject, 
                что мы делаем при помощи метода Get:*/
            IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();




            ShoppingCart cart = new ShoppingCart(calc) {Products = products};
            decimal totalValue = cart.CalculateProductTotal();
            return View(totalValue);
        }

    }
}
