using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    // Интерфейс позволяет нам сломать сильную связь между классами 
    // ShoppingCart и LinqValueCalculator
    public class LinqValueCalculator : IValueCalculator
    {
        private IDiscountHelper discounter;

        public LinqValueCalculator(IDiscountHelper discountParam)
        {
            discounter = discountParam;
        }
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            //return products.Sum(p => p.Price);
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}