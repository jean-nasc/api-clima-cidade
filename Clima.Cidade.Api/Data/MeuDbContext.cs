using Clima.Cidade.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Clima.Cidade.Api.Data
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options) { }

        public DbSet<ClimaCidade> ClimaCidades { get; set; }
    }
}