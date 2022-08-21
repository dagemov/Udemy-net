using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AtuoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AtuoresController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.Include(x=> x.Libros).ToListAsync();//Retornando de forma asyncona los autros de la base de datos
        }
        //CREAR
        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        //Actualizar
        //[HttpPut("{id:int}")] colocando condicion en el put de entrada de datos =api/autores/1
        [HttpPut("{id:int}")]// la url es api/autores + la ruta del put
        public async Task<ActionResult> Put(Autor autor,int id)
        {
            if (autor.Id != id)
            {
                return BadRequest("El Id del autor no Coincide con el Id del url");//error 404 badRequest
            }
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }
            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }
        //BORRAR
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Autor() { Id=id});//forma de no pasar el autor por parametro y no exponer la clase en el parametro
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
