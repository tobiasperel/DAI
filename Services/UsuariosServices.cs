using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Models;
using Pizzas.API.Utils;

namespace Pizzas.API.Services
{
    public static class UsuariosServices
    {
        public static Usuario Login(string userName, string password)
        {   
            Usuario gordoUsuario1 = null;
            string strToken;
            gordoUsuario1 = GetByUserNamePassword(userName, password);  
            if(gordoUsuario1 != null){
                strToken = RefreshToken(gordoUsuario1.Id);
                if(strToken != null){
                    gordoUsuario1 = GetByUserNamePassword(userName, password);  
                }
            }
            return gordoUsuario1;
        }

        public static Usuario GetByUserNamePassword (string userName, string password)
        {
            Usuario usuario1 = null;
            using (SqlConnection db = BD.GetConnection())
            {
                string sql = "SELECT * FROM USUARIOS WHERE UserName = @pUserName AND Password = @pPassword";
                usuario1 = db.QueryFirstOrDefault<Usuario>(sql, new { pUserName = userName, pPassword = password});
            }
            return usuario1;
        }

        public static Usuario GetByToken (string token)
        {
            Usuario usuario1 = null;
            using (SqlConnection db = BD.GetConnection())
            {
                string sql = "SELECT * FROM USUARIOS WHERE Token = @pToken";
                usuario1 = db.QueryFirstOrDefault<Usuario>(sql, new { pToken = token});
            }
            return usuario1;
        }

        public static string RefreshToken (int id)
        {
            Usuario usuario1 = null;
            using (SqlConnection db = BD.GetConnection())
            {
                string sql = "SELECT * FROM USUARIOS WHERE Id = @pId";
                usuario1 = db.QueryFirstOrDefault<Usuario>(sql, new { pId = id});
            }
            if (usuario1 !=null)
            {
                usuario1.TokenExpirationDate = DateTime.Now.AddMinutes(15);
                usuario1.Token = Guid.NewGuid().ToString();
                int intRowsAffected = 0;
                using (SqlConnection db = BD.GetConnection())
                {
                    string sql = "UPDATE Usuarios SET Token = NEWID(), TokenExpirationDate = @pExpiracion WHERE Id = @pIdUsuario";
                    intRowsAffected = db.Execute(sql, new { pidUsuario = usuario1.Id, pExpiracion = usuario1.TokenExpirationDate  });
            
                }
                return usuario1.Token;
            }
            else
            {
                return null;
            }
            
        }
        
        
    
    }
}