using System.ComponentModel.DataAnnotations;

namespace KiraShopApi.Dtos.Produto
{
    public class ReadProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int MarcaId { get; set; }
        public string ImagemUrl { get; set; } = string.Empty;
        public List<int> CategoriaIds { get; set; } = new();
    }

    public class CreateProdutoDto
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public decimal Preco { get; set; }

        public string Descricao { get; set; } = string.Empty;

        [Required]
        public int MarcaId { get; set; }

        public string ImagemUrl { get; set; } = string.Empty;

        public List<int> CategoriaIds { get; set; } = new();
    }

    public class UpdateProdutoDto
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public decimal Preco { get; set; }

        public string Descricao { get; set; } = string.Empty;

        [Required]
        public int MarcaId { get; set; }

        public string ImagemUrl { get; set; } = string.Empty;

        public List<int> CategoriaIds { get; set; } = new();
    }
}