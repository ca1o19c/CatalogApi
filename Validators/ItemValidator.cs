using CatalogApi.Dto;
using CatalogApi.Enumerator;
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
                .WithMessage(ResponseMessages.ITEM_NAME_INVALID.GetEnumDescription());

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage(ResponseMessages.PRICE_LENGTH_INVALID.GetEnumDescription());
        }
    }
}
