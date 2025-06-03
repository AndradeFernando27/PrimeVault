using System.ComponentModel.DataAnnotations;

namespace PrimeVaultApi.DTOs;

public class UsuarioLerDto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Obrigatório informar o nome")]
    [MaxLength(100, ErrorMessage = "Insira um nome com menos de 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Email inválido")]
    public string? Email { get; set; }

    public DateTime dataCriacao { get; set; }
}
