using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cancha.Api.Data;
using Cancha.Shared.Entities;

namespace Cancha.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorariosDisponibleController : ControllerBase
    {
        private readonly DataContext _context;
        public HorariosDisponibleController(DataContext context)
        {
            _context = context;
        }
        [HttpGet] // GET: Lista de horarios disponibles
        public async Task<ActionResult<IEnumerable<HorarioDisponible>>> GetHorariosDisponibles()
        {
            return await _context.HorariosDisponibles.ToListAsync();
        }
        [HttpGet("{id:int}")] // GET: Horario disponible por id
        public async Task<ActionResult<HorarioDisponible>> GetHorarioDisponible(int id)
        {
            var horarioDisponible = await _context.HorariosDisponibles.FindAsync(id);
            if (horarioDisponible == null)
            {
                return NotFound("El Horario Disponible no fue encontrado");
            }
            return horarioDisponible;
        }
        [HttpPost] // Crear un nuevo horario disponible
        public async Task<ActionResult<HorarioDisponible>> CreateHorarioDisponible(HorarioDisponible horarioDisponible)
        {
            _context.HorariosDisponibles.Add(horarioDisponible);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHorarioDisponible), new { id = horarioDisponible.Id }, horarioDisponible);
        }
        [HttpPut("{id:int}")] // Actualizar un horario disponible existente
        public async Task<IActionResult> UpdateHorarioDisponible(int id, HorarioDisponible horarioDisponible)
        {
            if (id != horarioDisponible.Id)
            {
                return BadRequest("El Id del Horario Disponible no coincide con el Id de la URL");
            }
            _context.Entry(horarioDisponible).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioDisponibleExists(id))
                {
                    return NotFound("El Horario Disponible no fue encontrado");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        private bool HorarioDisponibleExists(int id)
        {
            return _context.HorariosDisponibles.Any(e => e.Id == id);
        }
        [HttpDelete("{id:int}")] // Eliminar un horario disponible por id
        public async Task<IActionResult> DeleteHorarioDisponible(int id)
        {
            var horarioDisponible = await _context.HorariosDisponibles.FindAsync(id);
            if (horarioDisponible == null)
            {
                return NotFound("El Horario Disponible no fue encontrado");
            }
            _context.HorariosDisponibles.Remove(horarioDisponible);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}