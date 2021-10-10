using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Icarus.Data;
using Newtonsoft.Json;
using Microsoft.Data.Sqlite;

namespace CatalogBuilder
{
    class Program
    {
        

        static void Main(string[] args)
        {
            List<Item> items = new List<Item>();

            for(int i = 1000000; i <= 1010000; i++)
            {
                Item thisItem = new Item();
                Random r = new Random();

                thisItem.Sku = i;
                thisItem.Name = "Test Item " + (i - 999999);
                thisItem.SalesFloorQuantity = r.Next(0, 1000);
                thisItem.OverstockQuantity = r.Next(0, 1000);
                thisItem.PrepaidQuantity = r.Next(0, 100);
                thisItem.MaxSalesFloor = thisItem.SalesFloorQuantity;
                thisItem.DateRecieved = DateTime.Now;
                thisItem.DateInventoried = DateTime.Now;
                thisItem.InventoriedTeamMember = "Jordan S";
                thisItem.DepartmentID = 100;
                thisItem.DepartmentName = "Test Department";
                thisItem.Catagory = "Test Item";
                thisItem.Vendors.Add("Test Vendor");
                thisItem.LocationStocked.Add($"{r.Next(100,200)}A");

                items.Add(thisItem);
            }

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = @"C:\Users\Admin\source\repos\Icarus\Documents\ICARUSData.db";

            using(var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                using(var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();

                    foreach(Item item in items)
                    {
                        insertCmd.CommandText = $"INSERT INTO Catalog VALUES('{item.Sku.ToString()}', '{item.Name}', '{item.OverallQuantity.ToString()}', '{item.SalesFloorQuantity.ToString()}', " +
                            $"'{item.OverstockQuantity.ToString()}', '{item.PrepaidQuantity.ToString()}', '{item.MaxSalesFloor.ToString()}', '{item.DateRecieved.ToString()}', " +
                            $"'{item.DateInventoried.ToString()}', '{item.InventoriedTeamMember.ToString()}', '{string.Join(", ", item.LocationStocked)}', '{item.DepartmentID.ToString()}', " +
                            $"'{item.DepartmentName}', '{item.Catagory}', '{string.Join(", ", item.Vendors)}');";

                        insertCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }
    }
}
