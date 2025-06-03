using System.ComponentModel.DataAnnotations;

namespace PrimeVaultApi.Models;

public class Conta
{
    public int Id { get; set; }

    public int User_id { get; set; }
    public Usuario? Usuario { get; set; }

    public string NumeroConta { get; set; } = String.Empty;
    public string TipoConta { get; set; } = String.Empty;

    public double Saldo { get; set; }

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow; // ano-mes-dia
    public DateTime EditadoEm { get; set; } = DateTime.UtcNow;



}
