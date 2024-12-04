using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FelpsAcademia.API.Data;
using FelpsAcademia.API.Entities;

namespace FelpsAcademia.API.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly FelpsAcademiaAPIContext _context;

        public UsuariosController(FelpsAcademiaAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todos os usuários registrados.
        /// </summary>
        /// <remarks>
        /// Retorna a lista completa de usuários registrados na academia.
        /// </remarks>
        /// <response code="200">Retorna a lista de usuários.</response>
        /// <response code="400">Se ocorrer um erro ao processar a requisição.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await _context.Usuario.ToListAsync();
        }

        /// <summary>
        /// Obter um usuário específico por ID.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Retorna os detalhes de um usuário específico</returns>
        /// <response code="200">Retorna o usuário encontrado.</response>
        /// <response code="404">Se o usuário não for encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        /// <summary>
        /// Atualizar os dados de um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="usuario">Dados do usuário a ser atualizado</param>
        /// <returns>Retorna um código de status NoContent (204) se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(Guid id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Criar um novo usuário.
        /// </summary>
        /// <param name="usuario">Dados do usuário a ser criado</param>
        /// <returns>Retorna um código de status Created (201) com o objeto do usuário criado</returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        /// <summary>
        /// Excluir um usuário existente por ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser excluído</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
