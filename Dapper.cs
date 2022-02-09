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
        static string connString = "data source=.\\SQLEXPRESS; initial catalog=HockeyShop1; persist security info=true; Integrated Security=true";

        /// <summary>
        /// Shows existing products
        /// </summary>
        /// <returns></returns>
        public static List<Product> GetAllProducts()
        {
            var product = new List<Product>();



            var sql = "SELECT * FROM Products";

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                product = connection.Query<Product>(sql).ToList();

            }


            return product;

        }


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



        /// <summary>
        /// Updates a kolumn
        /// </summary>
        /// <param name="newPrice"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static int UpdateProduct(int newPrice, int productId)
        {
            int affectedRows = 0;
            var sql = $"UPDATE Products SET Price = {newPrice} WHERE Id = {productId}";
            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }
    }

}
