using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            //Listando libros de autores que existen , usamos el include de incluir
            var existe = await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
            if(existe == null)
            {
                return NotFound();
            }
            return existe;
        }
        //CREAR
        [HttpPost]
        /*ejecute asi  en el post{
          "titulo": "Libro2",
          "autorId": 1
        }*/
    public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await context.Autores.AnyAsync(x=> x.Id==libro.AutorId); //Condicional de clase amarrada que depende de la otra , en este caso libro depedne de un autor
            if (!existeAutor)
            {
                return BadRequest($"No existe el autor del id = {libro.AutorId}");
            }
            
            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
