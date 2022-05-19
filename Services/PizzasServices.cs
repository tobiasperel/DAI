using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Utils;
namespace Pizzas.API.Services
{
    public static class PizzasServices
    {
        private static string _connectionString = @"Persist Security Info=False;User ID=Pizzas;password=Pizzas;Initial Catalog=DAI-Pizzas;Data Source=.;";

        public static List<Pizza> GetAll()
        {
            List<Pizza> returnList;
            using (SqlConnection db = BD.GetConnection())
            {
                string sql = "SELECT * FROM PIZZAS";
                returnList = db.Query<Pizza>(sql).ToList();
            }
            return returnList;
        }
        
        public static Pizza GetById(int id)
        {
            Pizza pizza = null;
            using (SqlConnection db = BD.GetConnection())
            {
                
                string sql = "SELECT * FROM PIZZAS WHERE Id = @pId";
                pizza = db.QueryFirstOrDefault<Pizza>(sql, new { pId = id });
            }
            return pizza;
        }

        public static int Insert(Pizza pizza)
        {
            int intRowsAffected = 0;
            using (SqlConnection db =  BD.GetConnection())
            {
                string sql = "INSERT INTO Pizzas (Nombre,LibreGluten,Importe,Descripcion) VALUES (@pNombre,@pLibreGluten,@pImporte,@pDescripcion)";
                intRowsAffected = db.Execute(sql, new { pNombre = pizza.Nombre, pLibreGluten = pizza.LibreGluten, pImporte = pizza.Importe, pDescripcion = pizza.Descripcion });
                
            }
            return intRowsAffected;
        }
        
        public static int UpdateById(Pizza pizza)
        {
            int intRowsAffected = 0;
            string  sqlQuery;
            sqlQuery  = "UPDATE Pizzas SET ";
            sqlQuery += "    Nombre         = @pNombre, ";
            sqlQuery += "    LibreGluten    = @pLibreGluten, ";
            sqlQuery += "    Importe        = @pImporte, ";
            sqlQuery += "    Descripcion    = @pDescripcion ";
            sqlQuery += "WHERE Id = @idPizza";

            using (SqlConnection db = BD.GetConnection())
            {
                intRowsAffected = db.Execute(sqlQuery, new { idPizza = pizza.Id, pNombre = pizza.Nombre, pLibreGluten = pizza.LibreGluten, pImporte = pizza.Importe, pDescripcion = pizza.Descripcion });
            }
            return intRowsAffected;
        }
        public static int DeleteById(int id){
            int intRowsAffected = 0;

            string  sqlQuery;
            sqlQuery  = "DELETE ";
            sqlQuery += "FROM Pizzas ";
            sqlQuery += "WHERE Id = @idPizza";
            using (SqlConnection db = BD.GetConnection())
            {
                intRowsAffected = db.Execute(sqlQuery, new { idPizza = id});
            }
            return intRowsAffected;
        }


    }
}