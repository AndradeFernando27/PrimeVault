using System.ComponentModel.DataAnnotations;

namespace PrimeVaultApi.Models;

public class Conta
{
    [Key]
    int Id { get; set; }
    [Required]
    [MaxLength(100)]
    int User_id { get; set; }
    [Required (ErrorMessage = "Account number must be added")]
    [MaxLength(100)]
    string AccountNumber { get; set; } = String.Empty;
    [Required(ErrorMessage = "Account Type must be added")]
    [MaxLength(100)]
    string AccountType { get; set; } = String.Empty;
    [Required(ErrorMessage = "Balance need to have at least 0")]
    double Balance { get; set; }

    public DateTime CreateAt { get; set; }



}
