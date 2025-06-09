using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using elaw_desenvolvedor_junior.Api;
using elaw_desenvolvedor_junior.Application.Interfaces;
using elaw_desenvolvedor_junior.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using elaw_desenvolvedor_junior.Application.Services;

namespace elaw_desenvolvedor_junior_test
{
    public class ClientControllerTests
    {
        private readonly Mock<IClientService> _serviceMock;
        private readonly ClientController _controller;

        public ClientControllerTests()
        {
            _serviceMock = new Mock<IClientService>();
            _controller = new ClientController(_serviceMock.Object);
        }

        [Fact]
        public void Create_NullDto_ReturnsBadRequest()
        {
            // Act
            var result = _controller.Create(null);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Request body cannot be null.", badRequest.Value);
        }

        [Fact]
        public void Create_ValidDto_ReturnsCreatedAtAction()
        {
            // Arrange
            var dto = new CreateClientDto
            {
                Name = "Name",
                Email = "email@test.com",
                Phone = "123",
                Address = new AddressDto
                {
                    Street = "R Lorem",
                    City = "Lorem",
                    Number = "1",
                    State = "RJ",
                    ZipCode = "0000"
                }
            };

            // Validação manual de modelo
            _controller.ModelState.Clear(); // Garante estado limpo
            var validationContext = new ValidationContext(dto, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(dto, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            // Se o ModelState tiver erros, isso simulou o comportamento real
            Assert.True(_controller.ModelState.IsValid, "ModelState está inválido. Corrija os campos obrigatórios antes de seguir.");

            var returned = new GetClientDto
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            _serviceMock.Setup(s => s.CreateClient(dto)).Returns(returned);

            // Act
            var result = _controller.Create(dto);

            // Assert
            var createdAt = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetById), createdAt.ActionName);

            var respObj = createdAt.Value;
            var respType = respObj.GetType();
            var msgProp = respType.GetProperty("message");
            var dataProp = respType.GetProperty("data");

            Assert.NotNull(msgProp);
            Assert.NotNull(dataProp);
            Assert.Equal("Created successfully", msgProp.GetValue(respObj));

            var dataObj = dataProp.GetValue(respObj);
            Assert.Equal(returned.Id, ((GetClientDto)dataObj).Id);
            Assert.Equal(returned.Name, ((GetClientDto)dataObj).Name);
            Assert.Equal(returned.Email, ((GetClientDto)dataObj).Email);
            Assert.Equal(returned.Phone, ((GetClientDto)dataObj).Phone);
            Assert.Equal(returned.Address, ((GetClientDto)dataObj).Address);
        }



        [Fact]
        public void GetAll_NoUsers_ReturnsMessage()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetClients()).Returns(new List<GetClientDto>());

            // Act
            var result = _controller.GetAll();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var respObj = ok.Value;

            var respType = respObj.GetType();
            var msgProp = respType.GetProperty("message");

            Assert.NotNull(msgProp);
            Assert.Equal("No users found.", msgProp.GetValue(respObj));
        }


        [Fact]
        public void GetAll_HasUsers_ReturnsList()
        {
            // Arrange
            var list = new List<GetClientDto> { new GetClientDto { Id = Guid.NewGuid() } };
            _serviceMock.Setup(s => s.GetClients()).Returns(list);

            // Act
            var result = _controller.GetAll();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(list, ok.Value);
        }

        [Fact]
        public void GetById_NotFound_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.GetClientById(id)).Returns((GetClientDto)null);

            // Act
            var result = _controller.GetById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetById_Found_ReturnsOk()
        {
            // Arrange
            var dto = new GetClientDto { Id = Guid.NewGuid() };
            _serviceMock.Setup(s => s.GetClientById(dto.Id)).Returns(dto);

            // Act
            var result = _controller.GetById(dto.Id);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(dto, ok.Value);
        }


        [Fact]
        public void Update_InvalidDto_ReturnsBadRequest()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var result = _controller.Update(id, null);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Request body cannot be null.", badRequest.Value);
        }



        [Fact]
        public void Update_NotFound_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateClientDto { };
            _serviceMock.Setup(s => s.UpdateClient(id, updateDto)).Returns((GetClientDto)null);

            // Act
            var result = _controller.Update(id, updateDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Update_Valid_ReturnsOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateClientDto { Name = "Name" };
            var updated = new GetClientDto { Id = id, Name = "Name" };
            _serviceMock.Setup(s => s.UpdateClient(id, updateDto)).Returns(updated);

            // Act
            var result = _controller.Update(id, updateDto);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var respObj = ok.Value;
            var respType = respObj.GetType();

            var msgProp = respType.GetProperty("message");
            var dataProp = respType.GetProperty("data");

            Assert.NotNull(msgProp);
            Assert.NotNull(dataProp);

            Assert.Equal("User updated successfully", msgProp.GetValue(respObj));
            Assert.Equal(updated, dataProp.GetValue(respObj));
        }


        [Fact]
        public void Delete_NotFound_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.GetClientById(id)).Returns((GetClientDto)null);

            // Act
            var result = _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Delete_Valid_ReturnsOk()
        {
            // Arrange
            var dto = new GetClientDto { Id = Guid.NewGuid() };
            _serviceMock.Setup(s => s.GetClientById(dto.Id)).Returns(dto);

            // Act
            var result = _controller.Delete(dto.Id);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var respObj = ok.Value;

            var respType = respObj.GetType();
            var msgProp = respType.GetProperty("message");
            var dataProp = respType.GetProperty("data");

            Assert.NotNull(msgProp);
            Assert.NotNull(dataProp);
            Assert.Equal("User deleted successfully", msgProp.GetValue(respObj));
            Assert.Equal(dto, dataProp.GetValue(respObj));

            _serviceMock.Verify(s => s.DeleteClientById(dto.Id), Times.Once);
        }
    }
}
