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
    [Route("api/Pagamentos")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly FelpsAcademiaAPIContext _context;

        public PagamentosController(FelpsAcademiaAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todos os pagamentos registrados.
        /// </summary>
        /// <remarks>
        ///     Retorno:
        ///     [
        ///         {
        ///             "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "valor": 150.00,
        ///             "dataPagamento": "2024-12-01T10:00:00",
        ///             "formaPagamento": "Cartão de Crédito",
        ///             "usuarioId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna a lista de pagamentos.</response>
        /// <response code="400">Se ocorrer um erro ao processar a requisição.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamento()
        {
            return await _context.Pagamento.ToListAsync();
        }

        /// <summary>
        /// Obter um pagamento específico por ID.
        /// </summary>
        /// <param name="id">ID do pagamento</param>
        /// <returns>Retorna os detalhes de um pagamento específico</returns>
        /// <response code="200">Retorna o pagamento encontrado.</response>
        /// <response code="404">Se o pagamento não for encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Pagamento>> GetPagamento(Guid id)
        {
            var pagamento = await _context.Pagamento.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound();
            }

            return pagamento;
        }

        /// <summary>
        /// Atualizar os dados de um pagamento existente.
        /// </summary>
        /// <param name="id">ID do pagamento</param>
        /// <param name="pagamento">Dados do pagamento a ser atualizado</param>
        /// <returns>Retorna um código de status NoContent (204) se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagamento(Guid id, Pagamento pagamento)
        {
            if (id != pagamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(pagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagamentoExists(id))
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
        /// Criar um novo pagamento.
        /// </summary>
        /// <param name="pagamento">Dados do pagamento a ser criado</param>
        /// <returns>Retorna um código de status Created (201) com o objeto do pagamento criado</returns>
        [HttpPost]
        public async Task<ActionResult<Pagamento>> PostPagamento(Pagamento pagamento)
        {
            _context.Pagamento.Add(pagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPagamento", new { id = pagamento.Id }, pagamento);
        }

        /// <summary>
        /// Excluir um pagamento existente por ID.
        /// </summary>
        /// <param name="id">ID do pagamento a ser excluído</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamento(Guid id)
        {
            var pagamento = await _context.Pagamento.FindAsync(id);
            if (pagamento == null)
            {
                return NotFound();
            }

            _context.Pagamento.Remove(pagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagamentoExists(Guid id)
        {
            return _context.Pagamento.Any(e => e.Id == id);
        }
    }
}
