using Microsoft.AspNetCore.Mvc;
using PrimeVaultApi.Db;
using PrimeVaultApi.Models;

namespace PrimeVaultApi.Controllers;

[ApiController]
[Route("api/conta")]
public class ContaController : Controller
{
    private readonly AppDbContext _context;

    public ContaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetContaById(int id)
    {
        var contaEncontrada = await _context.Conta.FindAsync(id);

        if (contaEncontrada == null)
        {
            return NotFound();
        }

        return Ok(contaEncontrada);

    }

    [HttpPost]
    public async Task<IActionResult> PostConta([FromBody] Conta conta)
    {
        if(conta == null)
        {
            return BadRequest("conta nao deve ser nula");
        }

        await _context.AddAsync(conta);
        var verifica = await _context.SaveChangesAsync();

        if (verifica == 0)
        {
            return BadRequest("Erro ao cadastrar conta");
        }
        return Ok(conta);
    }

}
