using System.ComponentModel.DataAnnotations;

namespace PrimeVaultApi.DTOs;

public class UsuarioCriarDto
{
    [Required(ErrorMessage = "Obrigatório informar seu nome")]
    [MaxLength(100, ErrorMessage = "Informe um nome com menos de 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Email inválido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Senha obrigatória")]
    [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
    public string Senha { get; set; } = string.Empty;
}
