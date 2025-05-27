using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PrimeVaultApi.DTOs;

public class ContaDto
{
    [Required]
    [JsonPropertyName("UserId")]
    public int User_id { get; set; }

    [JsonPropertyName("NumeroConta")]
    [Required(ErrorMessage = "Account number must be added")]
    [MaxLength(100)]
    required public string NumeroConta { get; set; }

    [JsonPropertyName("TipoConta")]
    [Required(ErrorMessage = "Account Type must be added")]
    [MaxLength(100)]
    required public string TipoConta { get; set; }
    [Required(ErrorMessage = "Balance need to have at least 0")]
    [JsonPropertyName("Saldo")]
    required public double Saldo { get; set; }
}
