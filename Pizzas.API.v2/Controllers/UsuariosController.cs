using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Services;
using Pizzas.API.Utils;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Usuario usuario)
        {   
            IActionResult  respuesta = null;
            Usuario usuario1;
            usuario1 = UsuariosServices.Login(usuario.UserName, usuario.Password);  
            if(usuario1==null){
                respuesta = NotFound();
            } else{
                respuesta = Ok(usuario1);
            }
            return respuesta;
        }
/*
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            int intRowsAffected = 0;
            Pizza entity ;
            IActionResult   respuesta = null;
            if(id!= pizza.Id ){
                return BadRequest();
            }else{
                entity = BD.GetById(id);
                if(entity == null){
                    return NotFound();
                }else{
                    intRowsAffected=BD.UpdateById(pizza);
                    if(intRowsAffected > 0){
                        respuesta = Ok(pizza);
                    }
                    else{
                        respuesta = NotFound();
                    }
                }
                
            }
            return respuesta;
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteByID(int id)
        {
            IActionResult   respuesta = null;
            int intRowsAffected = 0;
            Pizza entity;
            entity = BD.GetById(id);
            if(id < 0 ){
                respuesta = BadRequest();
            }else{
                if (entity == null){
                    respuesta = NotFound();
                }else{
                    intRowsAffected = BD.DeleteById(id);
                    if(intRowsAffected > 0){
                        respuesta = Ok(entity);
                    }else{
                        respuesta=NotFound();
                    }
                }
                
            }
            return respuesta;
        }

*/
    }


}
