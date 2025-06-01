using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.RecoveryEmail.Base
{
    public class RecoveryEmailRequest
    {
        public int ParentEmailId { get; set; }
        public int EmailId { get; set; }
    }
}
