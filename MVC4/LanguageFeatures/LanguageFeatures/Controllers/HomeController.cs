using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFeatures.Models;
using System.Text;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/


        public string Index()
        {
            return "Navigate to a URL to show an example";
        }

        //Методы действия
        //http://localhost:51097/Home/AutoProperty
        public ViewResult AutoProperty()
        {
            // создается новый объект Product
            Product myProduct = new Product();
            // устанавливается значение свойства
            myProduct.Name = "Kayak";
            // читается свойство
            string productName = myProduct.Name;
            // генерируется представление
            return View("Result",
                (object) String.Format("Product name: {0}", productName));
            /*Вы заметили, что в листинге 4-4 мы привели второй аргумент для метода View к
типу object. Это потому что метод View перегружен, то есть принимает два
аргумента String, и это имеет другое значение, чем принимать String и object.
Чтобы избежать не того вызова, мы явно привели второй аргумент. Мы вернемся к
методу View и всем его перегрузкам в главе 18.*/

            /*Вы можете увидеть результат выполнения этого примера, запустив проект и перейдя к
/Home/AutoProperty*/

            /*Мы хотим гибкости свойств, но на данный момент нам не нужны пользовательские выражения
получения и установки. Решением является автоматически реализуемое свойство, также
известное как автоматическое свойство. */
        }

        public ViewResult CreateProductA()
        {
            // создание нового объекта Product
            Product myProduct = new Product();
            // установка значений свойств
            myProduct.ProductId = 100;
            myProduct.Name = "Kayak";
            myProduct.Description = "A boat for one person";
            myProduct.Price = 275M;
            myProduct.Category = "Watersports";
            return View("Result",
                (object) String.Format("Category: {0}", myProduct.Category));
        }

        //Использование функции инициализации объекта
        public ViewResult CreateProductB()
        {
            // создание и заполнение нового объекта Product
            Product myProduct = new Product
            {
                ProductId = 100,
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 275M,
                Category = "Watersports"
            };
            return View("Result", (object) String.Format("Category: {0}", myProduct.Category));
        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = {"apple", "orange", "plum"};
            List<int> intList = new List<int> {10, 20, 30, 40};
            Dictionary<string, int> myDict = new Dictionary<string, int>
            {
                {"apple", 10},
                {"orange", 20},
                {"plum", 30}
            };
            return View("Result", (object) stringArray[1]);
        }

        public ViewResult UseExtension()
        {
            // создание и заполнение ShoppingCart
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Price = 275M},
                    new Product {Name = "Lifejacket", Price = 48.95M},
                    new Product {Name = "Soccer ball", Price = 19.50M},
                    new Product {Name = "Corner flag", Price = 34.95M}
                }
            };
            // получение общей стоимости продуктов в корзине
            decimal cartTotal = cart.TotalPrices(); // Extension method
            return View("Result",
                (object) String.Format("Total: {0:c}", cartTotal));


            /*Как вы видите, мы называем метод TotalPrices для объекта ShoppingCart, как будто это часть
класса ShoppingCart, даже если это расширенный метод, определенный другим классом. .NET
найдет расширенные методы, если они находятся в рамках текущего класса, что обозначает, что
они являются частью одного и того же пространства имен или находятся в пространстве имен,
которое является предметом выражения using. */
        }




        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Price = 275M},
                    new Product {Name = "Lifejacket", Price = 48.95M},
                    new Product {Name = "Soccer ball", Price = 19.50M},
                    new Product {Name = "Corner flag", Price = 34.95M}
                }
            };
            // создание и заполнение массива объектов Product
            Product[] productArray =
            {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };
            // получение общей стоимости продуктов в корзине
            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = productArray.TotalPrices();
            return View("Result",
                (object) String.Format("Cart Total: {0}, Array Total: {1}",
                    cartTotal, arrayTotal));
        }


        public ViewResult UseFilterExtensionMethodA()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                    new Product
                    {
                        Name = "Lifejacket",
                        Category = "Watersports",
                        Price = 48.95M
                    },
                    new Product
                    {
                        Name = "Soccer ball",
                        Category = "Soccer",
                        Price = 19.50M
                    },
                    new Product
                    {
                        Name = "Corner flag",
                        Category = "Soccer",
                        Price = 34.95M
                    }
                }
            };
            decimal total = products.FilterByCategory("Soccer").Sum(prod => prod.Price);
            //foreach (Product prod in products.FilterByCategory("Soccer"))
            //{
            //    total += prod.Price;
            //}
            return View("Result", (object) String.Format("Total: {0}", total));
        }

        //http://localhost:51097/Home/UseFilterExtensionMethodB
        public ViewResult UseFilterExtensionMethodB()
        {
            // создаем и заполняем ShoppingCart
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                    new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                    new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                    new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                }
            };

            //--- Delegate
            /*
            Func<Product, bool> categoryFilter = delegate(Product prod)
            {
                return prod.Category == "Soccer";
            };
            */
            //--- Lambda
            /*
             * Символы => произносятся как "переходит" и связывают
параметр с результатом лямбда-выражения. В нашем примере параметр объекта Product,
названный prod, переходит к результату bool, который будет верным, если prod параметр
Category будет равен Soccer. 
             */
            //Func<Product, bool> categoryFilter = prod => prod.Category == "Soccer";

            /*
            decimal total = 0;
            foreach (Product prod in products.Filter(categoryFilter))
            {
                total += prod.Price;
            }
            */
            //decimal total = products.Filter(categoryFilter).Sum(prod => prod.Price);
            //decimal total = products.Filter(categoryFilter).Sum(prod => prod.Price);
            decimal total = products.Filter(prod => prod.Category == "Soccer").Sum(prod => prod.Price);

            return View("Result", (object) String.Format("Total: {0}", total));
        }

        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new {Name = "MVC", Category = "Pattern"},
                new {Name = "Hat", Category = "Clothing"},
                new {Name = "Apple", Category = "Fruit"}
            };
            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }
            return View("Result", (object) result.ToString());
        }



        public ViewResult FindProductsNoLinq()
        {
            Product[] products =
            {
                new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
            };
            // определяем массив для результатов
            Product[] foundProducts = new Product[3];
            // сортируем содержание массива
            Array.Sort(products, (item1, item2) =>
            {
                return Comparer<decimal>.Default.Compare(item1.Price, item2.Price);
            });
            // получаем три первых элемента массива в качестве результата
            Array.Copy(products, foundProducts, 3);
            // создаем результат
            StringBuilder result = new StringBuilder();
            foreach (Product p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
            }
            return View("Result", (object) result.ToString());
        }

        public ViewResult FindProductsLinq()
        {
            Product[] products =
            {
                new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
            };
            var foundProducts = from match in products
                orderby match.Price descending
                select new
                {
                    match.Name,
                    match.Price
                };
            // создаем результат
            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }
            return View("Result", (object) result.ToString());
        }


        public ViewResult FindProductsLinqDotNotation()
        {
            Product[] products =
            {
                new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
            };
            var foundProducts = products.OrderByDescending(e => e.Price)
                .Take(3)
                .Select(e => new
                {
                    e.Name,
                    e.Price
                });

            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
            }
            return View("Result", (object) result.ToString());
        }

        /*Выборка, которая содержит только отложенные методы, 
         * не будет выполняться до тех пор, пока не будут перечислены элементы
         * результата
         */
        public ViewResult FindProductsNonDeferred()
        {
            Product[] products =
            {
                new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
            };
            var foundProducts = products.OrderByDescending(e => e.Price)
                .Take(3)
                .Select(e => new
                {
                    e.Name,
                    e.Price
                });
            products[2] = new Product {Name = "Stadium", Price = 79600M};
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
            }
            return View("Result", (object) result.ToString());
        }




    }
}


