using Entities.DTO;
using FluentValidation;

namespace Bussines
{
    // Validador para buscar aviso por Id
    public class GetAvisoValidator : AbstractValidator<GetAvisoDto>
    {
        public GetAvisoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id inválido");
        }
    }

    // Validador para criar um aviso
    public class CreateAvisoValidator : AbstractValidator<AvisoDto>
    {
        public CreateAvisoValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("Título é obrigatório");

            RuleFor(x => x.Mensagem)
                .NotEmpty()
                .WithMessage("Mensagem é obrigatória");
        }
    }

    // Validador para atualizar mensagem de um aviso
    public class UpdateAvisoValidator : AbstractValidator<UpdateAvisoDto>
    {
        public UpdateAvisoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id inválido");

            RuleFor(x => x.Mensagem)
                .NotEmpty()
                .WithMessage("Mensagem não pode ser vazia");
        }
    }
}
