using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CrudApi.Models;
using CrudApi.Data;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrudApi.Controllers
{
  [Route("/api/techs")]
  [ApiController]
  public class TechController: ControllerBase {
    
    public override NotFoundObjectResult NotFound(Object errors){
      return new NotFoundObjectResult(new { typeError = "NotFound", StatusCode = "404", errors });
    }

    [HttpPost]
    public async Task<ActionResult> userCreate([FromBody] TechItem techItem, [FromServices] DataContext context, int id){
      context.Techs.Add(techItem);
      await context.SaveChangesAsync();

      return new ObjectResult(techItem) { StatusCode = 201 };
    }

    [HttpGet]
    public async Task<ActionResult<List<TechItem>>> GetAll([FromServices] DataContext context){
      return await context.Techs
      .AsNoTracking()
      .ToListAsync();
    }
  }
}