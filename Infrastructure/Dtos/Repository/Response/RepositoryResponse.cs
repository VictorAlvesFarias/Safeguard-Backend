using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos.Repository.Response
{
    public class RepositoryResponse<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; }
    }
}
