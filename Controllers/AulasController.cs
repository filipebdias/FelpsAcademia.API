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
    [Route("api/Aulas")]
    [ApiController]
    public class AulasController : ControllerBase
    {
        private readonly FelpsAcademiaAPIContext _context;

        public AulasController(FelpsAcademiaAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todas as Aulas cadastradas.
        /// </summary>
        /// <remarks>
        /// Retorna um IEnumerable de aulas.
        /// Exemplo de resposta:
        /// 
        /// GET /api/Aulas
        /// [
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "nome": "Aula de Musculação",
        ///         "descricao": "Aula focada no aumento de força.",
        ///         "horaInicio": "2024-12-05T10:00:00",
        ///         "horaFim": "2024-12-05T11:00:00"
        ///     },
        ///     {
        ///         "id": "b8a885f6-b561-433e-8fba-4b21385a8d29",
        ///         "nome": "Aula de Yoga",
        ///         "descricao": "Aula focada em alongamentos e relaxamento.",
        ///         "horaInicio": "2024-12-06T08:00:00",
        ///         "horaFim": "2024-12-06T09:00:00"
        ///     }
        /// ]
        /// </remarks>
        /// <response code="200">Retorna uma lista de todas as aulas cadastradas.</response>
        /// <response code="400">Se ocorrer um erro.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAula()
        {
            var aulas = await _context.Aula.ToListAsync();
            return Ok(aulas);
        }

        /// <summary>
        /// Obter uma Aula específica pelo ID.
        /// </summary>
        /// <param name="id">ID da Aula.</param>
        /// <returns>Retorna uma aula se encontrada.</returns>
        /// <response code="200">Retorna a aula encontrada.</response>
        /// <response code="404">Se a aula não for encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAula(Guid id)
        {
            var aula = await _context.Aula.FindAsync(id);

            if (aula == null)
            {
                return NotFound();
            }

            return Ok(aula);
        }

        /// <summary>
        /// Atualizar uma Aula existente.
        /// </summary>
        /// <param name="id">ID da Aula.</param>
        /// <param name="aula">Objeto Aula com os dados atualizados.</param>
        /// <returns>Retorna uma resposta sem conteúdo.</returns>
        /// <response code="204">Aula atualizada com sucesso.</response>
        /// <response code="400">Se o ID não coincidir com o da aula.</response>
        /// <response code="404">Se a aula não for encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAula(Guid id, Aula aula)
        {
            if (id != aula.Id)
            {
                return BadRequest();
            }

            _context.Entry(aula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AulaExists(id))
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
        /// Criar uma nova Aula.
        /// </summary>
        /// <param name="aula">Dados da nova aula.</param>
        /// <returns>Retorna a Aula criada.</returns>
        /// <response code="201">Aula criada com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAula(Aula aula)
        {
            _context.Aula.Add(aula);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAula", new { id = aula.Id }, aula);
        }

        /// <summary>
        /// Excluir uma Aula pelo ID.
        /// </summary>
        /// <param name="id">ID da Aula a ser excluída.</param>
        /// <returns>Retorna uma resposta sem conteúdo.</returns>
        /// <response code="204">Aula excluída com sucesso.</response>
        /// <response code="404">Se a aula não for encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAula(Guid id)
        {
            var aula = await _context.Aula.FindAsync(id);
            if (aula == null)
            {
                return NotFound();
            }

            _context.Aula.Remove(aula);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AulaExists(Guid id)
        {
            return _context.Aula.Any(e => e.Id == id);
        }
    }
}
