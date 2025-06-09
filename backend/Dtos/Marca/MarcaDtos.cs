using System.ComponentModel.DataAnnotations;
using KiraShopApi.Dtos.Produto;

namespace KiraShopApi.Dtos.Marca
{
    public class CreateMarcaDto
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
    }

    public class ReadMarcaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<ReadProdutoDto> Produtos { get; set; } = new();
    }

    public class UpdateMarcaDto
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
    }
}