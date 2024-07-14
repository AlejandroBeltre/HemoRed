using FakeItEasy;
using System;
using System.Threading.Tasks;
using backend.Context;
using backend.Controllers;
using backend.DTO;
using Xunit;
using backend.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Assert = Xunit.Assert;

namespace HemoRed.Tests.Controllers
{
    [TestFixture]
    public class AddressControllerTests
    {
        private HemoRedContext _fakeHemoRedContext;

        [SetUp]
        public void SetUp()
        {
            this._fakeHemoRedContext = A.Fake<HemoRedContext>();
        }

        [TearDown]
        public void TearDown() // Make this method public
        {
            this._fakeHemoRedContext.Dispose(); // Properly dispose of the _fakeHemoRedContext
            this._fakeHemoRedContext = null;
        }
        private AddressController CreateAddressController()
        {
            return new AddressController(
                this._fakeHemoRedContext);
        }

        [Test]
        public async Task GetAddresses_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressController = this.CreateAddressController();

            // Act
            var result = await addressController.GetAddresses();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task GetAddresses_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var addressController = this.CreateAddressController();
            int id = 0;

            // Act
            var result = await addressController.GetAddresses(
                id);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task PuttblAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressController = this.CreateAddressController();
            int id = 0;
            newAddressDTO newAddressDto = null;

            // Act
            var result = await addressController.PuttblAddress(
                id,
                newAddressDto);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task PostAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressController = this.CreateAddressController();
            newAddressDTO newAddressDto = null;

            // Act
            var result = await addressController.PostAddress(
                newAddressDto);

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task DeletetblAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressController = this.CreateAddressController();
            int id = 0;

            // Act
            var result = await addressController.DeletetblAddress(
                id);

            // Assert
            Assert.Fail();
        }
    }
}
