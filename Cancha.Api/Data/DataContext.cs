using Microsoft.EntityFrameworkCore;
using Cancha.Shared.Entities;
namespace Cancha.Api.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)

        {

        }

        public DbSet<Cliente> Clientes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().HasIndex(c => c.Cedula).IsUnique();

        }
    }
}
