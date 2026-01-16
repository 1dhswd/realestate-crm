using FluentValidation;
using RealEstateCRM.Application.DTOs.Customer;

namespace RealEstateCRM.Application.Validators.Customer
{
    public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
    {
        public CreateCustomerDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir email adresi girin")
                .MaximumLength(100).WithMessage("Email en fazla 100 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz")
                .MaximumLength(20).WithMessage("Telefon numarası en fazla 20 karakter olabilir")
                .Matches(@"^[\d\s\+\-\(\)]+$").WithMessage("Geçerli bir telefon numarası girin");

            RuleFor(x => x.Source)
                .MaximumLength(50).WithMessage("Kaynak en fazla 50 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.Source));
        }
    }
}