using CatalogApi.Dto;
using CatalogApi.Entities;
using CatalogApi.Repositories;
using CatalogApi.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Controllers
{
    [ApiController]
    [Route("catalog-api/v1/items")]
    public class ItemsController : ControllerBase
    {

        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync())
                .Select(item => item.AsDto());

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item is null)
            {
                var errors = new List<string>
                {
                    "Item not found"
                };

                return NotFound(new ResultData(false, errors));
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult> CreateItemAsync(ItemRequest itemRequest)
        {
            var validateResult = new ItemValidator().Validate(itemRequest);

            if (!validateResult.IsValid)
                return BadRequest(new ResultData(false, validateResult.Errors.ConvertErrorMessageList()));


            var item = itemRequest.AsEntity();

            
            await repository.CreateItemAsync(item);

            return Ok(new ResultData(true, new List<string>()));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, ItemRequest itemRequest)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null)
            {
                var errors = new List<string>
                {
                    "Item not found"
                };

                return NotFound(new ResultData(false, errors));
            }

            var validateResult = new ItemValidator().Validate(itemRequest);

            if (!validateResult.IsValid)
                return BadRequest(new ResultData(false, validateResult.Errors.ConvertErrorMessageList()));

            var updatedItem = existingItem with
            {
                Name = itemRequest.Name,
                Price = itemRequest.Price
            };

            await repository .UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null)
            {
                var errors = new List<string>
                {
                    "Item not found"
                };

                return NotFound(new ResultData(false, errors));
            }

            await repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}
