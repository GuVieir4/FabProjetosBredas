using System.ComponentModel.DataAnnotations;

namespace KiraShopApi.Dtos.Carrinho
{
    public class CreateCarrinhoItemDto
    {
        [Required]
        public int ProdutoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }
    }

    public class UpdateCarrinhoItemDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }
    }

    public class ReadCarrinhoItemDto
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; } = string.Empty;
        public string ProdutoDescricao { get; set; } = string.Empty;
        public decimal ProdutoPreco { get; set; }
        public string ProdutoImagemUrl { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal Subtotal => ProdutoPreco * Quantidade;
    }
}

