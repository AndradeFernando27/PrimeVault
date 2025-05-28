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

        if (contas == null)
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
    [HttpPost("cadastrar-todos")]
    public async Task<IActionResult> PostUsuarioConta([FromBody] UsuarioContaDto usuarioContaDto)
    {
        using var transicao = await _context.Database.BeginTransactionAsync();

        try
        {
            var UsuarioCriarDto = usuarioContaDto.UsuarioCriarDto;
            var contaDto = usuarioContaDto.ContaDto;

            var userExisting = await _context.Usuario.FirstOrDefaultAsync(
                            u => u.Email == UsuarioCriarDto.Email);

            if (userExisting != null)
            {
                return BadRequest("Usuario ja existente");
            }

            var usuario = _mapper.Map<Usuario>(UsuarioCriarDto);
            usuario.CriadoEm = DateTime.UtcNow;
            await _context.AddAsync(usuario);
            var verificaDb = await _context.SaveChangesAsync();

            if (verificaDb == 0)
            {
                return BadRequest("Erro ao cadastrar usuario");
            }

            var contaExisting = await _context.Conta.FirstOrDefaultAsync(
                            c => c.NumeroConta == contaDto.NumeroConta);
            if (contaExisting != null)
            {
                return BadRequest("Numero da conta ja existe");
            }
            var conta = _mapper.Map<Conta>(contaDto);
            conta.CriadoEm = DateTime.UtcNow;
            conta.User_id = usuario.Id;

            _context.Add(conta);
            verificaDb = await _context.SaveChangesAsync();

            if (verificaDb == 0)
            {
                return BadRequest("Erro ao cadastrar conta");
            }
            await transicao.CommitAsync();

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            transicao.Rollback();
            return StatusCode(500, "Erro interno: " + ex.Message);
        }
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


            var conta = _mapper.Map<Conta>(contaDto);
            conta.CriadoEm = DateTime.UtcNow;

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
    public async Task<IActionResult> PutConta([FromBody] ContaEditDto contaDto)
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

        conta.NumeroConta = contaDto.NumeroConta;


        _context.Update(conta);
        var verifica = await _context.SaveChangesAsync();
        if (verifica == 0)
        {
            return BadRequest("Erro ao atualizar conta");
        }

        return Ok(conta);
    }
    [HttpPut("atualizar-todos")]
    public async Task<IActionResult> PutUsuarioConta([FromBody] UsuarioContaEditDto usuarioContaDto)
    {
        using var transicao = await _context.Database.BeginTransactionAsync();

        try
        {
            var UsuarioEditDto = usuarioContaDto.UsuarioEditDto;
            var contaEditDto = usuarioContaDto.ContaEditDto;

            var userExisting = await _context.Usuario.FirstOrDefaultAsync(
                            u => u.Email == UsuarioEditDto.Email);

            if (userExisting == null)
            {
                return BadRequest("Usuario nao existente");
            }

            if (userExisting.Email != UsuarioEditDto.Email)
            {
                var emailEmUso = await _context.Usuario
                    .AnyAsync(u => u.Email == UsuarioEditDto.Email);

                if (emailEmUso)
                {
                    return BadRequest("Email já está em uso.");
                }
            }

            userExisting.Nome = UsuarioEditDto.Nome;
            userExisting.Senha = UsuarioEditDto.Senha;
            userExisting.EditadoEm = DateTime.UtcNow;
            _context.Usuario.Update(userExisting);

            var verificaDb = await _context.SaveChangesAsync();


            var contaExisting = await _context.Conta.FirstOrDefaultAsync(
                            c => c.NumeroConta == contaEditDto.NumeroConta);

            if (contaExisting == null)
            {
                return BadRequest("Conta não existente");
            }

            contaExisting.TipoConta = contaEditDto.TipoConta;
            contaExisting.EditadoEm = DateTime.UtcNow;

            _context.Conta.Update(contaExisting);
            verificaDb = await _context.SaveChangesAsync();


            await transicao.CommitAsync();

            return Ok("Editado com sucesso!");
        }
        catch (Exception ex)
        {
            await transicao.RollbackAsync();
            return StatusCode(500, "Erro interno: " + ex.Message);
        }
    }

    [HttpDelete("delete/{numeroConta}")]
    public async Task<IActionResult> DeleteConta(string numeroConta)
    {
        if (numeroConta == null)
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
    [HttpGet("saldo/usuario/{userId}")]
    public async Task<ActionResult<decimal>> GetSaldoByUserId(int userId) // Ou Guid userId, dependendo do tipo do seu UserId
    {
        var conta = await _context.Conta
                                  .Where(c => c.User_id == userId) // Use User_id aqui
                                  .FirstOrDefaultAsync(); // Pega a primeira conta (ou null se não encontrar)

        if (conta == null) // Se 'conta' é null, significa que não encontrou nenhuma conta para esse usuário
        {
            return NotFound($"Nenhuma conta encontrada para o usuário com ID: {userId}.");
        }

        // Se uma conta foi encontrada, retorne o saldo dela.
        return Ok(conta.Saldo);
    }
}
