using FluentValidation;
using RealEstateCRM.Application.DTOs.Auth;

namespace RealEstateCRM.Application.Validators.Auth
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username boş olamaz")
                .MinimumLength(3).WithMessage("Username en az 3 karakter olmalı");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı");
        }
    }
}