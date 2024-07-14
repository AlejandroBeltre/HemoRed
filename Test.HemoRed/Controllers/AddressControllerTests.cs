using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;
using backend.Context;
using backend.Controllers;
using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Test.HemoRed.Controllers
{
    public class AddressControllerTests
    {
        private HemoRedContext fakeHemoRedContext;

        public AddressControllerTests()
        {
            this.fakeHemoRedContext = A.Fake<HemoRedContext>();
        }


        private AddressController CreateAddressController()
        {
            return new AddressController(
                this.fakeHemoRedContext);
        }

        [Fact]
        public async Task GetAddresses_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressController = this.CreateAddressController();

            // Act
            ActionResult<IEnumerable<AddressDto>> result = await addressController.GetAddresses();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task GetAddresses_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var addressController = this.CreateAddressController();
            int id = 0;

            // Act
            var result = await addressController.GetAddresses(
                id);

            // Assert
            Assert.True(false);
        }

        [Fact]
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
            Assert.True(false);
        }

        [Fact]
        public async Task PostAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressController = this.CreateAddressController();
            newAddressDTO newAddressDto = null;

            // Act
            var result = await addressController.PostAddress(
                newAddressDto);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task DeletetblAddress_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var addressController = this.CreateAddressController();
            int id = 0;

            // Act
            var result = await addressController.DeletetblAddress(
                id);

            // Assert
            Assert.True(false);
        }
    }
}
