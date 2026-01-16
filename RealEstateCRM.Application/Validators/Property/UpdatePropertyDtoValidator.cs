using FluentValidation;
using RealEstateCRM.Application.DTOs.Property;

namespace RealEstateCRM.Application.Validators.Property
{
    public class UpdatePropertyDtoValidator : AbstractValidator<UpdatePropertyDto>
    {
        public UpdatePropertyDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçersiz ID");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz")
                .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalı");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalı");

            RuleFor(x => x.Area)
                .GreaterThan(0).WithMessage("Alan 0'dan büyük olmalı");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir boş olamaz");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("İlçe boş olamaz");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori seçilmeli");

            RuleFor(x => x.TypeId)
                .GreaterThan(0).WithMessage("İlan tipi seçilmeli");
        }
    }
}