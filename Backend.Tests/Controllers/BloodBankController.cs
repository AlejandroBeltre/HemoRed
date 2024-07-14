using backend.Context;
using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Controllers;
using FluentAssertions;

namespace Backend.Tests.Controllers
{
    internal class TestBloodBankController
    {
        private Mock<HemoRedContext> _mockContext;
        private BloodBankController _controller;
        private Mock<DbSet<TblBloodBank>> _mockBloodBankSet;
        private Mock<DbSet<TblAddress>> _mockAddressSet;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<HemoRedContext>();

            var bloodBanks = new List<TblBloodBank>
            {
                new TblBloodBank { BloodBankId = 1, AddressId = 1, BloodBankName = "Blood Bank A", AvailableHours = "9am - 5pm", Phone = "123456789", Image = "path/to/image1" },
                new TblBloodBank { BloodBankId = 2, AddressId = 2, BloodBankName = "Blood Bank B", AvailableHours = "10am - 6pm", Phone = "987654321", Image = "path/to/image2" }
            }.AsQueryable();

            var addresses = new List<TblAddress>
            {
                new TblAddress
                    { AddressId = 1, MunicipalityId = 1, ProvinceId = 1, Street = "Main St", BuildingNumber = 123 },
                new TblAddress
                    { AddressId = 2, MunicipalityId = 2, ProvinceId = 2, Street = "Second St", BuildingNumber = 456 }
            }.AsQueryable();

            _mockBloodBankSet = bloodBanks.BuildMock().BuildMockDbSet();
            _mockAddressSet = addresses.BuildMock().BuildMockDbSet();

            _mockContext.Setup(c => c.TblBloodBanks).Returns(_mockBloodBankSet.Object);
            _mockContext.Setup(c => c.TblAddresses).Returns(_mockAddressSet.Object);

            _mockContext.Setup(c => c.TblBloodBanks.FindAsync(It.IsAny<object[]>()))
                .Returns<object[]>(keyValues =>
                {
                    var id = (int)keyValues.First();
                    return new ValueTask<TblBloodBank>(Task.FromResult(bloodBanks.FirstOrDefault(b => b.BloodBankId == id)));
                });

            _controller = new BloodBankController(_mockContext.Object);
        }

        [Test]
        public async Task GetBloodBanks_ReturnsAllBloodBanks()
        {
            var result = _controller.GetBloodBanks();

            result.Result.Should().NotBeNull();
            result.Result.Value.Should().HaveCount(2);
        }

        [Test]
        public async Task GetBloodBank_ReturnsBloodBank_WhenIdExists()
        {
            var result = await _controller.GetBloodBank(1);
;
            result.Should().NotBeNull();
            var bloodBank = result.Value;
            result.Should().NotBeNull();
            bloodBank.BloodBankID.Equals(1);
        }

        [Test]
        public async Task GetBloodBank_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var result = await _controller.GetBloodBank(999);

            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task PostBloodBank_CreatesNewBloodBank()
        {
            var newBloodBankDto = new NewBloodBankDTO
            {
                AddressID = 1,
                BloodBankName = "New Blood Bank",
                AvailableHours = "8am - 4pm",
                Phone = "111222333"
            };

            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.Length).Returns(1);
            formFileMock.Setup(f => f.FileName).Returns("image.png");
            formFileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var result = await _controller.PostBloodBank(newBloodBankDto, formFileMock.Object);

            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            var bloodBank = createdResult.Value as BloodBankDto;
            Assert.IsNotNull(bloodBank);
            Assert.AreEqual("New Blood Bank", bloodBank.BloodBankName);
        }

        [Test]
        public async Task PutBloodBank_UpdatesBloodBank_WhenIdExists()
        {
            var newBloodBankDto = new NewBloodBankDTO
            {
                AddressID = 1,
                BloodBankName = "Updated Blood Bank",
                AvailableHours = "8am - 4pm",
                Phone = "444555666"
            };

            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.Length).Returns(1);
            formFileMock.Setup(f => f.FileName).Returns("image.png");
            formFileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var result = await _controller.PutBloodBank(1, newBloodBankDto, formFileMock.Object);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task PutBloodBank_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var newBloodBankDto = new NewBloodBankDTO
            {
                AddressID = 1,
                BloodBankName = "Updated Blood Bank",
                AvailableHours = "8am - 4pm",
                Phone = "444555666"
            };

            var formFileMock = new Mock<IFormFile>();
            formFileMock.Setup(f => f.Length).Returns(1);
            formFileMock.Setup(f => f.FileName).Returns("image.png");
            formFileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var result = await _controller.PutBloodBank(999, newBloodBankDto, formFileMock.Object);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task DeleteBloodBank_ReturnsNoContent_WhenIdExists()
        {
            var result = await _controller.DeleteBloodBank(2);

            result.Should().NotBeNull();
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteBloodBank_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var result = await _controller.DeleteBloodBank(999);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
