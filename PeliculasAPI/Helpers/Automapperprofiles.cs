using AutoMapper;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Helpers
{
    public class Automapperprofiles : Profile
    {
        public Automapperprofiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>().ReverseMap();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>().ReverseMap();
        }
    }
}
