using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RealEstateCRM.Application.Features.Leads.Commands.DeleteLead
{
    public class DeleteLeadCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteLeadCommand(int id)
        {
            Id = id;
        }
    }
}

