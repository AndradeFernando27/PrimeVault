using System.ComponentModel.DataAnnotations;

namespace PrimeVaultApi.Models;

public class Conta
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]

    public int User_id { get; set; }
    public Usuario? Usuario { get; set; }

    [Required (ErrorMessage = "Account number must be added")]
    [MaxLength(100)]
    public string NumeroConta { get; set; } = String.Empty;
    [Required(ErrorMessage = "Account Type must be added")]
    [MaxLength(100)]
    public string TipoConta { get; set; } = String.Empty;
    [Required(ErrorMessage = "Balance need to have at least 0")]
    public double Saldo { get; set; }

    public DateTime CriadoEm { get; set; }



}
