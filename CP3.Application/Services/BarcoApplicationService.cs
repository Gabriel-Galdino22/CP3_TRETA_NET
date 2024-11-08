using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;

namespace CP3.Application.Services
{
    public class BarcoApplicationService : IBarcoApplicationService
    {
        private readonly IBarcoRepository _repository;

        public BarcoApplicationService(IBarcoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BarcoEntity> ObterTodosBarcos() => _repository.ObterTodos();

        public BarcoEntity ObterBarcoPorId(int id) => _repository.ObterPorId(id);

        public BarcoEntity AdicionarBarco(IBarcoDto entity)
        {
            var barco = new BarcoEntity
            {
                Nome = entity.Nome,
                Modelo = entity.Modelo,
                Ano = entity.Ano,
                Tamanho = entity.Tamanho
            };
            return _repository.Adicionar(barco);
        }

        public BarcoEntity EditarBarco(int id, IBarcoDto entity)
        {
            var barco = _repository.ObterPorId(id);
            if (barco != null)
            {
                barco.Nome = entity.Nome;
                barco.Modelo = entity.Modelo;
                barco.Ano = entity.Ano;
                barco.Tamanho = entity.Tamanho;
                return _repository.Editar(barco);
            }
            return null;
        }

        public BarcoEntity RemoverBarco(int id) => _repository.Remover(id);
    }
}
