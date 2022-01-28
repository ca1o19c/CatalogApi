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
                return NotFound();

            return item.AsDto();
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(ItemRequest itemRequest)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemRequest.Name,
                Price = itemRequest.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreateItem(item);


            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());

        }
    }
}
