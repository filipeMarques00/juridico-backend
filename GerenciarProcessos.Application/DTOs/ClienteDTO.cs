using GerenciarProcessos.Domain.Enums;

namespace GerenciarProcessos.Application.DTOs;

public class ClienteDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;

    public TipoPessoa TipoPessoa { get; set; }
    public DateTime? DataNascimento { get; set; }
    public Sexo? Sexo { get; set; }
    public string Nacionalidade { get; set; } = string.Empty;

    public string CEP { get; set; } = string.Empty;
    public string Logradouro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Pais { get; set; } = "Brasil";
}
