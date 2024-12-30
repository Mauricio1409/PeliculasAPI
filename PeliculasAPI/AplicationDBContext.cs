using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entidades;

namespace PeliculasAPI
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext(DbContextOptions options) : base(options)
        {   
        }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
    }
}
