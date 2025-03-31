using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITickets.Database;
using WebAPITickets.Models;

namespace WebAPITickets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly ContextoBD _contexto;

        public RolesController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Roles>>> GetRol()

        {

            return await _contexto.Roles.ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<Roles>> CreateRol(Roles rol)
        {
            rol.ro_fecha_adicion = DateTime.UtcNow;
            _contexto.Roles.Add(rol);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRol),new { id = rol.ro_identificador }, rol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRol(int id, Roles rol)
        {
            if (id != rol.ro_identificador)
                return BadRequest();

            var rolExistente = await _contexto.Roles.FindAsync(id);
            if (rolExistente == null)
                return NotFound();

            rolExistente.ro_descripcion = rol.ro_descripcion;
            rolExistente.ro_modificado_por = rol.ro_modificado_por;
            rolExistente.ro_fecha_modificacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var rol = await _contexto.Roles.FindAsync(id);
            if (rol == null)
                return NotFound();

            _contexto.Roles.Remove(rol);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

    }
}
