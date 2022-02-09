using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HockeyShop1
{
    class Menu
    {

        static string connString = "data source=.\\SQLEXPRESS; initial catalog=HockeyShop1; persist security info=true; Integrated Security=true";

        /// <summary>
        /// The Menu
        /// </summary>
        public void Run()
        {

            int val = 0;
            do
            {

                Console.WriteLine("WELCOME");
                Console.WriteLine("Choose between options 1-8");
                Console.WriteLine("1. Show all products");
                Console.WriteLine("2. More info about product");
                Console.WriteLine("3. ShoppingCart");
                Console.WriteLine("4. Admin");
                Console.WriteLine("5. Avsluta");
                Console.WriteLine("-----------------------------------");

                try
                {
                    val = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Wrong input, choose between 1-5");
                }

                switch (val)
                {
                    case 1:
                        Console.Clear();
                        SeeAllProducts();
                        break;
                    case 2:
                        Console.Clear();
                        SeeAllProducts();
                        Console.WriteLine("What product would u like to learn more about? (number on the left)");
                        MoreInfo();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Your Shoppingcart!");
                        Console.WriteLine("------------------");
                        SeeShoppingCart();



                        int newMeny = 0;
                        do
                        {

                            Console.WriteLine("What would u like to do?");
                            Console.WriteLine("1. Add product to Shoppingcart");
                            Console.WriteLine("2. Remove product from Shoppingcart");
                            Console.WriteLine("3. -");
                            Console.WriteLine("4. Exit Shoppingcart");
                            try
                            {
                                newMeny = int.Parse(Console.ReadLine());
                            }
                            catch
                            {
                                Console.WriteLine("Wrong input, choose between 1-4");
                            }

                            switch (newMeny)
                            {
                                case 1:
                                    Console.Clear();
                                    SeeAllProducts();
                                    Console.WriteLine("What product would you like to add? (Type in productID!)");
                                    var addToCart = Convert.ToInt32(Console.ReadLine());
                                    AddProductToShoppingCart(addToCart);

                                    break;
                                case 2:
                                    Console.Clear();
                                    SeeShoppingCart();
                                    Console.WriteLine("What product would u like to remove?");
                                    var removeFromCart = Convert.ToInt32(Console.ReadLine());
                                    RemoveProductFromCart(removeFromCart);
                                    break;
                                case 3: // BUY PRODUCTS!!!
                                    break;
                                case 4:
                                    BackToMainMenu();
                                    Console.Clear();
                                    break;
                                default:
                                    Console.WriteLine("Wrong input, choose between 1-4");
                                    break;
                            }


                        }
                        while (newMeny != 4);
                        break;
                    case 4:
                        Console.Clear();
                        int loginAttempts = 0;

                        for (int i = 0; i < 3; i++)
                        {

                            Console.WriteLine("Enter password");
                            string password = Console.ReadLine();

                            if (password != "FredrikFredric")
                                loginAttempts++;
                            else
                                break;
                        }
                        if (loginAttempts > 2)
                            Console.WriteLine("Login failure");
                        else
                            Console.WriteLine("Login successful");
                        Console.ReadKey();

                        int adminMenu = 0;

                        do
                        {
                            Console.WriteLine("What would u like to do?");
                            Console.WriteLine("1. Add a new product to the assortment");
                            Console.WriteLine("2. Remove a product from the assortment");
                            Console.WriteLine("3. Alter product in the assortment");
                            Console.WriteLine("4. Exit");

                            try
                            {
                                adminMenu = int.Parse(Console.ReadLine());
                            }
                            catch
                            {
                                Console.WriteLine("Wrong input, choose between 1-4");
                            }

                            switch (adminMenu)
                            {
                                case 1:
                                    Console.WriteLine("Please type in CategoryId, BrandId, ModelName, Color, Price");
                                    var newProduct = new Models.Product
                                    {
                                        CategoryId = int.Parse(Console.ReadLine()),
                                        BrandId = int.Parse(Console.ReadLine()),
                                        ModelName = Console.ReadLine(),
                                        Color = Console.ReadLine(),
                                        Price = int.Parse(Console.ReadLine()),
                                        Description = (Console.ReadLine())
                                    };
                                    Dapper.AddProductAdmin(newProduct);
                                    break;
                                case 2:
                                    SeeAllProducts();
                                    Console.WriteLine("\nPlease type in what product you want to remove (Use Id)");
                                    var removeProduct = int.Parse(Console.ReadLine());
                                    Dapper.RemoveProductAdmin(removeProduct);
                                    break;
                                case 3:
                                    break;
                                case 4:
                                    break;
                                default:
                                    Console.WriteLine("Wrong input, choose between 1-4");
                                    break;
                            }

                        } while (adminMenu != 4);

                        break;

                    case 5:
                        exit();
                        break;

                    default:
                        Console.WriteLine("Wrong input, choose between 1-5");
                        break;
                }

            } while (val != 5);
        }

        /// <summary>
        /// Ending program
        /// </summary>
        public void exit()
        {
            Console.WriteLine("Hejdå!");
        }


        /// <summary>
        /// Ending Shoppingcart
        /// </summary>
        public void BackToMainMenu()
        {

        }


        /// <summary>
        /// Shows all products LINQ
        /// </summary>
        public static void SeeAllProducts()
        {
            using (var db = new Models.HockeyShop1Context())
            {
                var products = db.Products;

                var allProducts = from prod in db.Products
                                  join category in db.Categories on prod.BrandId equals category.Id
                                  join brand in db.Brands on prod.BrandId equals brand.Id
                                  select new SortedProductQuery { PropID = prod.Id, CategoryName = category.CategoryName, BrandName = brand.BrandName, ModelName = prod.ModelName, Price = prod.Price, Color = prod.Color };


                foreach (var p in allProducts)
                {
                    Console.WriteLine($"Product ID: {p.PropID}\tCategory: {p.CategoryName}\tBrand: {p.BrandName}\tModel: {p.ModelName}\tPrice: {p.Price}kr\tColor: {p.Color}");
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------");
                }
            }
        }


        /// <summary>
        /// Description for the item
        /// </summary>
        public static void MoreInfo()
        {
            using (var db = new Models.HockeyShop1Context())
            {
                var products = db.Products;

                var moreInfo = products.Where(products => products.Id == Convert.ToInt32(Console.ReadLine()));

                foreach (var prod in moreInfo)
                {
                    Console.WriteLine($"{prod.Id}\t{prod.ModelName}\t{prod.Description}");
                }

            }
        }


        /// <summary>
        /// Shows ShoppingCart
        /// </summary>
        public static void SeeShoppingCart()
        {
            using (var db = new Models.HockeyShop1Context())
            {
                var products = db.Products;

                var shoppingCarts = from item in db.ShoppinCarts
                                    join prod in db.Products on item.ProductId equals prod.Id
                                    join category in db.Categories on item.Id equals category.Id
                                    join brand in db.Brands on item.Id equals brand.Id
                                    select new SortedProductQuery { PropID = prod.Id, Ids = item.Id, CategoryName = category.CategoryName, BrandName = brand.BrandName, ModelName = prod.ModelName, Price = prod.Price, Color = prod.Color };


                foreach (var p in shoppingCarts)
                {
                    Console.WriteLine($"Product ID: {p.PropID}\nID: {p.Ids}\nCategory: {p.CategoryName}\nBrand: {p.BrandName}\nModel: {p.ModelName}\nPrice: {p.Price}kr\nColor: {p.Color}");
                    Console.WriteLine("----------------------------------");
                }
            }
        }


        /// <summary>
        /// Adds a product to shoppingcart
        /// </summary>
        public static int AddProductToShoppingCart(int addToCart1)
        {
            using (var db = new Models.HockeyShop1Context())
            {
                {

                    int affectedRows = 0;
                    var product = db.Products;
                    var sql = $"INSERT INTO [Shoppin Cart] (CustomerId,ProductId)VALUES ({1},{addToCart1})";

                    using (var connection = new SqlConnection(connString))
                    {
                        try
                        {
                            affectedRows = connection.Execute(sql);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine($"Product added your cart!");
                        Console.WriteLine("--------------------------");
                    }
                    return affectedRows;
                }
            }
        }


        /// <summary>
        /// Removes a product from the shoppingcart
        /// </summary>
        /// <param name="removeFromCart"></param>
        /// <returns></returns>
        public static int RemoveProductFromCart(int removeFromCart)
        {
            int affectedRows = 0;
            var sql = $"DELETE FROM [Shoppin Cart] WHERE Id = {removeFromCart}";

            using (var connection = new SqlConnection(connString))
            {
                try
                {
                    affectedRows = connection.Execute(sql);

                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                Console.WriteLine($"Product removed from your cart {affectedRows}");
            }
            return affectedRows;
        }
    }
}

