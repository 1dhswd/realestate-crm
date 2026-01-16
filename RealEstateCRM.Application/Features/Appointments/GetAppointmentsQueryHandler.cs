using AutoMapper;
using MediatR;
using RealEstateCRM.Application.DTOs.Appointment;
using RealEstateCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Features.Appointments
{
    public class GetAppointmentsQueryHandler
    : IRequestHandler<GetAppointmentsQuery, List<AppointmentDto>>
    {
        private readonly IAppointmentRepository _repo;
        private readonly IMapper _mapper;

        public GetAppointmentsQueryHandler(
            IAppointmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<AppointmentDto>> Handle(
            GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Appointment> appointments;

            if (request.Start.HasValue && request.End.HasValue)
            {
                appointments = await _repo
                    .GetAppointmentsByDateRange(request.Start.Value, request.End.Value);
            }
            else
            {
                appointments = await _repo.GetAppointmentsWithDetailsAsync();
            }

            return _mapper.Map<List<AppointmentDto>>(appointments);
        }
    }

}
