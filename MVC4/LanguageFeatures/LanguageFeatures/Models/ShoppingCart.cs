using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    /*Предположим, что нам нужна возможность
определять общую стоимость объектов Product в классе ShoppingCart, но мы не можем изменить
сам класс, например, потому что он получен от третьей стороны и у нас нет исходного кода. К
счастью, мы можем использовать метод расширения для получения необходимого функционала*/
    public class ShoppingCart : IEnumerable<Product>
    {
        public List<Product> Products { get; set; }
        public IEnumerator<Product> GetEnumerator()
        {
            return Products.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}