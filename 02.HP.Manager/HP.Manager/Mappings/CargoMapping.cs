using HP.Core.Entities;
using HP.Manager.DTOs.Cargo;
using HP.Manager.DTOs.EstruturaOrganizacional;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Mappings
{
    public class CargoMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CargoDto, Cargo>().TwoWays();
            config.NewConfig<AdicionaCargoDto, Cargo>().TwoWays();
            config.NewConfig<AtualizaCargoDto, Cargo>().TwoWays();
        }
    }
}
