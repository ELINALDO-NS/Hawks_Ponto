using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Endereco
{
    public record EnderecoDto(    
    string Cep,
    string Logradouro,
    string Numero,
    string? Complemento,
    string Bairro,
    string Cidade,
    string Uf);
}
