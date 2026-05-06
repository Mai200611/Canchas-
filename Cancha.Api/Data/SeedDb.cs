using Cancha.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CanchaEntidad = Cancha.Shared.Entities.Cancha;

namespace Cancha.Api.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // Inyectamos el manejo de usuarios y roles, requisito indispensable del PDF
        public SeedDb(DataContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            // 1. Verificar Roles y Usuario Admin (Requisito Punto 3 del PDF)
            await CheckRolesAsync();
            await CheckUserAsync("Admin", "Canchas", "admin@yopmail.com", "123456", "Admin");

            // 2. Cargar entidades de negocio (Mínimo 2 entidades según PDF)
            await CheckCanchasAsync();
            await CheckClientesAsync();

            await _context.SaveChangesAsync();
        }

        private async Task CheckRolesAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }
        }

        private async Task<IdentityUser> CheckUserAsync(string firstName, string lastName, string email, string password, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user, password);
                await _userManager.AddToRoleAsync(user, role);
            }

            return user;
        }

        private async Task CheckCanchasAsync()
        {
            if (!_context.Canchas.Any())
            {
                _context.Canchas.Add(new CanchaEntidad { Nombre = "Cancha Central ITM", Tipo = TipoCancha.Futbol, PrecioHora = 50000, Activa = true });
                _context.Canchas.Add(new CanchaEntidad { Nombre = "Cancha Tenis Belén", Tipo = TipoCancha.Tennis, PrecioHora = 30000, Activa = true });
            }
        }

        private async Task CheckClientesAsync()
        {
            if (!_context.Clientes.Any())
            {
                _context.Clientes.Add(new Cliente { Nombre = "Yeferxon Marquez", Cedula = "1000200300", Correo = "yefer@canchas.com", Telefono = "3004005060" });
            }
        }
    }
}