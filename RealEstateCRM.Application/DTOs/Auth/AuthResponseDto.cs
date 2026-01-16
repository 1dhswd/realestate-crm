using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public string[] Roles { get; set; }
    }
}
