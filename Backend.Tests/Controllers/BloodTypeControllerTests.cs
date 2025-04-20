using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Controllers;
using backend.Context;
using backend.Models;
using backend.DTO;
using backend.Enums;
using System.Threading.Tasks;

namespace Backend.Tests.Controllers
{
    public class BloodTypeValidationTests
    {
        [Fact]
        public async Task PostRequest_ValidBloodType_ReturnsCreatedAtAction()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HemoRedContext>()
                .UseInMemoryDatabase(databaseName: "ValidBloodTypeTest_1")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                // Remove any MySQL-specific configuration here
                .Options;

            // Preparar la base de datos en memoria con datos de prueba
            using (var context = new TestHemoRedContext(options))
            {
                // Añadir tipos de sangre válidos
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 1, BloodType = "A+" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 2, BloodType = "A-" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 3, BloodType = "B+" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 4, BloodType = "B-" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 5, BloodType = "AB+" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 6, BloodType = "AB-" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 7, BloodType = "O+" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 8, BloodType = "O-" });

                // Añadir un banco de sangre
                context.TblBloodBanks.Add(new TblBloodBank
                {
                    BloodBankId = 1,
                    BloodBankName = "Test Blood Bank",
                    AddressId = 1,
                    AvailableHours = "9AM-5PM",
                    Phone = "123-456-7890"
                });

                // Añadir dirección necesaria para relaciones
                context.TblAddresses.Add(new TblAddress
                {
                    AddressId = 1,
                    MunicipalityId = 1,
                    ProvinceId = 1,
                    Street = "Test Street",
                    BuildingNumber = 123
                });

                // Añadir un usuario
                context.TblUsers.Add(new TblUser
                {
                    DocumentNumber = "001-1234567-8",
                    FullName = "Test User",
                    Email = "test@example.com",
                    Password = "password",
                    BloodTypeId = 1,
                    BirthDate = new System.DateTime(1990, 1, 1),
                    Gender = Gender.M,
                    UserRole = UserRole.U
                });

                await context.SaveChangesAsync();
            }

            // Crear una nueva instancia del contexto para el controlador
            using (var context = new TestHemoRedContext(options))
            {
                var controller = new RequestController(context);

                // Crear una solicitud con tipo de sangre válido (A+)
                var requestDto = new NewRequestDto
                {
                    UserDocument = "001-1234567-8",
                    BloodTypeId = 1, // A+
                    BloodBankId = 1,
                    RequestTimeStamp = System.DateTime.Now,
                    RequestReason = "Test Request",
                    RequestedAmount = 2,
                    Status = Status.P
                };

                // Act
                var result = await controller.PostRequest(requestDto);

                // Assert
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
                var returnValue = Assert.IsType<RequestDto>(createdAtActionResult.Value);

                Assert.Equal(1, returnValue.BloodTypeId);
                Assert.Equal("001-1234567-8", returnValue.UserDocument);
                Assert.Equal("Test Request", returnValue.RequestReason);
            }
        }

        [Fact]
        public async Task PostRequest_InvalidBloodType_ReturnsBadRequest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HemoRedContext>()
                .UseInMemoryDatabase(databaseName: "ValidBloodTypeTest_2")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                // Remove any MySQL-specific configuration here
                .Options;

            // Preparar la base de datos en memoria con datos de prueba
            using (var context = new TestHemoRedContext(options))
            {
                // Añadir tipos de sangre válidos (los mismos que en la prueba anterior)
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 1, BloodType = "A+" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 2, BloodType = "A-" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 3, BloodType = "B+" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 4, BloodType = "B-" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 5, BloodType = "AB+" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 6, BloodType = "AB-" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 7, BloodType = "O+" });
                context.TblBloodTypes.Add(new TblBloodType { BloodTypeId = 8, BloodType = "O-" });

                // Añadir un banco de sangre
                context.TblBloodBanks.Add(new TblBloodBank
                {
                    BloodBankId = 1,
                    BloodBankName = "Test Blood Bank",
                    AddressId = 1,
                    AvailableHours = "9AM-5PM",
                    Phone = "123-456-7890"
                });

                // Añadir dirección necesaria para relaciones
                context.TblAddresses.Add(new TblAddress
                {
                    AddressId = 1,
                    MunicipalityId = 1,
                    ProvinceId = 1,
                    Street = "Test Street",
                    BuildingNumber = 123
                });

                // Añadir un usuario
                context.TblUsers.Add(new TblUser
                {
                    DocumentNumber = "001-1234567-8",
                    FullName = "Test User",
                    Email = "test@example.com",
                    Password = "password",
                    BloodTypeId = 1,
                    BirthDate = new System.DateTime(1990, 1, 1),
                    Gender = Gender.M,
                    UserRole = UserRole.U
                });

                await context.SaveChangesAsync();
            }

            // Crear una nueva instancia del contexto para el controlador
            using (var context = new TestHemoRedContext(options))
            {
                var controller = new RequestController(context);

                // Crear una solicitud con tipo de sangre inválido (999, que no existe)
                var requestDto = new NewRequestDto
                {
                    UserDocument = "001-1234567-8",
                    BloodTypeId = 999, // Tipo inválido
                    BloodBankId = 1,
                    RequestTimeStamp = System.DateTime.Now,
                    RequestReason = "Test Request",
                    RequestedAmount = 2,
                    Status = Status.P
                };

                // Act
                var result = await controller.PostRequest(requestDto);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
                Assert.Contains("Invalid user or blood type", badRequestResult.Value.ToString());
            }
        }
    }
}