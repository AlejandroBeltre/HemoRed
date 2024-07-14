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

namespace Backend.Tests.Controllers
{
    internal class CampaignParticipationControllerTests
    {
        private Mock<HemoRedContext> _mockContext;
        private CampaignParticipationController _controller;
        private Mock<DbSet<TblCampaignParticipation>> _mockCampaignParticipationSet;
        private Mock<DbSet<TblCampaign>> _mockCampaignSet;
        private Mock<DbSet<TblOrganizer>> _mockOrganizerSet;
        private Mock<DbSet<TblDonation>> _mockDonationSet;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<HemoRedContext>();

            var campaignParticipations = new List<TblCampaignParticipation>
            {
                new TblCampaignParticipation { CampaignId = 1, OrganizerId = 1, DonationId = 1 },
                new TblCampaignParticipation { CampaignId = 2, OrganizerId = 2, DonationId = 2 }
            }.AsQueryable();

            _mockCampaignParticipationSet = campaignParticipations.BuildMock().BuildMockDbSet();
            _mockContext.Setup(c => c.TblCampaignParticipations).Returns(_mockCampaignParticipationSet.Object);

            var campaigns = new List<TblCampaign>
            {
                new TblCampaign { CampaignId = 1, CampaignName = "Campaign 1" },
                new TblCampaign { CampaignId = 2, CampaignName = "Campaign 2" }
            }.AsQueryable();

            _mockCampaignSet = campaigns.BuildMock().BuildMockDbSet();
            _mockContext.Setup(c => c.TblCampaigns).Returns(_mockCampaignSet.Object);
            _mockContext.Setup(c => c.TblCampaigns.FindAsync(It.IsAny<object[]>()))
                .Returns<object[]>(keyValues =>
                {
                    var id = (int)keyValues.First();
                    return new ValueTask<TblCampaign>(Task.FromResult(campaigns.FirstOrDefault(c => c.CampaignId == id)));
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
            var donations = new List<TblDonation>
            {
                new TblDonation { DonationId = 1, BloodTypeId = 1, BloodBankId = 1, DonationTimestamp = DateTime.Now },
                new TblDonation { DonationId = 2, BloodTypeId = 2, BloodBankId = 2, DonationTimestamp = DateTime.Now }
            }.AsQueryable();

            _mockDonationSet = donations.BuildMock().BuildMockDbSet();
            _mockContext.Setup(c => c.TblDonations).Returns(_mockDonationSet.Object);
            _mockContext.Setup(c => c.TblDonations.FindAsync(It.IsAny<object[]>()))
                .Returns<object[]>(keyValues =>
                {
                    var id = (int)keyValues.First();
                    return new ValueTask<TblDonation>(Task.FromResult(donations.FirstOrDefault(d => d.DonationId == id)));
                });

            _controller = new CampaignParticipationController(_mockContext.Object);
        }

        [Test]
        public async Task GetTblCampaignParticipations_ReturnsAllCampaignParticipations()
        {
            var result = await _controller.GetTblCampaignParticipations();
            var participations = result.Value.ToList();

            Assert.AreEqual(2, participations.Count);
        }

        [Test]
        public async Task GetTblCampaignParticipation_ReturnsCampaignParticipation_WhenIdExists()
        {
            var result = await _controller.GetTblCampaignParticipation(1);
            var participation = result.Value;

            Assert.IsNotNull(participation);
            Assert.AreEqual(1, participation.CampaignID);
        }

        [Test]
        public async Task GetTblCampaignParticipation_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var result = await _controller.GetTblCampaignParticipation(999);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task PostTblCampaignParticipation_CreatesNewCampaignParticipation()
        {
            var newParticipationDto = new CampaignParticipationDto
            {
                CampaignID = 1,
                OrganizerID = 1,
                DonationID = 1
            };

            var result = await _controller.PostTblCampaignParticipation(newParticipationDto);
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            var createdParticipation = createdAtActionResult.Value as CampaignParticipationDto;

            Assert.IsNotNull(createdParticipation);
            Assert.AreEqual(1, createdParticipation.CampaignID);
        }

        [Test]
        public async Task PutTblCampaignParticipation_UpdatesCampaignParticipation()
        {
            var participationDto = new CampaignParticipationDto
            {
                CampaignID = 1,
                OrganizerID = 2,
                DonationID = 2
            };

            var result = await _controller.PutTblCampaignParticipation(1, participationDto);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteTblCampaignParticipation_DeletesCampaignParticipation()
        {
            var result = await _controller.DeleteTblCampaignParticipation(1);
            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}
