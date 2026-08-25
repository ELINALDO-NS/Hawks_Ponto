using HP.Core.Entities;
using HP.Core.Extentions;
using HP.Core.Interfaces;
using HP.Manager.DTOs.Pessoa;
using HP.Manager.Interfaces;
using MapsterMapper;


namespace HP.Manager.Implementation
{
    public class PessoaManager(IPessoaRepository _repository, IMapper _mapper) : IPessoaManager
    {
        public async Task<PessoaDto> AdicionarAsync(AdicionaPessoaDto pessoa, CancellationToken cancellationToken)
        {
            var novapessoa = _mapper.Map<Pessoa>(pessoa);

            if (pessoa.Cargo is not null)
            {
                novapessoa.Cargos = new List<CargoPessoa>
                {
                    new()
                    {
                        CargoId = pessoa.Cargo.Id,
                        DataInicio = pessoa.Cargo.DataInicio,
                    }
                };
            }
            if (pessoa.EstruturaOrganizacional is not null)
            {
                novapessoa.EstruturasOrganizacionais = new List<EstruturaOrganizacionalPessoa>
                {
                    new()
                    {
                        EstruturaOrganizacionalId = pessoa.EstruturaOrganizacional.Id,
                        DataInicio = pessoa.EstruturaOrganizacional.DataInicio,
                    }
                };
            }
            if (pessoa.Horario is not null)
            {
                novapessoa.Horarios = new List<HorarioPessoa>
                {
                    new()
                    {
                        HorarioId = pessoa.Horario.Id,
                        DataInicio = pessoa.Horario.DataInicio,
                    }
                };
            }

            await _repository.AdicionarAsync(novapessoa, cancellationToken);
            return _mapper.Map<PessoaDto>(novapessoa);
        }

        public async Task<PessoaDto?> AtualizarAsync(AtualizaPessoaDto pessoa, CancellationToken cancellationToken)
        {
            var pessoadto = _mapper.Map<Pessoa>(pessoa);

            if (pessoa.Cargo is not null)
            {
                pessoadto.Cargos.Add(

                    new CargoPessoa()
                    {
                        CargoId = pessoa.Cargo.Id,
                        DataInicio = pessoa.Cargo.DataInicio,
                    });
            }
            if (pessoa.EstruturaOrganizacional is not null)
            {
                pessoadto.EstruturasOrganizacionais = new List<EstruturaOrganizacionalPessoa>
                {
                    new()
                    {
                        EstruturaOrganizacionalId = pessoa.EstruturaOrganizacional.Id,
                        DataInicio = pessoa.EstruturaOrganizacional.DataInicio,
                    }
                };
            }
            if (pessoa.Horario is not null)
            {
                pessoadto.Horarios = new List<HorarioPessoa>
                {
                    new()
                    {
                        HorarioId = pessoa.Horario.Id,
                        DataInicio = pessoa.Horario.DataInicio,
                    }
                };
            }
            var pessoaatualizada = await _repository.AtualizarAsync(pessoadto, cancellationToken);
            if (pessoaatualizada is not null)
            {
                return _mapper.Map<PessoaDto>(pessoaatualizada);
            }
            return null;
        }
        public async Task<PessoaDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var pessoa = await _repository.ObterPorIdAsync(id, cancellationToken);
            if (pessoa is null)
            {
                return null;
            }
            return _mapper.Map<PessoaDto>(pessoa);
        }

        public async Task<IEnumerable<PessoaDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var pessoas = await _repository.ObterTodosAsync(cancellationToken);

            var pessoasDto = pessoas.Select(x =>
            {
                x.Cpf = x.Cpf.FormatarCPF_CNPJ();
                x.Pis = x.Pis.FormataPis();
                return _mapper.Map<PessoaDto>(x);
            }).ToList();
            return pessoasDto;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var excluido = await _repository.RemoverAsync(id, cancellationToken);
            return excluido;
        }
    }
}
