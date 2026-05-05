using Cancha.Shared.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cancha.Api.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCanchasAsync();
            await CheckClientesAsync();
            await _context.SaveChangesAsync();
        }

        private async Task CheckCanchasAsync()
        {
            if (!_context.Canchas.Any())
            {
                _context.Canchas.Add(new CanchaEntidad
                {
                    Nombre = "Cancha Central ITM",
                    Tipo = TipoCancha.Futbol,
                    PrecioHora = 50000,
                    Activa = true
                });
                _context.Canchas.Add(new CanchaEntidad
                {
                    Nombre = "Cancha Tenis Belén",
                    Tipo = TipoCancha.Tennis,
                    PrecioHora = 30000,
                    Activa = true
                });
            }
        }

        private async Task CheckClientesAsync()
        {
            if (!_context.Clientes.Any())
            {
                _context.Clientes.Add(new Cliente
                {
                    Nombre = "Admin Canchas",
                    Cedula = "1000200300",
                    Correo = "admin@canchas.com",
                    Telefono = "3004005060"
                });
            }
        }
    }
}