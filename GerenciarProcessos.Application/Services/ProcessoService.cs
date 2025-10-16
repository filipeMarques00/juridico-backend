using AutoMapper;
using GerenciarProcessos.Application.Dtos;
using GerenciarProcessos.Application.Interfaces;
using GerenciarProcessos.Domain.Entities;
using GerenciarProcessos.Domain.Interfaces;
using Microsoft.AspNetCore.Http; 
using System.Security.Claims; 

namespace GerenciarProcessos.Application.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly int _usuarioId; 

        public ProcessoService(IProcessoRepository processoRepository, IClienteRepository clienteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _processoRepository = processoRepository;
            _clienteRepository = clienteRepository;
            _mapper = mapper;

            var usuarioIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out _usuarioId))
            {
                throw new Exception("Não foi possível identificar o usuário logado a partir do token.");
            }
        }



        public async Task<ProcessoDto> CriarAsync(CriarProcessoDto dto)
        {
            var clienteDoUsuario = await _clienteRepository.ObterPorIdEUsuarioAsync(dto.ClienteId, _usuarioId);
            if (clienteDoUsuario == null)
            {
                throw new Exception("O cliente informado não existe ou não pertence ao usuário logado.");
            }

            var processo = _mapper.Map<Processo>(dto);
            processo.UsuarioId = _usuarioId; 
            processo.Status = Domain.Enums.StatusProcesso.Ativo;

            if (dto.Arquivo != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "processos");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{dto.Arquivo.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Arquivo.CopyToAsync(stream);
                }

                processo.ArquivoUrl = $"/uploads/processos/{fileName}";
            }

            await _processoRepository.AdicionarAsync(processo);
            return _mapper.Map<ProcessoDto>(processo);
        }

        public async Task AtualizarAsync(int id, CriarProcessoDto dto)
        {
            var processo = await _processoRepository.ObterPorIdEUsuarioAsync(id, _usuarioId);
            if (processo == null)
                throw new Exception("Processo não encontrado ou você não tem permissão para editá-lo.");

            if (processo.ClienteId != dto.ClienteId)
            {
                var novoClienteDoUsuario = await _clienteRepository.ObterPorIdEUsuarioAsync(dto.ClienteId, _usuarioId);
                if (novoClienteDoUsuario == null)
                {
                    throw new Exception("O novo cliente informado não existe ou não pertence ao usuário logado.");
                }
            }
            _mapper.Map(dto, processo);

            if (dto.Arquivo != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "processos");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{dto.Arquivo.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Arquivo.CopyToAsync(stream);
                }

                processo.ArquivoUrl = $"/uploads/processos/{fileName}";
            }

            await _processoRepository.AtualizarAsync(processo);
        }
        public async Task<IEnumerable<ProcessoDto>> ObterTodosAsync()
        {
            var processos = await _processoRepository.ObterTodosPorUsuarioAsync(_usuarioId);
            return _mapper.Map<IEnumerable<ProcessoDto>>(processos);
        }

        public async Task<ProcessoDto?> ObterPorIdAsync(int id)
        {
            var processo = await _processoRepository.ObterPorIdEUsuarioAsync(id, _usuarioId);
            return _mapper.Map<ProcessoDto?>(processo);
        }
        public async Task RemoverAsync(int id)
        {
            var processo = await _processoRepository.ObterPorIdEUsuarioAsync(id, _usuarioId);
            if (processo == null)
                throw new Exception("Processo não encontrado ou você não tem permissão para removê-lo.");

            await _processoRepository.RemoverAsync(id);
        }
    }
}