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
                CreatedDate = DateTimeOffset.UtcNow
            };
        }

        public static string GetEnumDescription<T> (this T enumValue)
        where T : struct, IConvertible
        {
            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
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
