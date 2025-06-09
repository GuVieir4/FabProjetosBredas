
using System.ComponentModel.DataAnnotations;
using KiraShopApi.Dtos.Produto;

namespace KiraShopApi.Dtos.Carrinho
{
    public class CreateCarrinhoDto
    {
        [Required]
        public int UsuarioId { get; set; }
    }
}
