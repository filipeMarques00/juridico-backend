using GerenciarProcessos.Application.Dtos;
using GerenciarProcessos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciarProcessos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessosController : ControllerBase
    {
        private readonly IProcessoService _processoService;

        public ProcessosController(IProcessoService processoService)
        {
            _processoService = processoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessoDto>>> ObterTodos()
        {
            var processos = await _processoService.ObterTodosAsync();
            return Ok(processos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessoDto>> ObterPorId(int id)
        {
            var processo = await _processoService.ObterPorIdAsync(id);
            if (processo == null)
                return NotFound("Processo não encontrado.");
            return Ok(processo);
        }

        [HttpPost]
        public async Task<ActionResult<ProcessoDto>> Criar([FromForm] CriarProcessoDto dto)
        {
            var novoProcesso = await _processoService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoProcesso.Id }, novoProcesso);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromForm] CriarProcessoDto dto)
        {
            try
            {
                await _processoService.AtualizarAsync(id, dto);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            await _processoService.RemoverAsync(id);
            return NoContent();
        }
    }
}
