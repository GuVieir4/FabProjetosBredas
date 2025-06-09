using System.ComponentModel.DataAnnotations;

namespace KiraShopApi.Dtos.Categoria
{
    public class CreateCategoriaDto
    {
        [Required]
        public string Nome { get; set; }
    }

}
