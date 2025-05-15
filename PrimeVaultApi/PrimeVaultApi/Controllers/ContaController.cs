using Microsoft.AspNetCore.Mvc;
using PrimeVaultApi.Db;
using PrimeVaultApi.DTOs;
using PrimeVaultApi.Models;
using AutoMapper;

namespace PrimeVaultApi.Controllers;

[ApiController]
[Route("api/conta")]
public class ContaController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ContaController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
    public async Task<IActionResult> PostConta([FromBody] ContaDto contaDto)
    {
        if(contaDto == null)
        {
            return BadRequest("conta nao deve ser nula");
        }
        var conta = _mapper.Map<Conta>(contaDto);

        await _context.AddAsync(conta);
        var verifica = await _context.SaveChangesAsync();

        if (verifica == 0)
        {
            return BadRequest("Erro ao cadastrar conta");
        }
        return Ok(conta);
    }

}
