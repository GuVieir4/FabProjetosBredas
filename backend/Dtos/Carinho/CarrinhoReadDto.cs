
using System.ComponentModel.DataAnnotations;
using KiraShopApi.Dtos.Produto;

namespace KiraShopApi.Dtos.Carrinho
{
    public class ReadCarrinhoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; } = string.Empty;
        public List<ReadCarrinhoItemDto> Itens { get; set; } = new();
        public decimal Total => Itens.Sum(i => i.Subtotal);
    }
}
