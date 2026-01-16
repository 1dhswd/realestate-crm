using FluentValidation;
using RealEstateCRM.Application.DTOs.Lead;

namespace RealEstateCRM.Application.Validators.Lead
{
    public class CreateLeadDtoValidator : AbstractValidator<CreateLeadDto>
    {
        public CreateLeadDtoValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Müşteri seçilmeli");

            RuleFor(x => x.StatusId)
                .GreaterThan(0).WithMessage("Durum seçilmeli");

            RuleFor(x => x.Budget)
                .GreaterThan(0).WithMessage("Bütçe 0'dan büyük olmalı")
                .When(x => x.Budget.HasValue);
        }
    }
}