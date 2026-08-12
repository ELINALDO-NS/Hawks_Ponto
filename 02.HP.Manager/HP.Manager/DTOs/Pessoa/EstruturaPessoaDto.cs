using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Pessoa
{
    public record EstruturaPessoaDto(int Id,
    int Codigo,
    string Descricao,
    int? EstruturaPai
    );
}
