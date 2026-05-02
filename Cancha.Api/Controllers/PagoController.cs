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
    public class PagoController : ControllerBase
    {
        private readonly DataContext _context;
        public PagoController(DataContext context)
        {
            _context = context;
        }
        [HttpGet] // GET: Lista de pagos
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            return await _context.Pagos.ToListAsync();
        }
        [HttpGet("{id:int}")] // GET: Pago por id
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound("El Pago no fue encontrado");
            }
            return pago;
        }
        [HttpPost] // Crear un nuevo pago
        public async Task<ActionResult<Pago>> CreatePago(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPago), new { id = pago.Id }, pago);
        }
        [HttpPut("{id:int}")] // Actualizar un pago existente
        public async Task<IActionResult> UpdatePago(int id, Pago pago)
        {
            if (id != pago.Id)
            {
                return BadRequest("El Id del Pago no coincide con el Id de la URL");
            }
            _context.Entry(pago).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
                {
                    return NotFound("El Pago no fue encontrado");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.Id == id);
        }
    }
}