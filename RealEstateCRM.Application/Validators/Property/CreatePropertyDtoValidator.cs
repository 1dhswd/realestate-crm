using FluentValidation;
using RealEstateCRM.Application.DTOs.Property;

namespace RealEstateCRM.Application.Validators.Property
{
    public class CreatePropertyDtoValidator : AbstractValidator<CreatePropertyDto>
    {
        public CreatePropertyDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş olamaz")
                .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz")
                .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalı");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalı")
                .LessThanOrEqualTo(999999999).WithMessage("Fiyat çok yüksek");

            RuleFor(x => x.Area)
                .GreaterThan(0).WithMessage("Alan 0'dan büyük olmalı")
                .LessThanOrEqualTo(100000).WithMessage("Alan çok büyük");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir boş olamaz")
                .MaximumLength(100).WithMessage("Şehir adı en fazla 100 karakter olabilir");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("İlçe boş olamaz")
                .MaximumLength(100).WithMessage("İlçe adı en fazla 100 karakter olabilir");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori seçilmeli");

            RuleFor(x => x.TypeId)
                .GreaterThan(0).WithMessage("İlan tipi seçilmeli");

            RuleFor(x => x.OwnerId)
                .GreaterThan(0).WithMessage("Geçersiz kullanıcı");

            When(x => x.RoomCount.HasValue, () =>
            {
                RuleFor(x => x.RoomCount.Value)
                    .InclusiveBetween(0, 50).WithMessage("Oda sayısı 0-50 arasında olmalı");
            });

            When(x => x.BathroomCount.HasValue, () =>
            {
                RuleFor(x => x.BathroomCount.Value)
                    .InclusiveBetween(0, 20).WithMessage("Banyo sayısı 0-20 arasında olmalı");
            });

            When(x => x.Latitude.HasValue, () =>
            {
                RuleFor(x => x.Latitude.Value)
                    .InclusiveBetween(-90, 90).WithMessage("Geçersiz enlem");
            });

            When(x => x.Longitude.HasValue, () =>
            {
                RuleFor(x => x.Longitude.Value)
                    .InclusiveBetween(-180, 180).WithMessage("Geçersiz boylam");
            });
        }
    }
}