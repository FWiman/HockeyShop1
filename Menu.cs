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
            bool loop = true;
            do
            {

                Console.WriteLine("WELCOME");
                Console.WriteLine("Choose between options 1-8");
                Console.WriteLine("1. Show all products");
                Console.WriteLine("2. More info about product");
                Console.WriteLine("3. ShoppingCart");
                Console.WriteLine("4. -");
                Console.WriteLine("5. -");
                Console.WriteLine("6. -");
                Console.WriteLine("7. -");
                Console.WriteLine("8. Avsluta");
                Console.WriteLine("-----------------------------------");

                try
                {
                    val = int.Parse(Console.ReadLine());
                    loop = false;
                }
                catch
                {
                    Console.WriteLine("Du måste skriva in en siffra");
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
                                loop = false;
                            }
                            catch
                            {
                                Console.WriteLine("Du måste skriva in en siffra");
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
                                case 2: // REMOVE FROM SHOPPINGCART!
                                    break;
                                case 3:// BUY PRODUCTS!!!
                                    break;
                                case 4:
                                    BackToMainMenu();
                                    Console.Clear();
                                    break;
                                default:
                                    break;
                            }


                        }
                        while (newMeny != 4);
                        break;
                    /*case 4:
                        GetCities();
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine("Vilket parkeringshus vill du lägga till? Och i vilken stad vill du att det ska vara(StadsId) på varsin rad!");
                        var newParkingHouse = new ParkingHouse
                        {
                            HouseName = Console.ReadLine(),
                            CityId = int.Parse(Console.ReadLine())
                        };
                        InsertParkingHouse(newParkingHouse);
                        break;
                    case 5:
                        GetParkingHouses();
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Vilken siffra har din p-plats? Har den eluttag eller inte? Och vilket p-hus finns den i?(P-husID)");
                        var newParkingSlot = new ParkingSlot
                        {
                            SlotNumber = int.Parse(Console.ReadLine()),
                            ElectricOutlet = bool.Parse(Console.ReadLine()),
                            ParkingHouseId = int.Parse(Console.ReadLine())
                        };
                        break;
                    case 6:
                        GetAllCars();
                        break;
                    case 7:
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Vad för reg-nr är det?, Märke?, Färg?");
                        var newCar = new Car
                        {
                            Plate = Console.ReadLine(),
                            Make = Console.ReadLine(),
                            Color = Console.ReadLine()
                        };
                        InsertCar(newCar);
                        break; */
                    case 8:
                        exit();
                        break;

                    default:
                        Console.WriteLine("Du har tryckt på en knapp som inte finns att välja. Gör ett nytt val!");
                        break;
                }

            } while (val != 8);
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
                                    select new SortedProductQuery { PropID = prod.Id, CategoryName = category.CategoryName, BrandName = brand.BrandName, ModelName = prod.ModelName, Price = prod.Price, Color = prod.Color };


                foreach (var p in shoppingCarts)
                {
                    Console.WriteLine($"Product ID: {p.PropID}\nCategory: {p.CategoryName}\nBrand: {p.BrandName}\nModel: {p.ModelName}\nPrice: {p.Price}kr\nColor: {p.Color}");
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
                    var addtoCart = product.Where(product => product.Id == Convert.ToInt32(Console.ReadLine()));

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
    }
}

