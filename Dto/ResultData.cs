using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Dto
{
    public class ResultData
    {
        public ResultData(bool success, List<string> errors)
        {
            Success = success;
            Errors = errors;
        }

        public bool Success { get; }
        public List<string> Errors { get; }
    }
}
