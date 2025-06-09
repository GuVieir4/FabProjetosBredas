using System.ComponentModel.DataAnnotations;
using KiraShopApi.Enums;

namespace KiraShopApi.Dtos.Usuario
{
    public class CreateUsuarioDto
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 caracteres.")]
        public string CPF { get; set; } = string.Empty;

        [Required]
        public TipoUsuario Tipo { get; set; } = TipoUsuario.Cliente;
    }

    public class UpdateUsuarioDto
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Senha pode ser opcional na atualização, depende do seu fluxo
        public string Senha { get; set; } = string.Empty;

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 caracteres.")]
        public string CPF { get; set; } = string.Empty;

        [Required]
        public TipoUsuario Tipo { get; set; } = TipoUsuario.Cliente;
    }

    public class ReadUsuarioDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public TipoUsuario Tipo { get; set; }
    }
}
