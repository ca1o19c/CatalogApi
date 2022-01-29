using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Dto
{
    public record ItemRequest
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
    }
}
