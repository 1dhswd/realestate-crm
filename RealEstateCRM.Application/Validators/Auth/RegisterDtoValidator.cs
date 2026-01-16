using FluentValidation;
using RealEstateCRM.Application.DTOs.Auth;

namespace RealEstateCRM.Application.Validators.Auth
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş olamaz")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş olamaz")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir email adresi girin")
                .MaximumLength(100).WithMessage("Email en fazla 100 karakter olabilir");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı")
                .Matches(@"[A-Z]").WithMessage("Şifre en az bir büyük harf içermeli")
                .Matches(@"[a-z]").WithMessage("Şifre en az bir küçük harf içermeli")
                .Matches(@"[0-9]").WithMessage("Şifre en az bir rakam içermeli");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).WithMessage("Telefon numarası en fazla 20 karakter olabilir")
                .Matches(@"^[\d\s\+\-\(\)]+$").WithMessage("Geçerli bir telefon numarası girin")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}