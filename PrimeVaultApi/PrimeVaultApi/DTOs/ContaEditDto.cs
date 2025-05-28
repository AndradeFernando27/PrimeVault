using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PrimeVaultApi.DTOs;

public class ContaEditDto
{

    [JsonPropertyName("NumeroConta")]
    [Required(ErrorMessage = "Account number must be added")]
    [MaxLength(100)]
    required public string NumeroConta { get; set; }

    [JsonPropertyName("TipoConta")]
    [Required(ErrorMessage = "Account Type must be added")]
    [MaxLength(100)]
    required public string TipoConta { get; set; }

}
