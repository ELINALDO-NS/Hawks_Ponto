
namespace HP.Manager.DTOs.Endereco
{
    public record AdicionaEnderecoDto(
    string Cep,
    string Logradouro,
    string Numero,
    string? Complemento,
    string Bairro,
    string Cidade,
    string Uf);
    
}
