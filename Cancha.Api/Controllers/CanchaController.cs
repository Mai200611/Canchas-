using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cancha.Api.Data;
using Cancha.Shared.Entities;
using CanchaEntidad = Cancha.Shared.Entities.Cancha;

namespace Cancha.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanchaController : ControllerBase
    {
        private readonly DataContext _context;

        public CanchaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet] // GET: Lista de canchas
        public async Task<ActionResult<IEnumerable<CanchaEntidad>>> GetCanchas()
        {
            return await _context.Canchas.ToListAsync();
        }

        [HttpGet("{id:int}")] // GET: Cancha por id
        public async Task<ActionResult<CanchaEntidad>> GetCancha(int id)
        {
            var cancha = await _context.Canchas.FindAsync(id);

            if (cancha == null)
            {
                return NotFound("La Cancha no fue encontrada");
            }

            return cancha;
        }

        [HttpPost] // Crear una nueva cancha
        public async Task<ActionResult<CanchaEntidad>> CreateCancha(CanchaEntidad cancha)
        {
            _context.Canchas.Add(cancha);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCancha), new { id = cancha.Id }, cancha);
        }

        [HttpPut("{id:int}")] // Actualizar una cancha existente
        public async Task<IActionResult> UpdateCancha(int id, CanchaEntidad cancha)
        {
            if (id != cancha.Id)
            {
                return BadRequest("El Id de la Cancha no coincide con el Id de la URL");
            }

            _context.Entry(cancha).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanchaExists(id))
                {
                    return NotFound("La Cancha no fue encontrada");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")] // Eliminar una cancha
        public async Task<IActionResult> DeleteCancha(int id)
        {
            var cancha = await _context.Canchas.FindAsync(id);
            if (cancha == null)
            {
                return NotFound("La Cancha no fue encontrada");
            }
            _context.Canchas.Remove(cancha);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CanchaExists(int id)
        {
            return _context.Canchas.Any(e => e.Id == id);
        }

    }
}
