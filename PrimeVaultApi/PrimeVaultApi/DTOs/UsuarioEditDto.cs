using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PrimeVaultApi.DTOs;

public class UsuarioEditDto
{

    [Required(ErrorMessage = "Obrigatório informar o nome")]
    [JsonPropertyName("Nome")]
    [MaxLength(100, ErrorMessage = "Insira um nome com menos de 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [JsonPropertyName("Email")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string? Email { get; set; }
    
    [JsonPropertyName("Senha")]
    [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
    public string? Senha { get; set; }


}
