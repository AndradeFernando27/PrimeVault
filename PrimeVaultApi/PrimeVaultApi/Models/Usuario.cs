using PrimeVaultApi.Models;
using System.ComponentModel.DataAnnotations;

public class Usuario{
    
    [Key]
    public int Id {get; set;}

    [Required]
    [MaxLength(100)]
    public string ?Nome{get; set;} 

    public string ?Email{get; set;}

    public string ?Senha {get; set;}

    public Conta? Conta { get; set;}

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow; // ano-mes-dia
    public DateTime EditadoEm { get; set; } = DateTime.UtcNow;


    //trocar por disable na cs.proj
}