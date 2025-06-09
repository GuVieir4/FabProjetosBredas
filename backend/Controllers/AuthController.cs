using KiraShopApi.Dtos.Auth;
using KiraShopApi.Models;
using KiraShopApi.Services;
using KiraShopApi.Enums;
using KiraApi2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KiraShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly KiraApiDbContext _context;
        private readonly AuthService _authService;

        public AuthController(KiraApiDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (usuario == null)
                return Unauthorized(new { message = "Email ou senha inválidos." });

            if (!_authService.VerifyPassword(loginDto.Senha, usuario.Senha))
                return Unauthorized(new { message = "Email ou senha inválidos." });

            var token = _authService.GenerateToken(usuario.Id);

            var response = new AuthResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CPF = usuario.CPF,
                Tipo = usuario.Tipo,
                Token = token
            };

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Usuarios.AnyAsync(u => u.Email == registerDto.Email))
                return Conflict(new { message = "Email já cadastrado." });

            if (await _context.Usuarios.AnyAsync(u => u.CPF == registerDto.CPF))
                return Conflict(new { message = "CPF já cadastrado." });

            var hashedPassword = _authService.HashPassword(registerDto.Senha);

            var usuario = new Usuario
            {
                Nome = registerDto.Nome,
                Email = registerDto.Email,
                Senha = hashedPassword,
                CPF = registerDto.CPF,
                Tipo = TipoUsuario.Cliente // Usuário comum por padrão
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Criar carrinho para o usuário
            var carrinho = new Carrinho
            {
                UsuarioId = usuario.Id
            };
            _context.Carrinhos.Add(carrinho);
            await _context.SaveChangesAsync();

            var token = _authService.GenerateToken(usuario.Id);

            var response = new AuthResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CPF = usuario.CPF,
                Tipo = usuario.Tipo,
                Token = token
            };

            return CreatedAtAction(nameof(Login), response);
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateToken([FromBody] string token)
        {
            var userId = _authService.ValidateToken(token);
            if (userId == null)
                return Unauthorized(new { message = "Token inválido." });

            var usuario = await _context.Usuarios.FindAsync(userId);
            if (usuario == null)
                return Unauthorized(new { message = "Usuário não encontrado." });

            var response = new AuthResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CPF = usuario.CPF,
                Tipo = usuario.Tipo,
                Token = token
            };

            return Ok(response);
        }
    }
}

