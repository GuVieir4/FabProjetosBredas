using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KiraApi2;
using KiraShopApi.Models;
using System;

namespace KiraShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly KiraApiDbContext _context;

        public ProdutoController(KiraApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? marcaId = null,
            [FromQuery] int? categoriaId = null,
            [FromQuery] string? search = null)
        {
            var query = _context.Produtos
                .Include(p => p.Marca)
                .Include(p => p.Categorias)
                .AsQueryable();

            // Filtro por marca
            if (marcaId.HasValue)
            {
                query = query.Where(p => p.MarcaId == marcaId.Value);
            }

            // Filtro por categoria
            if (categoriaId.HasValue)
            {
                query = query.Where(p => p.Categorias.Any(c => c.Id == categoriaId.Value));
            }

            // Filtro por busca
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nome.Contains(search) || p.Descricao.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var produtos = await query
                .OrderByDescending(p => p.Id) // Ordenar por ID decrescente (mais recentes primeiro)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                produtos = produtos,
                pagination = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalItems = totalItems,
                    totalPages = totalPages,
                    hasNextPage = page < totalPages,
                    hasPreviousPage = page > 1
                }
            });
        }

        [HttpGet("lancamentos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetLancamentos()
        {
            var produtos = await _context.Produtos
                .Include(p => p.Marca)
                .Include(p => p.Categorias)
                .OrderByDescending(p => p.Id) // Ordenar por ID decrescente (mais recentes primeiro)
                .Take(5)
                .ToListAsync();

            return Ok(produtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _context.Produtos
                .Include(p => p.Marca)
                .Include(p => p.Categorias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            return produto;
        }

        [HttpGet("marca/{marcaId}")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetByMarca(int marcaId)
        {
            var produtos = await _context.Produtos
                .Include(p => p.Marca)
                .Include(p => p.Categorias)
                .Where(p => p.MarcaId == marcaId)
                .ToListAsync();

            if (produtos.Count == 0)
                return NotFound("Nenhum produto encontrado para essa marca.");

            return produtos;
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetByCategoria(int categoriaId)
        {
            var produtos = await _context.Produtos
                .Include(p => p.Marca)
                .Include(p => p.Categorias)
                .Where(p => p.Categorias.Any(c => c.Id == categoriaId))
                .ToListAsync();

            if (produtos.Count == 0)
                return NotFound("Nenhum produto encontrado para essa categoria.");

            return produtos;
        }


        [HttpPost]
        public async Task<ActionResult<Produto>> Create(ProdutoCreateDto dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagemUrl) && !IsValidUrl(dto.ImagemUrl))
            {
                return BadRequest("ImagemUrl inválida.");
            }

            bool exists = await _context.Produtos.AnyAsync(p =>
                p.Nome.ToLower().Trim() == dto.Nome.ToLower().Trim() &&
                p.MarcaId == dto.MarcaId
            );

            if (exists)
                return Conflict("Já existe um produto com esse nome e marca.");

            var marca = await _context.Marcas.FindAsync(dto.MarcaId);
            if (marca == null)
                return BadRequest("Marca não encontrada.");

            var categorias = await _context.Categorias
                .Where(c => dto.CategoriaIds.Contains(c.Id))
                .ToListAsync();

            if (categorias.Count != dto.CategoriaIds.Count)
                return BadRequest("Uma ou mais categorias não foram encontradas.");

            if (dto.Preco <= 0)
                return BadRequest("Preço deve ser maior que zero.");

            var produto = new Produto
            {
                Nome = dto.Nome.Trim(),
                Preco = dto.Preco,
                Descricao = dto.Descricao?.Trim() ?? string.Empty,
                MarcaId = dto.MarcaId,
                Categorias = categorias,
                ImagemUrl = dto.ImagemUrl?.Trim()
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProdutoCreateDto dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagemUrl) && !IsValidUrl(dto.ImagemUrl))
            {
                return BadRequest("ImagemUrl inválida.");
            }

            var produto = await _context.Produtos
                .Include(p => p.Categorias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            bool exists = await _context.Produtos.AnyAsync(p =>
                p.Id != id &&
                p.Nome.ToLower().Trim() == dto.Nome.ToLower().Trim() &&
                p.MarcaId == dto.MarcaId
            );

            if (exists)
                return Conflict("Já existe um produto com esse nome e marca.");

            var marca = await _context.Marcas.FindAsync(dto.MarcaId);
            if (marca == null)
                return BadRequest("Marca não encontrada.");

            var categorias = await _context.Categorias
                .Where(c => dto.CategoriaIds.Contains(c.Id))
                .ToListAsync();

            if (categorias.Count != dto.CategoriaIds.Count)
                return BadRequest("Uma ou mais categorias não foram encontradas.");

            if (dto.Preco <= 0)
                return BadRequest("Preço deve ser maior que zero.");

            produto.Nome = dto.Nome.Trim();
            produto.Preco = dto.Preco;
            produto.Descricao = dto.Descricao?.Trim() ?? string.Empty;
            produto.MarcaId = dto.MarcaId;
            produto.Categorias = categorias;
            produto.ImagemUrl = dto.ImagemUrl?.Trim() ?? string.Empty;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return NotFound();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }

    public class ProdutoCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string? Descricao { get; set; }
        public int MarcaId { get; set; }
        public string? ImagemUrl { get; set; }
        public List<int> CategoriaIds { get; set; } = new();
    }
}

