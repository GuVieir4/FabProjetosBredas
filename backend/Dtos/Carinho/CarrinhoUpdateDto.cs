
using System.ComponentModel.DataAnnotations;

namespace KiraShopApi.Dtos.Carrinho
{
    public class UpdateCarrinhoDto
    {
        public List<CreateCarrinhoItemDto> Itens { get; set; } = new();
    }
}
