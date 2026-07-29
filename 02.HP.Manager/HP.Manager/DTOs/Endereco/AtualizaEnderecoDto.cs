using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Endereco
{
    public record AtualizaEnderecoDto(int Id,
    string Cep,
    string Logradouro,
    string Numero,
    string? Complemento,
    string Bairro,
    string Cidade,
    string Uf);
}
