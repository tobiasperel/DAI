using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;

namespace Pizzas.API.Utils
{
    public static class BD
    {
        private static string CONNECTION_STRING = @"Persist Security Info=False;User ID=Pizzas;password=Pizzas;Initial Catalog=DAI-Pizzas;Data Source=.;";
        public static SqlConnection GetConnection(){
            SqlConnection db;
            string connectionString = CONNECTION_STRING;
            db = new SqlConnection(connectionString);
            return db;
        }
    }
}