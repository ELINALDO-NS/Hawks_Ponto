using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.DTOs.Marcacao
{
    public record ObterPorCpfEPeriodoDto(string cpf, DateTimeOffset inicio, DateTimeOffset fim);

}
