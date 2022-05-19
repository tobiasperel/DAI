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
        public IActionResult Login(Usuario Usuario1)
        {   
            Usuario gordoUsuario1 = null;
            IActionResult respuesta = null;
            
            gordoUsuario1 = UsuariosServices.Login(Usuario1.UserName, Usuario1.Password);
            if(gordoUsuario1 != null){
                respuesta = Ok(gordoUsuario1);
            }
            else{
                respuesta = NotFound();
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
