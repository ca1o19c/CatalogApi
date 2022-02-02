using CatalogApi.Dto;
using CatalogApi.Entities;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogApi
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }

        public static Item AsEntity(this ItemRequest itemRequest)
        {
            return new Item
            {
                Id = Guid.NewGuid(),
                Name = itemRequest.Name,
                Price = itemRequest.Price,
                CreatedDate = DateTimeOffset.Now
            };
        }

        public static List<String> ConvertErrorMessageList (this List<ValidationFailure> list)
        {
            var errors = new List<string>();

            foreach (var item in list)
            {
                errors.Add(item.ToString());
            }
            return errors;
        }
    }
}
