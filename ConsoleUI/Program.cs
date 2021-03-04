using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductTest();
            //CategoryTest();
            //ProductManager productManager = new ProductManager(new EfProductDal());
            //var result = productManager.GetAll();
            //if (result.Success==true)
            //{
            //    foreach (var product in result.Data)
            //    {
            //        Console.WriteLine(product.ProductName);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}


        }

        private static void CategoryTest()
        {
            //CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            //foreach (var category in categoryManager.GetAll())
            //{
            //    Console.WriteLine(category.CategoryName);
            //}
        }

        private static void ProductTest()
        {
            //ProductManager productManager = new ProductManager(new EfProductDal());
            //var result = productManager.productDetaDtos();
            //if (result.Success == true)
            //{
            //    foreach (var product in result.Data)
            //    {
            //        Console.WriteLine(product.ProductName + "/" + product.CategoryName);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}

        }
    }
}
