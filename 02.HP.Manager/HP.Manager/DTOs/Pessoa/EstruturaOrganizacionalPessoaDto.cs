using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Pessoa
{
    public record EstruturaOrganizacionalPessoaDto(int Id, int Codigo, string Descricao, DateOnly DataInicio, DateOnly? DataFim);
    
}
