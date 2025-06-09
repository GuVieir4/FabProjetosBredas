using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KiraShopApi.Dtos.Marca;
using KiraShopApi.Models;
using KiraApi2;

namespace KiraShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly KiraApiDbContext _context;

        public MarcaController(KiraApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Marca
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadMarcaDto>>> GetAll()
        {
            var marcas = await _context.Marcas.ToListAsync();

            var result = marcas.ConvertAll(m => new ReadMarcaDto
            {
                Id = m.Id,
                Nome = m.Nome
            });

            return Ok(result);
        }

        // GET: api/Marca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadMarcaDto>> Get(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);

            if (marca == null) return NotFound();

            var dto = new ReadMarcaDto
            {
                Id = marca.Id,
                Nome = marca.Nome
            };

            return Ok(dto);
        }

        // POST: api/Marca
        [HttpPost]
        public async Task<ActionResult<ReadMarcaDto>> Create(CreateMarcaDto dto)
        {
            if (await _context.Marcas.AnyAsync(m => m.Nome.ToLower() == dto.Nome.ToLower().Trim()))
                return Conflict("Já existe uma marca com esse nome.");

            var marca = new Marca
            {
                Nome = dto.Nome.Trim()
            };

            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();

            var readDto = new ReadMarcaDto
            {
                Id = marca.Id,
                Nome = marca.Nome
            };

            return CreatedAtAction(nameof(Get), new { id = marca.Id }, readDto);
        }

        // PUT: api/Marca/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMarcaDto dto)
        {
            var marca = await _context.Marcas.FindAsync(id);

            if (marca == null) return NotFound();

            if (await _context.Marcas.AnyAsync(m => m.Id != id && m.Nome.ToLower() == dto.Nome.ToLower().Trim()))
                return Conflict("Já existe outra marca com esse nome.");

            marca.Nome = dto.Nome.Trim();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Marca/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);

            if (marca == null) return NotFound();

            // Verificar se existem produtos vinculados a esta marca
            var produtosVinculados = await _context.Produtos.AnyAsync(p => p.MarcaId == id);

            if (produtosVinculados)
            {
                return BadRequest(new { message = "Não é possível excluir esta marca pois existem produtos vinculados a ela." });
            }

            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
