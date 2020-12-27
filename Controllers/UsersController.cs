using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudApi.Models;
using System.Linq;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase {
      [HttpPost]
      public async Task<ActionResult<UserItem>> createUser([FromBody] UserItem user, [FromServices] UserContext context){
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
      public async Task<ActionResult<IEnumerable<UserItem>>> getUsers([FromServices] UserContext context){
        return await context.Users.ToListAsync();
      }

      [Route("{id:int}")]
      [HttpGet]
      public async Task<ActionResult<UserItem>> getUserById(int id, [FromServices] UserContext context){
        var user = await context.Users.Where(user => user.id == id).FirstOrDefaultAsync();
        if(user == null){
          return NotFound();
        }

        return new ObjectResult(user) { StatusCode = 200 };
      }

      [Route("{id:int}")]
      [HttpDelete]
      public async Task<ActionResult> deleteUserById(int id, [FromServices] UserContext context){
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
      public async Task<ActionResult> updateUserById(int id, [FromBody] UserItem userUpdated, [FromServices] UserContext context){
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
    }
}