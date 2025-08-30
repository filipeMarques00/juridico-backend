using AutoMapper;
using GerenciarProcessos.Application.Dtos;
using GerenciarProcessos.Application.Interfaces;
using GerenciarProcessos.Domain.Entities;
using GerenciarProcessos.Domain.Interfaces;

namespace GerenciarProcessos.Application.Services
{
    public class ProcessoService : IProcessoService
    {
        private readonly IProcessoRepository _processoRepository;
        private readonly IMapper _mapper;

        public ProcessoService(IProcessoRepository processoRepository, IMapper mapper)
        {
            _processoRepository = processoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProcessoDto>> ObterTodosAsync()
        {
            var processos = await _processoRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<ProcessoDto>>(processos);
        }

        public async Task<ProcessoDto?> ObterPorIdAsync(int id)
        {
            var processo = await _processoRepository.ObterPorIdAsync(id);
            return _mapper.Map<ProcessoDto?>(processo);
        }

        public async Task<ProcessoDto> CriarAsync(CriarProcessoDto dto)
        {
            var processo = _mapper.Map<Processo>(dto);
            processo.Status = Domain.Enums.StatusProcesso.Ativo;

            // 🔹 Se houver arquivo, salvar em wwwroot/uploads/processos
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
            var processo = await _processoRepository.ObterPorIdAsync(id);
            if (processo == null)
                throw new Exception("Processo não encontrado.");

            _mapper.Map(dto, processo);

            // 🔹 Atualizar PDF se enviado
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

        public async Task RemoverAsync(int id)
        {
            await _processoRepository.RemoverAsync(id);
        }
    }
}
