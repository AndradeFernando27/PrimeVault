using Microsoft.AspNetCore.Mvc;
using PrimeVaultApi.Db;
using PrimeVaultApi.DTOs;
using PrimeVaultApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

    [HttpGet("todos")]
    public async Task<IActionResult> GetAllContas()
    {
        var contas = await _context.Conta.ToListAsync();

        if(contas == null)
        {
            throw new Exception("Nenhuma conta encontrada");
        }

        return Ok(contas);

    }
    [HttpGet("{numeroConta}")]
    public async Task<IActionResult> GetContasByNumero(string numeroConta)
    {
        if (numeroConta == null)
        {
            return BadRequest("Numero da conta nao pode ser nulo ou vazio");
        }


        var conta = await _context.Conta.FirstOrDefaultAsync(c => c.NumeroConta == numeroConta);

        if (conta == null)
        {
            return NotFound("Nenhuma conta encontrada");
        }

        return Ok(conta);
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> PostConta([FromBody] ContaDto contaDto)
    {
        try
        {
            if (contaDto == null)
            {
                return BadRequest("conta nao deve ser nula");
            }

            var existingConta = await _context.Conta.FirstOrDefaultAsync(
                            c => c.NumeroConta == contaDto.NumeroConta);

            if (existingConta != null)
            {
                return BadRequest("Numero da conta ja existe");
            }

            var userExisting = await _context.Usuario.FirstOrDefaultAsync(
                            u => u.Id == contaDto.User_id);

            if(userExisting == null)
            {
                return BadRequest("Usuario nao encontrado");
            }

            var conta = _mapper.Map<Conta>(contaDto);
            conta.CriadoEm = DateTime.UtcNow;

            Console.WriteLine("dados: " + contaDto.User_id + contaDto.NumeroConta + " " + contaDto.TipoConta + " " + contaDto.Saldo);

            await _context.AddAsync(conta);
            var verifica = await _context.SaveChangesAsync();

            if (verifica == 0)
            {
                return BadRequest("Erro ao cadastrar conta");
            }
            return Ok(conta);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro interno: " + ex.Message);
        }
    }
    [HttpPut("atualizar")]
    public async Task<IActionResult> PutConta([FromBody] ContaDto contaDto)
    {
        if (contaDto == null)
        {
            return BadRequest("conta nao deve ser nula");
        }

        var conta = await _context.Conta.FirstOrDefaultAsync(c => c.NumeroConta == contaDto.NumeroConta);

       

        if (conta == null)
        {
            return NotFound("Conta nao encontrada");
        }
        
        var contaAlterada = _mapper.Map(contaDto, conta);

        _context.Update(conta);
        var verifica = await _context.SaveChangesAsync();
        if (verifica == 0)
        {
            return BadRequest("Erro ao atualizar conta");
        }

        return Ok(conta);
    }

    [HttpDelete("delete/{numeroConta}")]
    public async Task<IActionResult> DeleteConta(string numeroConta)
    {
        if(numeroConta == null)
        {
            return BadRequest("Numero da conta nao pode ser nulo ou vazio");
        }

        var conta = await _context.Conta.FirstOrDefaultAsync(Conta => Conta.NumeroConta == numeroConta);

        if (conta == null)
        {
            return NotFound("Conta nao encontrada");
        }
        _context.Remove(conta);

        var verifica = await _context.SaveChangesAsync();
        if (verifica == 0)
        {
            return BadRequest("Erro ao deletar conta");
        }

        return Ok("Conta deletada com sucesso");
    }
}
