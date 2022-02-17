using Dapper;
using HockeyShop1.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HockeyShop1
{
    class Dapper
    {
        static string connString = "Server=tcp:newtonservertest.database.windows.net,1433;Initial Catalog=DemoDB;Persist Security Info=False;User ID=serveradmin;Password=Liverpool5487;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        /// <summary>
        /// Inserts one new product to the productlist
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>
        public static int AddProductAdmin(Product prod)
        {
            int affectedRows = 0;
            var sql = $"INSERT INTO Products (CategoryId, BrandId, ModelName, Color, Price) VALUES ('{prod.CategoryId}', '{prod.BrandId}', '{prod.ModelName}', '{prod.Color}','{prod.Price}')";

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                try
                {
                    affectedRows = connection.Execute(sql);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Product added!");
            }
            return affectedRows;
        }



        /// <summary>
        /// Removes one product from productlist
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static int RemoveProductAdmin(int product)
        {


            int affectedRows = 0;
            var sql = $"DELETE FROM Products WHERE Id = {product}";
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
                Console.WriteLine("Product removed");

            }
            return affectedRows;
        }

    }
}
