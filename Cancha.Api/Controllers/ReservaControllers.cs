using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cancha.Api.Data;
using Cancha.Shared.Entities;

namespace Cancha.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly DataContext _context;
        public ReservaController(DataContext context)
        {
            _context = context;
        }
        [HttpGet] // GET: Lista de reservas
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _context.Reservas.ToListAsync();
        }
        [HttpGet("{id:int}")] // GET: Reserva por id
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound("La Reserva no fue encontrada");
            }
            return reserva;
        }
        [HttpPost] // Crear una nueva reserva
        public async Task<ActionResult<Reserva>> CreateReserva(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, reserva);
        }
        [HttpPut("{id:int}")] // Actualizar una reserva existente
        public async Task<IActionResult> UpdateReserva(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest("El Id de la Reserva no coincide con el Id de la URL");
            }
            _context.Entry(reserva).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
                {
                    return NotFound("La Reserva no fue encontrada");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        private bool ReservaExists(int id) // Verificar si una reserva existe por id
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}