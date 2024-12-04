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
    [Route("api/Treinos")]
    [ApiController]
    public class TreinosController : ControllerBase
    {
        private readonly FelpsAcademiaAPIContext _context;

        public TreinosController(FelpsAcademiaAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todos os treinos registrados.
        /// </summary>
        /// <remarks>
        /// Retorna a lista completa de treinos registrados na academia.
        /// </remarks>
        /// <response code="200">Retorna a lista de treinos.</response>
        /// <response code="400">Se ocorrer um erro ao processar a requisição.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Treino>>> GetTreino()
        {
            return await _context.Treino.ToListAsync();
        }

        /// <summary>
        /// Obter um treino específico por ID.
        /// </summary>
        /// <param name="id">ID do treino</param>
        /// <returns>Retorna os detalhes de um treino específico</returns>
        /// <response code="200">Retorna o treino encontrado.</response>
        /// <response code="404">Se o treino não for encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Treino>> GetTreino(Guid id)
        {
            var treino = await _context.Treino.FindAsync(id);

            if (treino == null)
            {
                return NotFound();
            }

            return treino;
        }

        /// <summary>
        /// Atualizar os dados de um treino existente.
        /// </summary>
        /// <param name="id">ID do treino</param>
        /// <param name="treino">Dados do treino a ser atualizado</param>
        /// <returns>Retorna um código de status NoContent (204) se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTreino(Guid id, Treino treino)
        {
            if (id != treino.Id)
            {
                return BadRequest();
            }

            _context.Entry(treino).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreinoExists(id))
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
        /// Criar um novo treino.
        /// </summary>
        /// <param name="treino">Dados do treino a ser criado</param>
        /// <returns>Retorna um código de status Created (201) com o objeto do treino criado</returns>
        [HttpPost]
        public async Task<ActionResult<Treino>> PostTreino(Treino treino)
        {
            _context.Treino.Add(treino);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTreino", new { id = treino.Id }, treino);
        }

        /// <summary>
        /// Excluir um treino existente por ID.
        /// </summary>
        /// <param name="id">ID do treino a ser excluído</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreino(Guid id)
        {
            var treino = await _context.Treino.FindAsync(id);
            if (treino == null)
            {
                return NotFound();
            }

            _context.Treino.Remove(treino);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TreinoExists(Guid id)
        {
            return _context.Treino.Any(e => e.Id == id);
        }
    }
}
