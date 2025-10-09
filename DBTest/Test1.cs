using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using HorseEvent;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DBTest
{
    [TestClass]
    public sealed class Test1
    {
        private string ConnectionString { get; set; }

        [TestInitialize]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [TestMethod]
        public void TestMethod1()
        {
            using (var connection = new SqlConnection(ConnectionString))
            { 
                connection.Open();

                DataTable schematable = connection.GetSchema("Columns", new string[] { null, null, "Horses", null });

                var columns = schematable.AsEnumerable()
                    .Select(DataRowAttribute => DataRowAttribute.Field<string>("COLUMN_NAME"))
                    .ToList();

                Assert.IsTrue(columns.Contains("Id"), "Column Id should exist");
                Assert.IsTrue(columns.Contains("Name"), "Column name should exist");
            }

        }
    }
}
