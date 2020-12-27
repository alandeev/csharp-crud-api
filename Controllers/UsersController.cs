using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudApi.Models;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase {
      private UserContext _context;
      public UsersController(UserContext context){
        _context = context;
      }

      [HttpPost]
      public async Task<ActionResult<UserItem>> createUser(UserItem user){
        if(!ModelState.IsValid){
          return BadRequest(ModelState);
        }

        try{
          _context.Users.Add(user);
          await _context.SaveChangesAsync();
        
          return new ObjectResult(user) { StatusCode = 201 };
        }catch(Exception error){
          return new ObjectResult( new { error = error.Message } ) { StatusCode = 400 };
        }
      }

      [HttpGet]
      public async Task<ActionResult<IEnumerable<UserItem>>> getUsers(){
        return await _context.Users.ToListAsync();
      }

      [Route("{id:long}")]
      [HttpGet]
      public async Task<ActionResult<UserItem>> getUserById(long id){
        var user = await _context.Users.FindAsync(id);
        if(user == null){
          return NotFound();
        }

        return new ObjectResult(user) { StatusCode = 200 };
      }

      [Route("{id:long}")]
      [HttpDelete]
      public async Task<ActionResult> deleteUserById(long id){
        var user = await _context.Users.FindAsync(id);
        if(user == null){
          return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return NoContent();
      }

      [Route("{id:long}")]
      [HttpPut]
      public async Task<ActionResult> updateUserById(long id, UserItem userUpdated){
        var user = await _context.Users.FindAsync(id);
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
          await _context.SaveChangesAsync();
        }

        return NoContent();
      }
    }
}