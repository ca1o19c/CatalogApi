using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Dto
{
    public record ItemRequest
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public decimal Price { get; init; }
    }
}
