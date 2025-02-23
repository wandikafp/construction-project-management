using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generate ID
        public string Email { get; set; } = string.Empty;
    }
}
