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
    [Route("api/Instrutores")]
    [ApiController]
    public class InstrutoresController : ControllerBase
    {
        private readonly FelpsAcademiaAPIContext _context;

        public InstrutoresController(FelpsAcademiaAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todos os Instrutores cadastrados.
        /// </summary>
        /// <remarks>
        ///     Retorno:
        ///     
        ///     GETALL
        ///     [
        ///         {  
        ///             "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///             "nome": "João Silva",
        ///             "email": "joao@exemplo.com",
        ///             "telefone": "(61) 99999-9999",
        ///             "especialidade": "Musculação"
        ///         }    
        ///     ]
        /// </remarks>
        /// <response code="200">Retorna uma lista de instrutores cadastrados.</response>
        /// <response code="400">Se ocorrer um erro.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Instrutor>>> GetInstrutor()
        {
            return await _context.Instrutor.ToListAsync();
        }

        /// <summary>
        /// Obter um Instrutor, passando o Id como parâmetro.
        /// </summary>
        /// <param name="id">ID do Instrutor</param>
        /// <returns>Um instrutor específico</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Instrutor>> GetInstrutor(Guid id)
        {
            var instrutor = await _context.Instrutor.FindAsync(id);

            if (instrutor == null)
            {
                return NotFound();
            }

            return instrutor;
        }

        /// <summary>
        /// Atualizar os dados de um Instrutor com base no ID.
        /// </summary>
        /// <param name="id">ID do Instrutor</param>
        /// <param name="instrutor">Dados atualizados do instrutor</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstrutor(Guid id, Instrutor instrutor)
        {
            if (id != instrutor.Id)
            {
                return BadRequest();
            }

            _context.Entry(instrutor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstrutorExists(id))
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
        /// Cadastrar um novo Instrutor.
        /// </summary>
        /// <param name="instrutor">Dados do Instrutor a ser cadastrado</param>
        /// <returns>Retorna um código de status Created (201) com o objeto do Instrutor criado</returns>
        [HttpPost]
        public async Task<ActionResult<Instrutor>> PostInstrutor(Instrutor instrutor)
        {
            _context.Instrutor.Add(instrutor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstrutor", new { id = instrutor.Id }, instrutor);
        }

        /// <summary>
        /// Excluir um Instrutor pelo ID.
        /// </summary>
        /// <param name="id">ID do Instrutor a ser excluído</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstrutor(Guid id)
        {
            var instrutor = await _context.Instrutor.FindAsync(id);
            if (instrutor == null)
            {
                return NotFound();
            }

            _context.Instrutor.Remove(instrutor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstrutorExists(Guid id)
        {
            return _context.Instrutor.Any(e => e.Id == id);
        }
    }
}
