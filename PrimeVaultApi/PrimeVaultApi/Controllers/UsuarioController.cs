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
using AutoMapper;

namespace PrimeVaultApi.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UsuariosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("todos")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.Usuario.ToListAsync();

            if (!usuarios.Any())
            {
                return NoContent(); 
            }

            return Ok(usuarios);
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioLerDto>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioCriarDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoUsuario = _mapper.Map<Usuario>(createDto);

            novoUsuario.CriadoEm = DateTime.UtcNow;
            novoUsuario.EditadoEm = DateTime.UtcNow;
            _context.Usuario.Add(novoUsuario);
            await _context.SaveChangesAsync();


            return Ok(createDto);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> UpdateUsuario(UsuarioLerDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == updateDto.Email);
            if (usuario == null)
                return NotFound();

            // Atualiza somente os campos permitidos
            usuario.Nome = updateDto.Nome;
            usuario.Email = updateDto.Email;
            

            _context.Usuario.Update(usuario);

            await _context.SaveChangesAsync();

            return Ok(usuario);
        }

        [HttpDelete("delete/{Email}")]
        public async Task<ActionResult> DeleteUsuario(string email)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
                return NotFound();

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    
}
