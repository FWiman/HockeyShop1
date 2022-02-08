using System;

namespace HockeyShop1
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();
            menu.Run();

            /* INSERT
            var newProduct = new Models.Product
            {
                CategoryId = 1,
                BrandId = 1,
                ModelName = "Super Tacks X",
                Color = "Black",
                Price = 3999
            };
            int affectedRows = Dapper.InsertProduct(newProduct);
            Console.WriteLine("A new product was added to the list! "+ affectedRows);
            */


            /* UPDATE
            var affectedRows = Dapper.UpdateProduct(2999, 14);
            Console.WriteLine($"Product Price changed where ID is 14");
            */
        }
    }
}
