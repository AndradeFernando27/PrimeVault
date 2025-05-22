using System.ComponentModel.DataAnnotations;

namespace PrimeVaultApi.DTOs;

public class ContaDto
{
    [Required]
    public int User_id { get; set; }

    [Required(ErrorMessage = "Account number must be added")]
    [MaxLength(100)]
    required public string NumeroConta { get; set; }

    [Required(ErrorMessage = "Account Type must be added")]
    [MaxLength(100)]
    required public string TipoConta { get; set; }
    [Required(ErrorMessage = "Balance need to have at least 0")]
    required public double Saldo { get; set; }
}
