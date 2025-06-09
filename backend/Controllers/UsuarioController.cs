using KiraShopApi.Dtos.Usuario;
using KiraShopApi.Models;
using KiraApi2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace KiraShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly KiraApiDbContext _context;

        public UsuariosController(KiraApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new ReadUsuarioDto
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    CPF = u.CPF,
                    Tipo = u.Tipo
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        // GET: api/Usuarios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            var readDto = new ReadUsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CPF = usuario.CPF,
                Tipo = usuario.Tipo
            };

            return Ok(readDto);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] CreateUsuarioDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Usuarios.AnyAsync(u => u.CPF == createDto.CPF))
                return Conflict(new { message = "CPF já cadastrado." });

            if (await _context.Usuarios.AnyAsync(u => u.Email == createDto.Email))
                return Conflict(new { message = "Email já cadastrado." });

            var usuario = new Usuario
            {
                Nome = createDto.Nome,
                Email = createDto.Email,
                Senha = createDto.Senha,
                CPF = createDto.CPF,
                Tipo = createDto.Tipo
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var readDto = new ReadUsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CPF = usuario.CPF,
                Tipo = usuario.Tipo
            };

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, readDto);
        }

        // PUT: api/Usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuarioDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            if (await _context.Usuarios.AnyAsync(u => u.CPF == updateDto.CPF && u.Id != id))
                return Conflict(new { message = "CPF já cadastrado por outro usuário." });

            if (await _context.Usuarios.AnyAsync(u => u.Email == updateDto.Email && u.Id != id))
                return Conflict(new { message = "Email já cadastrado por outro usuário." });

            usuario.Nome = updateDto.Nome;
            usuario.Email = updateDto.Email;

            if (!string.IsNullOrWhiteSpace(updateDto.Senha))
            {
                usuario.Senha = updateDto.Senha;
            }

            usuario.CPF = updateDto.CPF;
            usuario.Tipo = updateDto.Tipo;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
