using Xunit;
using Moq;
using System.Net;
using System.Net.Http.Json;
using BankAppTestBack.Domain.Entities;
using BankAppTestBack.IntegrationTests;
using BankAppTestBack.Application.Dtos;
using System.Text.Json.Serialization;
using System.Text.Json;
using BankAppTestBack.Application.UseCases.Client.Commands.CreateClient;
using BankAppTestBack.Application.UseCases.Client.Commands.UpdateClient;
using Microsoft.AspNetCore.Http;

namespace BankAppTestBack
{
    public class ClientControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public ClientControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _jsonOptions.Converters.Add(new JsonStringEnumConverter());
        }

        [Fact]
        public async Task GetClientById_ShouldReturn200AndClient_WhenFound()
        {
            var clientExpected = new Client("Test Client",EGender.M,DateTime.Now,"1", "Otavalo sn y principal","098254785","123", false);
            clientExpected.Id = 10;
            _factory.ClientRepositoryMock
                .Setup(repo => repo.FindByIdAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientExpected);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var response = await _client.GetAsync("/Client/10");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var clientResponse = await response.Content.ReadFromJsonAsync<ClientResponse>(options);
            Assert.Equal("Test Client", clientResponse.Name);
        }

        [Fact]
        public async Task GetClientById_ShouldReturn404_WhenNotFound()
        {
            _factory.ClientRepositoryMock
                .Setup(repo => repo.FindByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Client)null!);

            var response = await _client.GetAsync("/Client/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostClient_ShouldReturn201Created_WhenSuccessful()
        {
            var clientCreateRequest = new CreateClientCommand
            { 
                Name = "New Test Client",
                Gender = EGender.F,
                Birthdate = DateTime.Parse("1990-01-01"),
                PersonId = "999",
                Address = "Some Address",
                Phone = "099999999",
                Password = "secure",
                State = true
            };

            var response = await _client.PostAsJsonAsync("/Client", clientCreateRequest);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            _factory.ClientRepositoryMock.Verify(
                repo => repo.Add(It.Is<Client>(c => c.PersonId == "999")),
                Times.Once()
            );

            Assert.NotNull(response.Headers.Location);
        }

        [Fact]
        public async Task PutClient_ShouldReturn200AndClientResponse_WhenSuccessful()
        {
            long clientId = 50;
            var clientToFind = new Client("Old Name", EGender.M, DateTime.Parse("1980-01-01"), "50", "Old Address", "111", "123", true);
            clientToFind.Id = clientId;

            _factory.ClientRepositoryMock
                .Setup(repo => repo.FindByIdAsync(clientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientToFind);

            // --- Lógica del Test ---
            var updateClientRequest = new UpdateClientRequest
            {
                Name = "Updated Name",
                Address = "New Address",
                Phone = "0987654321",
                State = false
            };

            var response = await _client.PutAsJsonAsync($"/Client/{clientId}", updateClientRequest, _jsonOptions);

            response.EnsureSuccessStatusCode();
            Assert.Equal(StatusCodes.Status200OK, (int)response.StatusCode);

            _factory.UnitOfWorkMock.Verify(
                uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once()
            );

            _factory.ClientRepositoryMock.Verify(
                repo => repo.FindByIdAsync(clientId, It.IsAny<CancellationToken>()),
                Times.Once()
            );

            var clientResponse = await response.Content.ReadFromJsonAsync<ClientResponse>(_jsonOptions);
            Assert.NotNull(clientResponse);
            Assert.Equal("Updated Name", clientResponse.Name);
        }

        [Fact]
        public async Task PutClient_ShouldReturn404NotFound_WhenClientDoesNotExist()
        {
            long nonExistentId = 999;

            _factory.ClientRepositoryMock
                .Setup(repo => repo.FindByIdAsync(nonExistentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Client)null!);

            var updateClientRequest = new UpdateClientRequest
            {
                Name = "Any Name",
                Address = "Any Address"
            };

            var response = await _client.PutAsJsonAsync($"/Client/{nonExistentId}", updateClientRequest, _jsonOptions);

            Assert.Equal(StatusCodes.Status404NotFound, (int)response.StatusCode);

            _factory.ClientRepositoryMock.Verify(
                repo => repo.Add(It.IsAny<Client>()),
                Times.Never()
            );
        }

        [Fact]
        public async Task DeleteClient_ShouldReturn204NoContent_WhenSuccessful()
        {
            long clientId = 15;
            var existingClient = new Client("To Delete", EGender.M, DateTime.Now, "15", "Address", "111", "123", true);
            existingClient.Id = clientId;

            _factory.ClientRepositoryMock
                .Setup(repo => repo.FindByIdAsync(clientId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingClient);

            _factory.ClientRepositoryMock
                .Setup(repo => repo.Remove(It.Is<Client>(c => c.Id == clientId)));

            var response = await _client.DeleteAsync($"/Client/{clientId}");

            Assert.Equal(StatusCodes.Status204NoContent, (int)response.StatusCode);

            _factory.ClientRepositoryMock.Verify(
                repo => repo.Remove(It.Is<Client>(c => c.Id == clientId)),
                Times.Once()
            );

            _factory.UnitOfWorkMock.Verify(
                uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once()
            );
        }

        [Fact]
        public async Task DeleteClient_ShouldReturn404NotFound_WhenClientDoesNotExist()
        {
            long nonExistentId = 999;
            _factory.ClientRepositoryMock
                .Setup(repo => repo.FindByIdAsync(nonExistentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Client)null!);

            var response = await _client.DeleteAsync($"/Client/{nonExistentId}");

            Assert.Equal(StatusCodes.Status404NotFound, (int)response.StatusCode);

            _factory.ClientRepositoryMock.Verify(
                repo => repo.Remove(It.IsAny<Client>()),
                Times.Never()
            );

            _factory.UnitOfWorkMock.Verify(
                uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Never()
            );
        }
    }
}