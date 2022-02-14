using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HockeyShop1
{
    class Menu
    {

        static string connString = "Server=tcp:newtonservertest.database.windows.net,1433;Initial Catalog=DemoDB;Persist Security Info=False;User ID=serveradmin;Password=Liverpool5487;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        /// <summary>
        /// The Menu
        /// </summary>
        public void Run()
        {
            int val = 0;
            do
            {
                Console.WriteLine("WELCOME");
                Console.WriteLine("Choose between options 1-5");
                Console.WriteLine("1. Show all products");
                Console.WriteLine("2. More info about product");
                Console.WriteLine("3. ShoppingCart");
                Console.WriteLine("4. Admin");
                Console.WriteLine("5. Search");
                Console.WriteLine("6. Avsluta");
                Console.WriteLine("-----------------------------------");

                try
                {
                    val = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Wrong input, choose between 1-6");
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
                            Console.WriteLine("3. Buy your shoppingcart");
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
                                    Console.WriteLine("What product would u like to remove?(Type ID)");
                                    var removeFromCart = Convert.ToInt32(Console.ReadLine());
                                    RemoveProductFromCart(removeFromCart);
                                    break;

                                case 3:
                                    Console.Clear();
                                    SeeShoppingCart();
                                    Console.WriteLine("Would you like to pay with card or swish?(1 for card and 2 for swish)");
                                    var payment = int.Parse(Console.ReadLine());
                                    if (payment == 1)
                                    {
                                        Console.WriteLine("Write your cardnumber and press enter to pay");
                                        Console.ReadLine();
                                        Payment();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Write your phonenumber and press enter to pay");
                                        Console.ReadLine();
                                        Payment();
                                    }
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
                            Console.WriteLine("3. Alter price in the assortment");
                            Console.WriteLine("4. Alter Modelname in the assortment");
                            Console.WriteLine("5. Alter description in the assortment");
                            Console.WriteLine("6. Alter category in the assortment");
                            Console.WriteLine("7. Exit");

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
                                    SeeAllProducts();
                                    Console.WriteLine("Change price on a product(Choose product with id)");
                                    var updatePriceOn = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("New price:");
                                    var newPriceOfProduct = Convert.ToInt32(Console.ReadLine());
                                    ChangeProductPrice(newPriceOfProduct, updatePriceOn);
                                    break;
                                case 4:
                                    SeeAllProducts();
                                    Console.WriteLine("Change modelname on a product(Choose product with id)");
                                    var updateId = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("New name:");
                                    var newName = Console.ReadLine();
                                    ChangeProductModelName(newName, updateId);
                                    break;
                                case 5:
                                    SeeAllProducts();
                                    Console.WriteLine("Change description on a product(Choose product with id)");
                                    var updatedesp = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Write new description:");
                                    var newDesp = Console.ReadLine();
                                    ChangeProductDescription(updatedesp, newDesp);
                                    break;
                                case 6:
                                    SeeAllProducts();
                                    Console.WriteLine("Change category on a product(Choose product with id)");
                                    var product = Convert.ToInt32(Console.ReadLine());
                                    SeeProductsForChangeCategory();
                                    Console.WriteLine("Choose wich category the product should have:");
                                    var newCategory = Convert.ToInt32(Console.ReadLine());
                                    ChangeProductCategory(newCategory, product);
                                    break;
                                case 7:
                                    BackToMainMenu();
                                    break;
                                default:
                                    Console.WriteLine("Wrong input, choose between 1-4");
                                    break;
                            }

                        } while (adminMenu != 7);
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("Tpye in your search word");
                        SearchAndShowProducts();
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 6:exit();
                        break;

                    default:
                        Console.WriteLine("Wrong input, choose between 1-5");
                        break;
                }
            } while (val != 6);
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

                var shoppingCart = from item in db.ShoppinCarts
                                   join prod in db.Products on item.ProductId equals prod.Id
                                   join category in db.Categories on prod.CategoryId equals category.Id
                                   join brand in db.Brands on prod.BrandId equals brand.Id
                                   select new SortedProductQuery { PropID = prod.Id, Ids = item.Id, CategoryName = category.CategoryName, BrandName = brand.BrandName, ModelName = prod.ModelName, Price = prod.Price, Color = prod.Color };


                foreach (var p in shoppingCart)
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


        /// <summary>
        /// Paying (emptying shoppingcart)
        /// </summary>
        /// <returns></returns>
        public static int Payment()
        {
            int affectedRows = 0;
            var sql = $"DELETE FROM [Shoppin Cart] WHERE Id BETWEEN 1 and 100";

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
                Console.WriteLine($"Thanks you shopping with us. Welcome again!");
            }
            return affectedRows;
        }


        /// <summary>
        /// Admin (Alter Price)
        /// </summary>
        /// <param name="newPrice"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static int ChangeProductPrice(float newPrice, int Id)
        {
            int affectedRows = 0;

            var sql = $"Update Products SET Price = ('{newPrice}') WHERE Id = ('{Id}')";

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
                Console.WriteLine($"Price updated ");
            }
            return affectedRows;
        }



        /// <summary>
        /// Admin (Alter Modelname)
        /// </summary>
        /// <param name="newNameOn"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static int ChangeProductModelName(string newNameOn, int Id)
        {
            int affectedRows = 0;
            var sql = $"Update Products SET ModelName = ('{newNameOn}') WHERE Id = ('{Id}')";

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
                Console.WriteLine($"Modelname updated ");
            }
            return affectedRows;

        }



        /// <summary>
        /// Admin (Alter Description)
        /// </summary>
        /// <param name="updateproduct"></param>
        /// <returns></returns>
        public static int ChangeProductDescription(int updateproduct, string newDesp)
        {

            int affectedRows = 0;
            var sql = $"Update Products SET Description = {updateproduct} WHERE Id = {updateproduct}";

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
                Console.WriteLine($"Description updated ");
            }
            return affectedRows;
        }



        /// <summary>
        /// Admin (Shows Category choices)
        /// </summary>
        public static void SeeProductsForChangeCategory()
        {
            using (var db = new Models.HockeyShop1Context())
            {
                var change = from category in db.Categories
                             select category;
                foreach (var c in change)
                {
                    Console.WriteLine($"{c.Id} {c.CategoryName}");
                }
            }
        }



        /// <summary>
        /// Admin (Alter Category)
        /// </summary>
        /// <param name="newId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static int ChangeProductCategory(int newId, int productId)
        {
            int affectedRows = 0;
            var sql = $"Update Products SET CategoryId = ('{newId}') WHERE Id = ('{productId}')";

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
                Console.WriteLine($"Category updated ");
            }
            return affectedRows;
        }



         /// <summary>
         /// Search method
         /// </summary>
        public static void SearchAndShowProducts()
        {
            var search = Console.ReadLine();

            using (var db = new Models.HockeyShop1Context())
            {
                var products = db.Products;
                var productsWithShortName = from prod in products
                                            join b in db.Brands on prod.BrandId equals b.Id
                                            where 
                                            prod.ModelName.Contains(search)
                                            orderby prod.ModelName
                                            select "Id: " + prod.Id + "\tCategory: " +prod.Category.CategoryName + "\tBrand: " + prod.Brand.BrandName + "\tName: " + prod.ModelName.ToUpper() + "\tPrice: (" + prod.Price + " kr)";

                foreach (var prodList in productsWithShortName)
                {
                    Console.WriteLine(prodList);
                }

            }

        }
    }
}


