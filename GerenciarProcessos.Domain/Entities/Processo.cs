using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciarProcessos.Domain.Enums;

namespace GerenciarProcessos.Domain.Entities
{
    public class Processo
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public StatusProcesso Status { get; set; }
        public string Vara { get; set; } = string.Empty;
        public string Comarca { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public string? ArquivoUrl { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
