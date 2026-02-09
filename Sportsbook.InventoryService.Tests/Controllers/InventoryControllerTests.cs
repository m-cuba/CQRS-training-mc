using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Sportsbook.InventoryService.API.Controllers;
using Sportsbook.InventoryService.API.Request;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.CreateItem;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.GetBySku;
using Sportsbook.InventoryService.Application.Features.InventoryItemContext.RemoveStock;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using Sportsbook.InventoryService.Application.Seedwork.Responses;
using Sportsbook.InventoryService.Core.InventoryContext;

namespace Sportsbook.InventoryService.API.Tests.Controllers
{
    [TestFixture]
    public class InventoryControllerTests
    {
        private InventoryController _controller;

        private ICreateItemCommandHandler _createItemHandler;
        private IAddStockCommandHandler _addStockHandler;
        private IRemoveStockCommandHandler _removeStockHandler;
        private IGetItemBySkuQueryHandler _getBySkuHandler;
        private IGetAllItemsQueryHandler _getAllHandler;

        [SetUp]
        public void Setup()
        {
            _createItemHandler = A.Fake<ICreateItemCommandHandler>();
            _addStockHandler = A.Fake<IAddStockCommandHandler>();
            _removeStockHandler = A.Fake<IRemoveStockCommandHandler>();
            _getBySkuHandler = A.Fake<IGetItemBySkuQueryHandler>();
            _getAllHandler = A.Fake<IGetAllItemsQueryHandler>();

            _controller = new InventoryController(
                _createItemHandler,
                _addStockHandler,
                _removeStockHandler,
                _getBySkuHandler,
                _getAllHandler);
        }

        [Test]
        public async Task CreateItem_ReturnsCreated()
        {
            var request = new CreateItemRequest("SKU-1", "Item 1");

            var result = await _controller.CreateItem(request, CancellationToken.None);

            Assert.That(result, Is.TypeOf<CreatedAtActionResult>());

            A.CallTo(() => _createItemHandler.Handle(
                A<CreateItemCommand>.That.Matches(c =>
                    c.Sku == "SKU-1" &&
                    c.Name == "Item 1"), CancellationToken.None))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task RemoveStock_WhenInsufficientStock_ReturnsConflict()
        {
            // Arrange
            var request = new RemoveStockRequest(10);

            A.CallTo(() => _removeStockHandler.Handle(
                    A<RemoveStockCommand>.That.Matches(c =>
                        c.Sku == "SKU-1" &&
                        c.Quantity == 10), CancellationToken.None))
                .Throws(new InsufficientStockException(new Sku("SKU-1")));

            // Act
            var result = await _controller.RemoveStock("SKU-1", request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.TypeOf<ConflictObjectResult>());
        }

        [Test]
        public async Task GetBySku_WhenNotFound_ReturnsNotFound()
        {
            A.CallTo(() => _getBySkuHandler.Handle(A<GetBySkuQuery>._, CancellationToken.None))
                .Returns((InventoryItemResponse?)null);

            var result = await _controller.GetBySku("SKU-X", CancellationToken.None);

            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task GetAll_ReturnsOk()
        {
            var items = new List<InventoryItemResponse>
            {
                new("SKU-1", "Item 1", 10)
            };

            A.CallTo(() => _getAllHandler.Handle(A<CancellationToken>._))
                .Returns(items);

            var result = await _controller.GetAll(CancellationToken.None);

            var okResult = result.Result as OkObjectResult;

            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.Value, Is.EqualTo(items));
        }
    }
}
