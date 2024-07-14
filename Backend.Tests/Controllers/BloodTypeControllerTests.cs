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
    internal class BloodTypeControllerTests
    {

            private Mock<HemoRedContext> _mockContext;
            private BloodTypeController _controller;
            private Mock<DbSet<TblBloodType>> _mockBloodTypeSet;

            [SetUp]
            public void Setup()
            {
                _mockContext = new Mock<HemoRedContext>();

                var bloodTypes = new List<TblBloodType>
            {
                new TblBloodType { BloodTypeId = 1, BloodType = "A+" },
                new TblBloodType { BloodTypeId = 2, BloodType = "B+" }
            }.AsQueryable();

                _mockBloodTypeSet = bloodTypes.BuildMock().BuildMockDbSet();

                _mockContext.Setup(c => c.TblBloodTypes).Returns(_mockBloodTypeSet.Object);

                _mockContext.Setup(c => c.TblBloodTypes.FindAsync(It.IsAny<object[]>()))
                    .Returns<object[]>(keyValues =>
                    {
                        var id = (int)keyValues.First();
                        return new ValueTask<TblBloodType>(Task.FromResult(bloodTypes.FirstOrDefault(b => b.BloodTypeId == id)));
                    });

                _controller = new BloodTypeController(_mockContext.Object);
            }

            [Test]
            public async Task GetBloodTypes_ReturnsAllBloodTypes()
            {
                var result = await _controller.GetBloodTypes();
                Assert.AreEqual(2, result.Count());
            }

            [Test]
            public async Task GetBloodTypeById_ReturnsBloodType_WhenIdExists()
            {
                var result = await _controller.GetBloodTypeById(1);
                Assert.IsNotNull(result);
                Assert.AreEqual("A+", result.BloodType);
            }

            [Test]
            public async Task GetBloodTypeById_ReturnsNull_WhenIdDoesNotExist()
            {
                var result = await _controller.GetBloodTypeById(999);
                Assert.IsNull(result);
            }

            [Test]
            public async Task DeleteBloodType_ReturnsTrue_WhenIdExists()
            {
                var result = await _controller.DeleteBloodType(1);
                Assert.IsTrue(result);
            }

            [Test]
            public async Task DeleteBloodType_ReturnsFalse_WhenIdDoesNotExist()
            {
                var result = await _controller.DeleteBloodType(999);
                Assert.IsFalse(result);
            }
        }
    }
