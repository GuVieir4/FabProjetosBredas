using System.ComponentModel.DataAnnotations;

namespace KiraShopApi.Dtos.Categoria
{
    public class UpdateCategoriaDto
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
    }
}
