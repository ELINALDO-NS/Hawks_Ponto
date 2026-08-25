using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Pessoa
{
    public record HorarioPessoaDto(int Id, string Codigo, string Descricao, DateOnly DataInicio, DateOnly? DataFim);
    
}
