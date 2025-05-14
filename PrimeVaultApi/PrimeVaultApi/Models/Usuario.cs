using PrimeVaultApi.Models;
using System.ComponentModel.DataAnnotations;

public class Usuario{
    
    [Key]
    public int Id {get; set;}

    [Required]
    [MaxLength(100)]
    public string ?Nome{get; set;} 

    public string ?Email{get; set;}

    public DateTime CriadoEm{ get; set;}// ano-mes-dia

    public string ?Senha {get; set;}

    public Conta? Conta { get; set;}


    //trocar por disable na cs.proj
}