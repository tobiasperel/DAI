using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using Pizzas.API.Services;
using Pizzas.API.Utils;
using Pizzas.API.Helpers;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {

        [HttpGet]
        public IActionResult getAll()
        {
            IActionResult respuesta;
            List<Pizza> entityList;
            entityList = PizzasServices.GetAll();
            respuesta = Ok(entityList);
            return (respuesta);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            IActionResult respuesta;
            Pizza unaPizza =  PizzasServices.GetById(id);
            if(unaPizza == null){
                respuesta = NotFound();
            }
            else{
                respuesta = Ok(unaPizza);
            }
            
            return (respuesta);
        }
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            string headerToken;
            // Obtengo el Token del Header
            headerToken = Request.Headers["token"];
            if (!SecurityHelper.IsValidToken(headerToken))
            {
                return Unauthorized();
            }
            int intRowsAffected;
            intRowsAffected = PizzasServices.Insert(pizza);
            return CreatedAtAction(nameof(Create), new{id= pizza.Id},pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            string headerToken;
            // Obtengo el Token del Header
            headerToken = Request.Headers["token"];
            if (!SecurityHelper.IsValidToken(headerToken))
            {
                return Unauthorized();
            }
            int intRowsAffected = 0;
            Pizza entity ;
            IActionResult   respuesta = null;
            if(id!= pizza.Id ){
                return BadRequest();
            }else{
                entity = PizzasServices.GetById(id);
                if(entity == null){
                    return NotFound();
                }else{
                    intRowsAffected=PizzasServices.UpdateById(pizza);
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
            string headerToken;
            // Obtengo el Token del Header
            headerToken = Request.Headers["token"];
            if (!SecurityHelper.IsValidToken(headerToken))
            {
                return Unauthorized();
            }
            IActionResult   respuesta = null;
            int intRowsAffected = 0;
            Pizza entity;
            entity = PizzasServices.GetById(id);
            if(id < 0 ){
                respuesta = BadRequest();
            }else{
                if (entity == null){
                    respuesta = NotFound();
                }else{
                    intRowsAffected = PizzasServices.DeleteById(id);
                    if(intRowsAffected > 0){
                        respuesta = Ok(entity);
                    }else{
                        respuesta=NotFound();
                    }
                }
                
            }
            return respuesta;
        }


    }


}
