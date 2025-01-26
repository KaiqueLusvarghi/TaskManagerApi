using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOS;
using TaskManagerApi.Models;


namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly TaskManagerContext _context;

        public TarefasController(TaskManagerContext context)
        {
            _context = context;
        }


        // GET: api/Tarefas/5
        // GET: api/Tarefas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetTarefas(int pagina = 1, int tamanhoPagina = 10, string status = null, int? categoriaId = null)
        {
            // Filtro dinâmico com status e categoria
            var query = _context.Tarefas.AsQueryable();  // Tornando a consulta mais dinâmica

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(t => t.Status == status);
            }

            if (categoriaId.HasValue)
            {
                query = query.Where(t => t.CategoriaId == categoriaId.Value);
            }

            // Projeção para DTO e paginação
            var tarefas = await query
                .AsNoTracking() // Desativa o rastreamento de mudanças, otimiza a leitura
                .Skip((pagina - 1) * tamanhoPagina)  // Pular registros conforme a página
                .Take(tamanhoPagina)  // Limitar ao número de itens por página
                .Select(t => new TarefaDTO
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao ?? string.Empty,
                    DataCriacao = t.DataCriacao,
                    PrazoConclusao = t.PrazoConclusao,
                    Status = t.Status,
                    CategoriaId = t.CategoriaId,
                    Categoria = new CategoriaDTO  // Inclui a projeção da Categoria
                    {
                        Id = t.Categoria.Id,
                        Nome = t.Categoria.Nome
                    }
                })
                .ToListAsync();

            return Ok(tarefas);
        }


        // GET: api/Tarefas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TarefaDTO>> GetTarefa(int id)
        {
            var tarefa = await _context.Tarefas
                .AsNoTracking()  // Usando AsNoTracking para otimizar
                .Where(t => t.Id == id)
                .Select(t => new TarefaDTO
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao ?? string.Empty,
                    DataCriacao = t.DataCriacao,
                    PrazoConclusao = t.PrazoConclusao,
                    Status = t.Status,
                    CategoriaId = t.CategoriaId,
                    Categoria = new CategoriaDTO  // Inclui a projeção da Categoria
                    {
                        Id = t.Categoria.Id,
                        Nome = t.Categoria.Nome
                    }
                })
                .FirstOrDefaultAsync();

            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;
        }



        // PUT: api/Tarefas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, TarefaDTO tarefaDto)
        {
            if (id != tarefaDto.Id)
            {
                return BadRequest("O ID da tarefa na URL não corresponde ao ID no corpo da requisição.");
            }

            var tarefaExistente = await _context.Tarefas.FindAsync(id);
            if (tarefaExistente == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            tarefaExistente.Titulo = tarefaDto.Titulo;
            tarefaExistente.Descricao = tarefaDto.Descricao;
            tarefaExistente.DataCriacao = tarefaDto.DataCriacao;
            tarefaExistente.PrazoConclusao = tarefaDto.PrazoConclusao;
            tarefaExistente.Status = tarefaDto.Status;
            tarefaExistente.CategoriaId = tarefaDto.CategoriaId;

            _context.Entry(tarefaExistente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(id))
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

        // POST: api/Tarefas
        [HttpPost]
        public async Task<ActionResult<TarefaDTO>> PostTarefa(TarefaDTO tarefaDto)
        {
            var categoria = await _context.Categorias.FindAsync(tarefaDto.CategoriaId);
            if (categoria == null)
            {
                return BadRequest("Categoria inválida.");
            }

            var tarefa = new Tarefa
            {
                Titulo = tarefaDto.Titulo,
                Descricao = tarefaDto.Descricao,
                DataCriacao = tarefaDto.DataCriacao,
                PrazoConclusao = tarefaDto.PrazoConclusao,
                Status = tarefaDto.Status,
                CategoriaId = tarefaDto.CategoriaId
            };

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            var tarefaResponseDto = new TarefaDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                DataCriacao = tarefa.DataCriacao,
                PrazoConclusao = tarefa.PrazoConclusao,
                Status = tarefa.Status,
                CategoriaId = tarefa.CategoriaId
            };

            return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefaResponseDto);
        }

        // DELETE: api/Tarefas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefas.Any(e => e.Id == id);
        }
    }
}
