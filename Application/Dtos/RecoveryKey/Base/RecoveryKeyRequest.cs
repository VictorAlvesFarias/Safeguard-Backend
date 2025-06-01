using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.RecoveryKey.Base
{
    public class RecoveryKeyRequest
    {
        public string Key { get; set; }
        public string EmailId { get; set; }
    }
}
