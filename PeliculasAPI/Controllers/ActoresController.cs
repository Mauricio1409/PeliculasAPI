using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route ("api/Actores")]
    public class ActoresController : Controller
    {
        private readonly AplicationDBContext context;
        private readonly IMapper mapper;

        public ActoresController(AplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet(Name = "ObtenerActores")]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            var entidades = await context.Actores.ToListAsync();
            var dtos = mapper.Map<List<ActorDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{Id:int}", Name = "ObtenerActor")]
        public async Task<ActionResult<ActorDTO>> GetById(int Id)
        {
            var Actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == Id);
            if (Actor == null)
            {
                return NotFound();
            }
            var dto = mapper.Map<ActorDTO>(Actor);
            return dto;
        }

        [HttpPost(Name = "CrearActor")]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            var existeActor = await context.Actores.AnyAsync(x => x.Nombre == actorCreacionDTO.Nombre);
            if (existeActor)
            {
                return BadRequest($"El genero de nombre {actorCreacionDTO.Nombre} ya esta reguistrado");
            }
            var nuevoActor = mapper.Map<Actor>(actorCreacionDTO);
            context.Add(nuevoActor);
            await context.SaveChangesAsync();

            var dto = mapper.Map<ActorDTO>(nuevoActor);
            return new CreatedAtRouteResult("ObtenerActor", new { Id = dto.Id }, dto);

        }
        [HttpPut("{Id:int}", Name = "ActualizarActor")]
        public async Task<ActionResult> Put([FromForm] ActorCreacionDTO actorCreacionDTO, int Id)
        {
            var existe = await context.Actores.AnyAsync(x => x.Id == Id);
            if (!existe)
            {
                return BadRequest("No se encontro un Actor con el Id Ingresado");
            }

            var entidad = mapper.Map<Actor>(actorCreacionDTO);
            entidad.Id = Id;

            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{Id:int}", Name = "BorrarActor")]
        public async Task<ActionResult> Delete(int Id)
        {
            var existe = await context.Actores.AnyAsync(x => x.Id == Id);
            if (!existe)
            {
                return BadRequest("No se encontro un Actor con el Id Ingresado");
            }
            context.Remove(new Actor { Id = Id });
            await context.SaveChangesAsync();
            return NoContent();

        }
    }
}
