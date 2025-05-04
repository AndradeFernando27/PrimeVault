using System.ComponentModel.DataAnnotations;

public class Usuario{
    
    [Key]
    public int Id {get; set;}

    [Required]
    [MaxLength(100)]
    public string ?nome{get; set;} 

    public string ?email{get; set;}

    public DateOnly dataCadastro{ get; set;}// ano-mes-dia

    public string ?senha {get; set;}


    //trocar por disable na cs.proj
}