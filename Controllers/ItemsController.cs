using CatalogApi.Dto;
using CatalogApi.Entities;
using CatalogApi.Repositories;
using CatalogApi.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());

            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

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
        public ActionResult CreateItem(ItemRequest itemRequest)
        {
            var validateResult = new ItemValidator().Validate(itemRequest);

            if (!validateResult.IsValid)
                return BadRequest(new ResultData(false, validateResult.Errors.ConvertErrorMessageList()));


            var item = itemRequest.AsEntity();


            repository.CreateItem(item);

            return Ok(new ResultData(true, new List<string>()));
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, ItemRequest itemRequest)
        {
            var existingItem = repository.GetItem(id);

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

            repository.UpdateItem(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                var errors = new List<string>
                {
                    "Item not found"
                };

                return NotFound(new ResultData(false, errors));
            }

            repository.DeleteItem(id);

            return NoContent();
        }
    }
}
