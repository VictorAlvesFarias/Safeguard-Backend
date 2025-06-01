using Domain.Entitites;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.RecoveryEmail.Base
{
    public class RecoveryEmailResponse
    {
        public int ParentEmailId { get; set; }
        public int Id { get; set; }
        public AppFile Image { get; set; }
        public string Email { get; set; }
    }
}
