using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrimeVaultApi.DTOs;

public class UsuarioLoginDto
{
    [JsonPropertyName("Login")]
    public string? Login {get; set;}

    [JsonPropertyName("Senha")]
    public string? Senha  {get; set;}
}
