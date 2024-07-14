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
using System.Threading;
using FluentAssertions;
using MockQueryable.Moq;

namespace Tests
{
    public class BloodBagControllerTests
    {
        private Mock<HemoRedContext> _mockContext;
        private BloodBagController _controller;
        private Mock<DbSet<TblBloodBag>> _mockBloodBagSet;
        private Mock<DbSet<TblBloodType>> _mockBloodTypeSet;
        private Mock<DbSet<TblBloodBank>> _mockBloodBankSet;
        private Mock<DbSet<TblDonation>> _mockDonationSet;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<HemoRedContext>();

            var bloodBags = new List<TblBloodBag>
            {
                new TblBloodBag { BagId = 1, BloodTypeId = 1, BloodBankId = 1, DonationId = 1, ExpirationDate = DateTime.Now.AddDays(30), IsReserved = false },
                new TblBloodBag { BagId = 2, BloodTypeId = 2, BloodBankId = 2, DonationId = 2, ExpirationDate = DateTime.Now.AddDays(60), IsReserved = true }
            }.AsQueryable();

            var bloodTypes = new List<TblBloodType>
            {
                new TblBloodType { BloodTypeId = 1, BloodType = "A+" },
                new TblBloodType { BloodTypeId = 2, BloodType = "O-" }
            }.AsQueryable();

            var bloodBanks = new List<TblBloodBank>
            {
                new TblBloodBank { BloodBankId = 1, AddressId = 1, BloodBankName = "Blood Bank A", AvailableHours = "9am - 5pm", Phone = "123456789" },
                new TblBloodBank { BloodBankId = 2, AddressId = 2, BloodBankName = "Blood Bank B", AvailableHours = "10am - 6pm", Phone = "987654321" }
            }.AsQueryable();

            var donations = new List<TblDonation>
            {
                new TblDonation { DonationId = 1, BloodTypeId = 1, BloodBankId = 1, DonationTimestamp = DateTime.Now },
                new TblDonation { DonationId = 2, BloodTypeId = 2, BloodBankId = 2, DonationTimestamp = DateTime.Now }
            }.AsQueryable();

            _mockBloodBagSet = bloodBags.BuildMock().BuildMockDbSet();
            _mockBloodTypeSet = bloodTypes.BuildMock().BuildMockDbSet();
            _mockBloodBankSet = bloodBanks.BuildMock().BuildMockDbSet();
            _mockDonationSet = donations.BuildMock().BuildMockDbSet();

            _mockContext.Setup(c => c.TblBloodBags).Returns(_mockBloodBagSet.Object);
            _mockContext.Setup(c => c.TblBloodTypes).Returns(_mockBloodTypeSet.Object);
            _mockContext.Setup(c => c.TblBloodBanks).Returns(_mockBloodBankSet.Object);
            _mockContext.Setup(c => c.TblDonations).Returns(_mockDonationSet.Object);

            _controller = new BloodBagController(_mockContext.Object);
        }


        [Test]
        public async Task GetBloodBags_ReturnsAllBloodBags()
        {
            // Act
            var result = await _controller.GetBloodBags();

            // Assert
            result.Should().NotBeNull();
            var bloodBags = result.Value;
            bloodBags.Should().HaveCount(2);
        }

        [Test]
        public async Task GetBloodBag_ReturnsBloodBag_WhenIdExists()
        {
            var result = await _controller.GetBloodBag(1);

            result.Should().NotBeNull();
            var bloodBag = result.Value;
            result.Should().NotBeNull();
            bloodBag.BloodTypeID.Equals(1);
        }

        [Test]
        public async Task GetBloodBag_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var result = await _controller.GetBloodBag(999);

            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task PutBloodBag_ReturnsNoContent_WhenIdExists()
        {
            var newBloodBagDto = new NewBloodBagDTO
            {
                BloodTypeID = 1,
                BloodBankID = 1,
                DonationID = 1,
                ExpirationDate = DateTime.Now.AddDays(45),
                IsReserved = false
            };

            var result = await _controller.PutBloodBag(1, newBloodBagDto);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task PutBloodBag_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var newBloodBagDto = new NewBloodBagDTO
            {
                BloodTypeID = 1,
                BloodBankID = 1,
                DonationID = 1,
                ExpirationDate = DateTime.Now.AddDays(45),
                IsReserved = false
            };

            var result = await _controller.PutBloodBag(999, newBloodBagDto);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task PostBloodBag_CreatesNewBloodBag()
        {
            var newBloodBagDto = new NewBloodBagDTO
            {
                BloodTypeID = 4,
                BloodBankID = 1,
                DonationID = 1,
                ExpirationDate = DateTime.Now.AddDays(45).AddHours(90),
                IsReserved = false
            };

            var result = await _controller.PostBloodBag(newBloodBagDto);


            result.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteBloodBag_ReturnsNoContent_WhenIdExists()
        {
            var result = await _controller.DeleteBloodBag(1);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteBloodBag_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var result = await _controller.DeleteBloodBag(999);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
