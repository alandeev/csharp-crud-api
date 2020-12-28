using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CrudApi.Models;
using CrudApi.Data;

namespace CrudApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController: ControllerBase {
    public override NotFoundObjectResult NotFound(Object errors){
      return new NotFoundObjectResult(new { typeError = "NotFound", StatusCode = "404", errors });
    }

    [HttpPost]
    public async Task<ActionResult<UserItem>> createUser([FromBody] UserItem user, [FromServices] DataContext context){
      if(!ModelState.IsValid){
        return BadRequest(ModelState);
      }

      try{
        context.Users.Add(user);
        await context.SaveChangesAsync();
      
        return new ObjectResult(user) { StatusCode = 201 };
      }catch(Exception error){
        return new ObjectResult( new { error = error.Message } ) { StatusCode = 400 };
      }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserItem>>> getUsers([FromServices] DataContext context){
      return await context.Users.Include(user => user.techs).ToListAsync();
    }

    [Route("{id:int}")]
    [HttpGet]
    public async Task<ActionResult<UserItem>> getUserById(int id, [FromServices] DataContext context){
      var user = await context.Users.Where(user => user.id == id).Include(user => user.techs).FirstOrDefaultAsync();
      if(user == null){
        return NotFound();
      }

      return new ObjectResult(user) { StatusCode = 200 };
    }

    [Route("{id:int}")]
    [HttpDelete]
    public async Task<ActionResult> deleteUserById(int id, [FromServices] DataContext context){
      var user = await context.Users.FindAsync(id);
      if(user == null){
        return NotFound();
      }

      context.Users.Remove(user);
      await context.SaveChangesAsync();
      
      return NoContent();
    }

    [Route("{id:int}")]
    [HttpPut]
    public async Task<ActionResult> updateUserById(int id, [FromBody] UserItem userUpdated, [FromServices] DataContext context){
      var user = await context.Users.FindAsync(id);
      if(user == null){
        return NotFound();
      }

      bool isEdited = false;

      if(userUpdated.name != null){
        user.name = userUpdated.name;
        isEdited = true;
      }

      if(userUpdated.age > 0){
        user.age = userUpdated.age;
        isEdited = true;
      }

      if(isEdited){
        await context.SaveChangesAsync();
      }

      return NoContent();
    }

    [HttpPost]
    [Route("{user_id:int}/techs/{tech_id:int}")]
    public async Task<ActionResult> addTech(int user_id, int tech_id, [FromServices] DataContext context){
      var user = await context.Users.FindAsync(user_id);
      if(user == null){
        return NotFound(new string[] { "Usuario não encontrado" });
      }

      // if(user.techs.Find(tech => tech.id == tech_id) != null){
      //   return BadRequest();
      // }

      var tech = await context.Techs.FindAsync(tech_id);
      if(tech == null){
        return NotFound(new string[] { "Tecnologia não encontrada" });
      }

      List<TechItem> teches = new List<TechItem>();
      teches.Add(tech);

      user.techs = teches;
      await context.SaveChangesAsync();
      
      return new ObjectResult(user) { StatusCode = 201 };
    }
  }
}