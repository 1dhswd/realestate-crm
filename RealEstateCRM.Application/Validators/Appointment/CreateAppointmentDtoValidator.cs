using FluentValidation;
using RealEstateCRM.Application.DTOs.Appointment;
using System;

namespace RealEstateCRM.Application.Validators.Appointment
{
    public class AppointmentDtoValidator : AbstractValidator<AppointmentDto>
    {
        public AppointmentDtoValidator()
        {
            RuleFor(x => x.AppointmentDate)
                .NotEmpty().WithMessage("Randevu tarihi boş olamaz")
                .Must(d => d > DateTime.Now).WithMessage("Randevu tarihi geçmiş olamaz");

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("Süre 0'dan büyük olmalı");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Lokasyon boş olamaz")
                .MaximumLength(100).WithMessage("Lokasyon en fazla 100 karakter olabilir");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Not en fazla 500 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.Notes));

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Geçersiz durum seçildi");

            RuleFor(x => x.LeadName)
                .NotEmpty().WithMessage("Lead seçilmeli");

            RuleFor(x => x.AgentName)
                .NotEmpty().WithMessage("Agent seçilmeli");

            RuleFor(x => x.PropertyTitle)
                .NotEmpty().WithMessage("Property seçilmeli");
        }
    }
}
