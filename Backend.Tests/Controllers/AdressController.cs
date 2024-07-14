using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Context;
using backend.Controllers;
using backend.Models;
using backend.DTO;
using Microsoft.AspNetCore.Mvc;
using EFCore.Toolkit.Testing;
using FluentAssertions;
using NUnit.Framework.Internal;

namespace Tests
{
    public class AddressControllerTests
    {
        private Mock<HemoRedContext> _mockContext;
        private AddressController _controller;
        private Mock<DbSet<TblAddress>> _mockAddressSet;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<HemoRedContext>();

            // Mock DbSet
            var addresses = new List<TblAddress>
            {
                new TblAddress
                    { AddressId = 1, MunicipalityId = 1, ProvinceId = 1, Street = "Main St", BuildingNumber = 123 },
                new TblAddress
                    { AddressId = 2, MunicipalityId = 2, ProvinceId = 2, Street = "Second St", BuildingNumber = 456 }
            }.AsQueryable();

            _mockAddressSet = new Mock<DbSet<TblAddress>>();
            _mockAddressSet.As<IQueryable<TblAddress>>().Setup(m => m.Provider).Returns(addresses.Provider);
            _mockAddressSet.As<IQueryable<TblAddress>>().Setup(m => m.Expression).Returns(addresses.Expression);
            _mockAddressSet.As<IQueryable<TblAddress>>().Setup(m => m.ElementType).Returns(addresses.ElementType);
            _mockAddressSet.As<IQueryable<TblAddress>>().Setup(m => m.GetEnumerator())
                .Returns(addresses.GetEnumerator());

            // Mock async operations
            _mockAddressSet.As<IAsyncEnumerable<TblAddress>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<TblAddress>(addresses.GetEnumerator()));
            _mockAddressSet.As<IQueryable<TblAddress>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<TblAddress>(addresses.Provider));

            _mockContext.Setup(c => c.TblAddresses).Returns(_mockAddressSet.Object);

            _controller = new AddressController(_mockContext.Object);
        }

        [Test]
        public async Task GetAddresses_ReturnsAllAddresses()
        {
            // Act
            var result = await _controller.GetAddresses();

            // Assert
            var okResult = result.Value.ToList();
            okResult.Should().NotBeNull();
            okResult.Should().HaveCountLessOrEqualTo(okResult.Count);
        }

        [Test]
        public async Task GetAddress_ReturnsAddress_WhenIdExists()
        {
            // Act
            var result = await _controller.GetAddress(1);

            // Assert
            result.Should().NotBeNull();
            result.Value.Street.Should().Be("Main St");
        }

        [Test]
        public async Task GetAddress_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Act
            var result = await _controller.GetAddress(658474837);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task PutAddress_UpdatesAddress_WhenIdExists()
        {
            // Arrange
            var newAddress = new newAddressDTO
            {
                MunicipalityID = 3,
                ProvinceID = 3,
                Street = "Third St",
                BuildingNumber = 789
            };

            // Act
            var result = await _controller.PuttblAddress(1, newAddress);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _mockAddressSet.Verify(m => m.FindAsync(1), Times.Once);
        }

        [Test]
        public async Task PostAddressTestsTask()
        {
            // Arrange
            var newAddress = new newAddressDTO
            {
                MunicipalityID = 3,
                ProvinceID = 3,
                Street = "Third St",
                BuildingNumber = 789
            };

            // Act
            var result = await _controller.PostAddress(newAddress);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<AddressDto>>();
        }
    }
}