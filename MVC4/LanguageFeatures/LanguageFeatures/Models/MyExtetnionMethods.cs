﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageFeatures.Models
{
    public static class MyExtetnionMethods
    {

        /*Ключевое слово this перед первым параметром отмечает TotalPrices как расширенный метод.
Первый параметр говорит .NET, к какому классу можно применять метод расширения, в нашем
случае к ShoppingCart. Мы можем обратиться к экземпляру ShoppingCart, к которому был
применен расширенный метод, с помощью параметра cartParam. Наш метод перечисляет объекты
Product в ShoppingCart и возвращает сумму свойства Product.Price. 
         */

        /*Первый тип параметра изменился на IEnumerable<Product>, это обозначает, что цикл foreach в
теле метода работает непосредственно с объектами Product. В противном случае метод
расширения остается неизменным. Переход на интерфейс означает, что мы можем рассчитать
общую стоимость объектов Product, перечисленных любым IEnumerable<Product>, который
включает в себя экземпляры ShoppingCart, а также массивы объектов Product
         */

        public static decimal TotalPrices(this /*ShoppingCart*/ IEnumerable<Product> cartParam)
        {
            decimal total = 0;
            foreach (Product prod in cartParam)
            {
                total += prod.Price;
            }
            return total;
        }

        /*
        public static IEnumerable<Product> FilterByCategory(
            this IEnumerable<Product> productEnum, string categoryParam)
        {
            //foreach (Product prod in productEnum)
            //{
            //    if (prod.Category == categoryParam)
            //    {
            //        yield return prod;
            //    }
            //}
            return productEnum.Where(prod => prod.Category == categoryParam);
        }
        */


        /*
         * Мы использовали Func в качестве фильтрующего параметра, это обозначает, что нам не нужно
определять делегат в качестве типа. Делегат принимает параметр Product и возвращает значение
bool, которое будет true, если этот объект Product должны быть включен в результат.
         */

        public static IEnumerable<Product> FilterByCategory(
            this IEnumerable<Product> productEnum, string categoryParam)
        {
            foreach (Product prod in productEnum)
            {
                if (prod.Category == categoryParam)
                {
                    yield return prod;
                }
            }
        }

        public static IEnumerable<Product> Filter(
            this IEnumerable<Product> productEnum, Func<Product, bool> selectorParam)
        {
            foreach (Product prod in productEnum)
            {
                if (selectorParam(prod))
                {
                    yield return prod;
                }
            }
        }
    }
}