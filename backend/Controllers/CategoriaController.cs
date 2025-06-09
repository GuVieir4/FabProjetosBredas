using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KiraShopApi.Dtos.Categoria;
using KiraShopApi.Models;
using KiraApi2;

namespace KiraShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly KiraApiDbContext _context;

        public CategoriaController(KiraApiDbContext context)
        {
            _context = context;
        }

        // GET api/categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadCategoriaDto>>> GetAll()
        {
            var categorias = await _context.Categorias.ToListAsync();

            var readDtos = categorias.Select(c => new ReadCategoriaDto
            {
                Id = c.Id,
                Nome = c.Nome
            });

            return Ok(readDtos);
        }

        // GET api/categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCategoriaDto>> GetById(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return NotFound();

            var readDto = new ReadCategoriaDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };

            return Ok(readDto);
        }

        // POST api/categoria
        [HttpPost]
        public async Task<ActionResult<ReadCategoriaDto>> Create(CreateCategoriaDto dto)
        {
            // Checa se nome já existe (opcional)
            bool exists = await _context.Categorias.AnyAsync(c => c.Nome.ToLower() == dto.Nome.ToLower().Trim());
            if (exists)
                return Conflict("Já existe uma categoria com esse nome.");

            var categoria = new Categoria
            {
                Nome = dto.Nome.Trim()
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            var readDto = new ReadCategoriaDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, readDto);
        }

        // PUT api/categoria/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoriaDto dto)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return NotFound();

            // Checa duplicidade ignorando o atual
            bool exists = await _context.Categorias
                .AnyAsync(c => c.Id != id && c.Nome.ToLower() == dto.Nome.ToLower().Trim());
            if (exists)
                return Conflict("Já existe uma categoria com esse nome.");

            categoria.Nome = dto.Nome.Trim();
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return NotFound();

            // Verificar se existem produtos vinculados a esta categoria
            var produtosVinculados = await _context.Produtos
                .Include(p => p.Categorias)
                .AnyAsync(p => p.Categorias.Any(c => c.Id == id));

            if (produtosVinculados)
            {
                return BadRequest(new { message = "Não é possível excluir esta categoria pois existem produtos vinculados a ela." });
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
