using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KiraShopApi.Models;
using KiraShopApi.Dtos.Carrinho;
using KiraApi2;

namespace KiraShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly KiraApiDbContext _context;

        public CarrinhoController(KiraApiDbContext context)
        {
            _context = context;
        }

        // GET: api/carrinho/usuario/{usuarioId}
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<ReadCarrinhoDto>> GetCarrinhoByUsuario(int usuarioId)
        {
            var carrinho = await _context.Carrinhos
                .Include(c => c.Usuario)
                .Include(c => c.Itens)
                    .ThenInclude(i => i.Produto)
                        .ThenInclude(p => p.Marca)
                .Include(c => c.Itens)
                    .ThenInclude(i => i.Produto)
                        .ThenInclude(p => p.Categorias)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            if (carrinho == null)
            {
                // Se não existe carrinho, criar um novo
                var usuario = await _context.Usuarios.FindAsync(usuarioId);
                if (usuario == null)
                    return NotFound("Usuário não encontrado.");

                var novoCarrinho = new Carrinho
                {
                    UsuarioId = usuarioId,
                    Itens = new List<CarrinhoItem>()
                };

                _context.Carrinhos.Add(novoCarrinho);
                await _context.SaveChangesAsync();

                var readDtoNovo = new ReadCarrinhoDto
                {
                    Id = novoCarrinho.Id,
                    UsuarioId = novoCarrinho.UsuarioId,
                    UsuarioNome = usuario.Nome,
                    Itens = new List<ReadCarrinhoItemDto>()
                };

                return Ok(readDtoNovo);
            }

            var dto = new ReadCarrinhoDto
            {
                Id = carrinho.Id,
                UsuarioId = carrinho.UsuarioId,
                UsuarioNome = carrinho.Usuario?.Nome ?? "",
                Itens = carrinho.Itens?.Select(i => new ReadCarrinhoItemDto
                {
                    Id = i.Id,
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.Produto?.Nome ?? "",
                    ProdutoDescricao = i.Produto?.Descricao ?? "",
                    ProdutoPreco = i.Produto?.Preco ?? 0,
                    ProdutoImagemUrl = i.Produto?.ImagemUrl ?? "",
                    Quantidade = i.Quantidade
                }).ToList() ?? new List<ReadCarrinhoItemDto>()
            };

            return Ok(dto);
        }

        // POST: api/carrinho/usuario/{usuarioId}/produto/{produtoId}
        [HttpPost("usuario/{usuarioId}/produto/{produtoId}")]
        public async Task<IActionResult> AdicionarItem(int usuarioId, CreateCarrinhoItemDto createItemDto)
        {
            var carrinho = await _context.Carrinhos
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            if (carrinho == null)
            {
                // Criar carrinho se não existir
                var usuario = await _context.Usuarios.FindAsync(usuarioId);
                if (usuario == null)
                    return NotFound("Usuário não encontrado.");

                carrinho = new Carrinho
                {
                    UsuarioId = usuarioId,
                    Itens = new List<CarrinhoItem>()
                };

                _context.Carrinhos.Add(carrinho);
                await _context.SaveChangesAsync();
            }

            var produto = await _context.Produtos.FindAsync(createItemDto.ProdutoId);
            if (produto == null)
                return NotFound("Produto não encontrado.");

            // Verificar se o produto já está no carrinho
            var itemExistente = carrinho.Itens?.FirstOrDefault(i => i.ProdutoId == createItemDto.ProdutoId);
            if (itemExistente != null)
            {
                // Se já existe, atualizar a quantidade
                itemExistente.Quantidade += createItemDto.Quantidade;
                _context.CarrinhoItens.Update(itemExistente);
            }
            else
            {
                // Se não existe, criar novo item
                var novoItem = new CarrinhoItem
                {
                    CarrinhoId = carrinho.Id,
                    ProdutoId = createItemDto.ProdutoId,
                    Quantidade = createItemDto.Quantidade
                };

                _context.CarrinhoItens.Add(novoItem);
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Item adicionado ao carrinho com sucesso." });
        }

        // PUT: api/carrinho/usuario/{usuarioId}/item/{produtoId}
        [HttpPut("usuario/{usuarioId}/item/{produtoId}")]
        public async Task<IActionResult> AtualizarQuantidadeItem(int usuarioId, int produtoId, UpdateCarrinhoItemDto updateItemDto)
        {
            var carrinho = await _context.Carrinhos
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            if (carrinho == null)
                return NotFound("Carrinho não encontrado.");

            var item = carrinho.Itens?.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item == null)
                return NotFound("Item não encontrado no carrinho.");

            item.Quantidade = updateItemDto.Quantidade;
            _context.CarrinhoItens.Update(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Quantidade atualizada com sucesso." });
        }

        // DELETE: api/carrinho/usuario/{usuarioId}/item/{produtoId}
        [HttpDelete("usuario/{usuarioId}/item/{produtoId}")]
        public async Task<IActionResult> RemoverItem(int usuarioId, int produtoId)
        {
            var carrinho = await _context.Carrinhos
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

            if (carrinho == null)
                return NotFound("Carrinho não encontrado.");

            var item = carrinho.Itens?.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item == null)
                return NotFound("Item não encontrado no carrinho.");

            _context.CarrinhoItens.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Item removido do carrinho com sucesso." });
        }

        // POST: api/carrinho
        [HttpPost]
        public async Task<ActionResult<ReadCarrinhoDto>> CreateCarrinho(CreateCarrinhoDto createDto)
        {
            var usuario = await _context.Usuarios.FindAsync(createDto.UsuarioId);
            if (usuario == null)
                return BadRequest("Usuário não encontrado.");

            var carrinhoExistente = await _context.Carrinhos.FirstOrDefaultAsync(c => c.UsuarioId == createDto.UsuarioId);
            if (carrinhoExistente != null)
                return Conflict("Usuário já possui um carrinho.");

            var carrinho = new Carrinho
            {
                UsuarioId = createDto.UsuarioId,
                Itens = new List<CarrinhoItem>()
            };

            _context.Carrinhos.Add(carrinho);
            await _context.SaveChangesAsync();

            var readDto = new ReadCarrinhoDto
            {
                Id = carrinho.Id,
                UsuarioId = carrinho.UsuarioId,
                UsuarioNome = usuario.Nome,
                Itens = new List<ReadCarrinhoItemDto>()
            };

            return Ok(readDto);
        }

        // PUT: api/carrinho/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarrinho(int id, UpdateCarrinhoDto updateDto)
        {
            var carrinho = await _context.Carrinhos
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (carrinho == null)
                return NotFound();

            // Remover todos os itens existentes
            if (carrinho.Itens != null)
            {
                _context.CarrinhoItens.RemoveRange(carrinho.Itens);
            }

            // Adicionar novos itens
            var novosItens = new List<CarrinhoItem>();
            foreach (var itemDto in updateDto.Itens)
            {
                var produto = await _context.Produtos.FindAsync(itemDto.ProdutoId);
                if (produto == null)
                    return BadRequest($"Produto com ID {itemDto.ProdutoId} não encontrado.");

                novosItens.Add(new CarrinhoItem
                {
                    CarrinhoId = carrinho.Id,
                    ProdutoId = itemDto.ProdutoId,
                    Quantidade = itemDto.Quantidade
                });
            }

            _context.CarrinhoItens.AddRange(novosItens);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/carrinho/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrinho(int id)
        {
            var carrinho = await _context.Carrinhos
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (carrinho == null)
                return NotFound();

            // Remover todos os itens primeiro
            if (carrinho.Itens != null)
            {
                _context.CarrinhoItens.RemoveRange(carrinho.Itens);
            }

            _context.Carrinhos.Remove(carrinho);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

