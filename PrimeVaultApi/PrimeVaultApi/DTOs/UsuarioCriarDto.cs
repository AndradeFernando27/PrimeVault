using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PrimeVaultApi.DTOs;

public class UsuarioCriarDto
{
    [Required(ErrorMessage = "Obrigatório informar seu nome")]
    [MaxLength(100, ErrorMessage = "Informe um nome com menos de 100 caracteres")]
    [JsonPropertyName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("Email")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string? Email { get; set; }

    [JsonPropertyName("Senha")]
    [Required(ErrorMessage = "Senha obrigatória")]
    [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
    public string Senha { get; set; } = string.Empty;
}
