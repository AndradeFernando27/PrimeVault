using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeVaultApi.DTOs;
using PrimeVaultApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using PrimeVaultApi.Db;

namespace PrimeVaultApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usuarios = await _context.Usuario.ToListAsync();

            var usuariosDto = usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome ?? string.Empty,
                Email = u.Email,
                dataCriacao = u.CriadoEm
            }).ToList();

            return Ok(usuariosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();

            var dto = new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome ?? string.Empty,
                Email = usuario.Email,
                dataCriacao = usuario.CriadoEm
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> CreateUsuario([FromBody] UsuarioCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoUsuario = new Usuario
            {
                Nome = createDto.Nome,
                Email = createDto.Email,
                Senha = createDto.Senha,
                CriadoEm = DateTime.Now
            };

            _context.Usuario.Add(novoUsuario);
            await _context.SaveChangesAsync();

            var usuarioDto = new UsuarioDto
            {
                Id = novoUsuario.Id,
                Nome = novoUsuario.Nome ?? string.Empty,
                Email = novoUsuario.Email,
                dataCriacao = novoUsuario.CriadoEm
            };

            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioDto.Id }, usuarioDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUsuario(int id, [FromBody] UsuarioDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();

            // Atualiza somente os campos permitidos
            usuario.Nome = updateDto.Nome;
            usuario.Email = updateDto.Email;

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    public class UsuarioCreateDto
    {
        [Required(ErrorMessage = "Obrigatório informar seu nome")]
        [MaxLength(100, ErrorMessage = "Informe um nome com menos de 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; } = string.Empty;
    }
}
