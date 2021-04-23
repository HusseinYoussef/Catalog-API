using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog_API.Models;
using Catalog_API.Repositories;
using Catalog_API.Controllers;
using FluentAssertions;
using Catalog_API.Dtos;
using System.Collections.Generic;

namespace Catalog.Tests
{
    public class ItemsControllerUnitTests
    {
        private readonly Mock<IItemRepository> itemRepositoryStub = new Mock<IItemRepository>();
        private readonly Random rand = new Random();

        [Fact]
        public async Task GetItem_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange
            itemRepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                            .ReturnsAsync((Item)null);
            var controller = new ItemsController(itemRepositoryStub.Object);

            // Act
            var result = await controller.GetItem(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetItem_WithExistingItem_ReturnsFoundItem()
        {
            // Arrange
            var expectedItem = CreateRandomItem();
            itemRepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(expectedItem);
            var controller = new ItemsController(itemRepositoryStub.Object);

            // Act
            var result = await controller.GetItem(Guid.NewGuid());

            // Assert
            var ok_result = Assert.IsType<OkObjectResult>(result.Result);
            var item = Assert.IsType<ItemReadDto>(ok_result.Value);
            item.Should().BeEquivalentTo(expectedItem, options => options.ComparingByMembers<ItemReadDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetItems_WithExistingItems_ReturnsAllItems()
        {
            // Arrange
            var expectedItems = new[] { CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };
            itemRepositoryStub.Setup(repo => repo.GetItemsAsync(It.IsAny<String>()))
                            .ReturnsAsync(expectedItems);
            var controller = new ItemsController(itemRepositoryStub.Object);

            // Act
            var result = await controller.GetItems();

            // Assert
            var ok_result = Assert.IsType<OkObjectResult>(result.Result);
            var items = Assert.IsType<List<ItemReadDto>>(ok_result.Value);
            expectedItems.Should().BeEquivalentTo(items);
        }

        [Fact]
        public async Task GetItems_WithNoItems_ReturnsNotFound()
        {
            // Arrange
            var expectedItems = new List<Item>();
            itemRepositoryStub.Setup(repo => repo.GetItemsAsync(It.IsAny<String>()))
                            .ReturnsAsync(expectedItems);
            var controller = new ItemsController(itemRepositoryStub.Object);

            // Act
            var result = await controller.GetItems();

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateItem_WithItemToCreate_ReturnsCreatedItem()
        {
            // Arrange
            var itemToCreate = new ItemCreateDto()
            {
                Name = Guid.NewGuid().ToString(),
                Price = rand.Next(1000)
            };
            var controller = new ItemsController(itemRepositoryStub.Object);

            // Act
            var result = await controller.CreateItem(itemToCreate);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var item = Assert.IsType<ItemReadDto>(createdResult.Value);
            itemToCreate.Should().BeEquivalentTo(item, options => options.ComparingByMembers<ItemCreateDto>().ExcludingMissingMembers());
            item.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateItem_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange
            var itemToUpdate = new ItemUpdateDto()
            {
                Name = Guid.NewGuid().ToString(),
                Price = rand.Next(1000)
            };
            itemRepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                            .ReturnsAsync((Item)null);
            var controller = new ItemsController(itemRepositoryStub.Object);

            // Act
            var result = await controller.UpdateItem(Guid.NewGuid(), itemToUpdate);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateItem_WithExistingItem_ReturnsNoContent()
        {
            // Arrange
            var item = CreateRandomItem();
            var itemToUpdate = new ItemUpdateDto()
            {
                Name = Guid.NewGuid().ToString(),
                Price = rand.Next(1000)
            };
            itemRepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(item);
            var controller = new ItemsController(itemRepositoryStub.Object);

            // Act
            var result = await controller.UpdateItem(item.Id, itemToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteItem_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange
            itemRepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                            .ReturnsAsync((Item)null);
            var controller = new ItemsController(itemRepositoryStub.Object);

            // Act
            var result = await controller.DeleteItem(Guid.NewGuid());

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteItem_WithExistingItem_ReturnsNoContent()
        {
            // Arrange
            var item = CreateRandomItem();
            itemRepositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(item);
            var controller = new ItemsController(itemRepositoryStub.Object);

            // Act
            var result = await controller.DeleteItem(item.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task GetItems_WithMatchingItems_ReturnsMatchingItems()
        {
            // Arrange
            var items = new[]{
                new Item() {Name = "Potion"},
                new Item() {Name = "Energy"},
                new Item() {Name = "Mega-Potion"}
             };
            var query = "potion";

            itemRepositoryStub.Setup(repo => repo.GetItemsAsync(query))
                            .ReturnsAsync(new[]{items[0], items[2]});
            var controller = new ItemsController(itemRepositoryStub.Object);

            // Act
            var result = await controller.GetItems(query);

            // Assert
            var returnedItems = (result.Result as OkObjectResult).Value as IEnumerable<ItemReadDto>;
            returnedItems.Should().OnlyContain(i => i.Name.ToLower().Contains(query.ToLower()));
        }

        private Item CreateRandomItem()
        {
            return new Item()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Price = rand.Next(1000),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
