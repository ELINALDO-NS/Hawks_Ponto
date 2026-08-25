using HP.Core.Entities;
using HP.Manager.DTOs.Pessoa;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Mappings
{
    public class AdicionaHorarioPessoaMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AdicionaHorarioPessoaDto, HorarioPessoa>()
                                .TwoWays();
        }
    }
}
