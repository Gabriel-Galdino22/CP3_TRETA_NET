using CP3.Domain.Interfaces.Dtos;
using FluentValidation;

namespace CP3.Application.Dtos
{
    public class BarcoDto : IBarcoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Tamanho { get; set; }

        public void Validate()
        {
            new BarcoDtoValidation().ValidateAndThrow(this);
        }
    }

    internal class BarcoDtoValidation : AbstractValidator<BarcoDto>
    {
        public BarcoDtoValidation()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Modelo).NotEmpty().WithMessage("Modelo é obrigatório");
            RuleFor(x => x.Ano).GreaterThan(0).WithMessage("Ano deve ser maior que 0");
            RuleFor(x => x.Tamanho).GreaterThan(0).WithMessage("Tamanho deve ser maior que 0");
        }
    }
}
