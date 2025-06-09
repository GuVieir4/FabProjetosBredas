using System.ComponentModel.DataAnnotations;

namespace KiraShopApi.Dtos.Categoria
{
    public class ReadCategoriaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
