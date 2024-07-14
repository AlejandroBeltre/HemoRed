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
using MockQueryable.Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;

namespace Backend.Tests.Controllers
{
    internal class CampaignControllerTests
    {
        private Mock<HemoRedContext> _mockContext;
        private CampaignController _controller;
        private Mock<DbSet<TblCampaign>> _mockCampaignSet;
        private Mock<DbSet<TblAddress>> _mockAddressSet;
        private Mock<DbSet<TblOrganizer>> _mockOrganizerSet;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<HemoRedContext>();

            var campaigns = new List<TblCampaign>
            {
                new TblCampaign { CampaignId = 1, CampaignName = "Campaign 1", Description = "Desc 1", StartTimestamp = DateTime.Now, EndTimestamp = DateTime.Now.AddDays(1), AddressId = 1, OrganizerId = 1 },
                new TblCampaign { CampaignId = 2, CampaignName = "Campaign 2", Description = "Desc 2", StartTimestamp = DateTime.Now, EndTimestamp = DateTime.Now.AddDays(2), AddressId = 2, OrganizerId = 2 }
            }.AsQueryable();

            _mockCampaignSet = campaigns.BuildMock().BuildMockDbSet();
            _mockContext.Setup(c => c.TblCampaigns).Returns(_mockCampaignSet.Object);
            _mockContext.Setup(c => c.TblCampaigns.FindAsync(It.IsAny<object[]>()))
                .Returns<object[]>(keyValues =>
                {
                    var id = (int)keyValues.First();
                    return new ValueTask<TblCampaign>(Task.FromResult(campaigns.FirstOrDefault(c => c.CampaignId == id)));
                });

            var addresses = new List<TblAddress>
            {
                new TblAddress
                    { AddressId = 1, MunicipalityId = 1, ProvinceId = 1, Street = "Main St", BuildingNumber = 123 },
                new TblAddress
                    { AddressId = 2, MunicipalityId = 2, ProvinceId = 2, Street = "Second St", BuildingNumber = 456 }
            }.AsQueryable();

            _mockAddressSet = addresses.BuildMock().BuildMockDbSet();
            _mockContext.Setup(c => c.TblAddresses).Returns(_mockAddressSet.Object);
            _mockContext.Setup(c => c.TblAddresses.FindAsync(It.IsAny<object[]>()))
                .Returns<object[]>(keyValues =>
                {
                    var id = (int)keyValues.First();
                    return new ValueTask<TblAddress>(Task.FromResult(addresses.FirstOrDefault(a => a.AddressId == id)));
                });

            var organizers = new List<TblOrganizer>
            {
                new TblOrganizer { OrganizerId = 1, OrganizerName = "Organizer 1" },
                new TblOrganizer { OrganizerId = 2, OrganizerName = "Organizer 2" }
            }.AsQueryable();

            _mockOrganizerSet = organizers.BuildMock().BuildMockDbSet();
            _mockContext.Setup(c => c.TblOrganizers).Returns(_mockOrganizerSet.Object);
            _mockContext.Setup(c => c.TblOrganizers.FindAsync(It.IsAny<object[]>()))
                .Returns<object[]>(keyValues =>
                {
                    var id = (int)keyValues.First();
                    return new ValueTask<TblOrganizer>(Task.FromResult(organizers.FirstOrDefault(o => o.OrganizerId == id)));
                });

            _controller = new CampaignController(_mockContext.Object);
        }

        [Test]
        public async Task GetCampaigns_ReturnsAllCampaigns()
        {
            var result = await _controller.GetCampaigns();

            result.Should().NotBeNull();
            result.Value.Should().HaveCount(2);

        }

        [Test]
        public async Task GetCampaign_ReturnsCampaign_WhenIdExists()
        {
            var result = await _controller.GetCampaign(1);

            result.Should().NotBeNull();
            result.Value.CampaignName.Should().Be("Campaign 1");
        }

        [Test]
        public async Task GetCampaign_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var result = await _controller.GetCampaign(999);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task PostCampaign_CreatesNewCampaign()
        {
            var newCampaignDto = new NewCampaignDto
            {
                AddressID = 1,
                CampaignName = "New Campaign",
                Description = "New Description",
                StartTimestamp = DateTime.Now,
                EndTimestamp = DateTime.Now.AddDays(1),
                OrganizerID = 1
            };

            var fileMock = new Mock<IFormFile>();
            var content = "Fake File Content";
            var fileName = "test.jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var result = await _controller.PostCampaign(newCampaignDto, fileMock.Object);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            var createdCampaign = createdAtActionResult.Value as CampaignDto;

            Assert.IsNotNull(createdCampaign);
            Assert.AreEqual("New Campaign", createdCampaign.CampaignName);
        }

        [Test]
        public async Task PutCampaign_UpdatesCampaign()
        {
            var campaignDto = new CampaignDto
            {
                AddressID = 1,
                CampaignID = 1,
                CampaignName = "Updated Campaign",
                Description = "Updated Description",
                StartTimestamp = DateTime.Now,
                EndTimestamp = DateTime.Now.AddDays(1),
                OrganizerID = 1
            };

            var result = await _controller.PutCampaign(1, campaignDto, null);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteCampaign_DeletesCampaign()
        {
            var result = await _controller.DeleteCampaign(1);
            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}
