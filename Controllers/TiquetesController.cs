using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITickets.Database;
using WebAPITickets.Models;

namespace WebAPITickets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiquetesController : Controller
    {
        private readonly ContextoBD _contexto;

        public TiquetesController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTiquetes()
        {
            return await _contexto.Tiquetes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Tiquetes>> CreateTiquete(Tiquetes tiquete)
        {
            tiquete.ti_fecha_adicion = DateTime.UtcNow;
            _contexto.Tiquetes.Add(tiquete);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTiquetes), new { id = tiquete.ti_identificador }, tiquete);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTiquete(int id, Tiquetes tiquete)
        {
            if (id != tiquete.ti_identificador)
                return BadRequest();

            var tiqueteExistente = await _contexto.Tiquetes.FindAsync(id);
            if (tiqueteExistente == null)
                return NotFound();

            tiqueteExistente.ti_asunto = tiquete.ti_asunto;
            tiqueteExistente.ti_categoria = tiquete.ti_categoria;
            tiqueteExistente.ti_us_id_asigna = tiquete.ti_us_id_asigna;
            tiqueteExistente.ti_urgencia = tiquete.ti_urgencia;
            tiqueteExistente.ti_importancia = tiquete.ti_importancia;
            tiqueteExistente.ti_estado = tiquete.ti_estado;
            tiqueteExistente.ti_solucion = tiquete.ti_solucion;
            tiqueteExistente.ti_modificado_por = tiquete.ti_modificado_por;
            tiqueteExistente.ti_fecha_modificacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiquete(int id)
        {
            var tiquete = await _contexto.Tiquetes.FindAsync(id);
            if (tiquete == null)
                return NotFound();

            _contexto.Tiquetes.Remove(tiquete);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
