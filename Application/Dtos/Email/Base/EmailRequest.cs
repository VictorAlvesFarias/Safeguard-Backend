﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Provider.Base
{

    public class EmailRequest
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public int Phone { get; set; }
        public string Password { get; set; }
        public int ProviderId { get; set; }   
    }
}
