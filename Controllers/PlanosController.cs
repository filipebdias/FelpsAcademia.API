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
    [Route("api/Planos")]
    [ApiController]
    public class PlanosController : ControllerBase
    {
        private readonly FelpsAcademiaAPIContext _context;

        public PlanosController(FelpsAcademiaAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todos os planos registrados.
        /// </summary>
        /// <remarks>
        ///     Retorno:
        ///     [
        ///         {
        ///             "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "nome": "Plano Ouro",
        ///             "valor": 250.00,
        ///             "descricao": "Plano com acesso a todos os serviços.",
        ///             "periodo": "Mensal"
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna a lista de planos.</response>
        /// <response code="400">Se ocorrer um erro ao processar a requisição.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Plano>>> GetPlano()
        {
            return await _context.Plano.ToListAsync();
        }

        /// <summary>
        /// Obter um plano específico por ID.
        /// </summary>
        /// <param name="id">ID do plano</param>
        /// <returns>Retorna os detalhes de um plano específico</returns>
        /// <response code="200">Retorna o plano encontrado.</response>
        /// <response code="404">Se o plano não for encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Plano>> GetPlano(Guid id)
        {
            var plano = await _context.Plano.FindAsync(id);

            if (plano == null)
            {
                return NotFound();
            }

            return plano;
        }

        /// <summary>
        /// Atualizar os dados de um plano existente.
        /// </summary>
        /// <param name="id">ID do plano</param>
        /// <param name="plano">Dados do plano a ser atualizado</param>
        /// <returns>Retorna um código de status NoContent (204) se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlano(Guid id, Plano plano)
        {
            if (id != plano.Id)
            {
                return BadRequest();
            }

            _context.Entry(plano).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanoExists(id))
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
        /// Criar um novo plano.
        /// </summary>
        /// <param name="plano">Dados do plano a ser criado</param>
        /// <returns>Retorna um código de status Created (201) com o objeto do plano criado</returns>
        [HttpPost]
        public async Task<ActionResult<Plano>> PostPlano(Plano plano)
        {
            _context.Plano.Add(plano);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlano", new { id = plano.Id }, plano);
        }

        /// <summary>
        /// Excluir um plano existente por ID.
        /// </summary>
        /// <param name="id">ID do plano a ser excluído</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlano(Guid id)
        {
            var plano = await _context.Plano.FindAsync(id);
            if (plano == null)
            {
                return NotFound();
            }

            _context.Plano.Remove(plano);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanoExists(Guid id)
        {
            return _context.Plano.Any(e => e.Id == id);
        }
    }
}
