using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : Controller
    {
        private readonly AplicationDBContext context;
        private readonly IMapper mapper;

        public GenerosController(AplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet (Name = "ObtenerGeneros")]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            var entidades = await context.Generos.ToListAsync();
            var dtos = mapper.Map<List<GeneroDTO>>(entidades);
            return dtos;
        }

        [HttpGet ("{Id:int}", Name = "ObtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> GetById(int Id)
        {
            var Genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == Id);
            if (Genero == null)
            {
                return NotFound();
            }
            var dto = mapper.Map<GeneroDTO>(Genero);
            return dto;
        }

        [HttpPost (Name = "CrearGenero")]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var existeGenero = await context.Generos.AnyAsync(x => x.Nombre == generoCreacionDTO.Nombre);
            if (existeGenero)
            {
                return BadRequest($"El genero de nombre {generoCreacionDTO.Nombre} ya esta reguistrado");
            }
            var nuevoGenero = mapper.Map<Genero>(generoCreacionDTO);
            context.Add(nuevoGenero);
            await context.SaveChangesAsync();

            var dto = mapper.Map<GeneroDTO>(nuevoGenero);
            return new CreatedAtRouteResult("ObtenerGenero", new { Id = dto.Id }, dto);

        }
        [HttpPut ("{Id:int}", Name ="ActualizarGenero")]
        public async Task<ActionResult> Put([FromBody] GeneroCreacionDTO generoCreacionDTO, int Id)
        {
            var existe = await context.Generos.AnyAsync(x => x.Id == Id);
            if (!existe)
            {
                return BadRequest("No se encontro un genero con el Id Ingresado");
            }

            var entidad = mapper.Map<Genero>(generoCreacionDTO);
            entidad.Id = Id;

            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete ("{Id:int}", Name ="BorrarGenero")]
        public async Task<ActionResult> Delete(int Id)
        {
            var existe = await context.Generos.AnyAsync(x => x.Id == Id);
            if (!existe)
            {
                return BadRequest("No se encontro un genero con el Id Ingresado");
            }
            context.Remove(new Genero { Id = Id });
            await context.SaveChangesAsync();
            return NoContent();

        }


    }
}

    