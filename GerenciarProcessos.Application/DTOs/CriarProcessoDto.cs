namespace GerenciarProcessos.Application.Dtos
{
    public class CriarProcessoDto
    {
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public string Vara { get; set; }
        public string Comarca { get; set; }
        public DateTime DataAbertura { get; set; }
        public int ClienteId { get; set; }
    }
}
