using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Default
{

    public class BaseResponse : BaseResponse<string?>
    {
        public BaseResponse(bool success = false)
        {
            Success = success;
            Data = null;

        }
    }
}
