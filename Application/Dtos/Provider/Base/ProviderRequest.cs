﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Provider.Base
{

    public class ProviderRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Signature { get; set; }
        public int ImageId { get; set; }
    }
}

