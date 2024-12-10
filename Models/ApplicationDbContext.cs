using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReceitasLiterarias.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Comentario> Comentarios { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }

}