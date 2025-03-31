using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITickets.Database;
using WebAPITickets.Models;

namespace WebAPITickets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly ContextoBD _contexto;

        public UsuariosController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Usuarios>> CreateUsuario(Usuarios usuario)
        {
            usuario.us_fecha_adicion = DateTime.UtcNow;
            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.us_identificador }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, Usuarios usuario)
        {
            if (id != usuario.us_identificador)
                return BadRequest();

            var usuarioExistente = await _contexto.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
                return NotFound();

            usuarioExistente.us_nombre_completo = usuario.us_nombre_completo;
            usuarioExistente.us_correo = usuario.us_correo;
            usuarioExistente.us_clave = usuario.us_clave;
            usuarioExistente.us_ro_identificador = usuario.us_ro_identificador;
            usuarioExistente.us_estado = usuario.us_estado;
            usuarioExistente.us_modificado_por = usuario.us_modificado_por;
            usuarioExistente.us_fecha_modificacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _contexto.Usuarios.Remove(usuario);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
