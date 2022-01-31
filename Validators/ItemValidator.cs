using CatalogApi.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Validators
{
    public class ItemValidator : AbstractValidator<ItemRequest>
    {   
        public ItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Please specify a name for the item");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("The price must be greater than or equal to 0");
        }
    }
}
