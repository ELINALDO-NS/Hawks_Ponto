using HP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Interfaces
{
    public interface ICalculaAtrasosExtraFaltasManager
    {
        Task CalculaDias(DateTimeOffset dataInicio, DateTimeOffset dataFim, Pessoa pessoa, CancellationToken cancellationToken);
    }
}
