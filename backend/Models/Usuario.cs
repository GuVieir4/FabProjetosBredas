using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using KiraShopApi.Enums;

namespace KiraShopApi.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public TipoUsuario Tipo { get; set; } = TipoUsuario.Cliente;

        [JsonIgnore]
        public Carrinho? Carrinho { get; set; }
    }
}
