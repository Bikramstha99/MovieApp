using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Models.Domain;

namespace MovieApp.Data
{
    public class MovieDbContext : IdentityDbContext
    {
        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Movies> Movies { get; set; }
    }
}
