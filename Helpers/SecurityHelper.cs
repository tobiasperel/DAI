using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Utils;

namespace Pizzas.API.Helpers
{
    public class SecurityHelper
    {
        public static bool IsValidToken(string token)
        {
                DateTime tokenDate;
                using (SqlConnection db = BD.GetConnection())
                {
                    string sql = "SELECT TokenExpirationDate FROM USUARIOS WHERE Token = @pToken";
                    tokenDate = db.QueryFirstOrDefault<DateTime>(sql, new { pToken = token});
                }
                if(tokenDate > DateTime.Now){
                    return true;
                }
                return false;
        }
    }
}