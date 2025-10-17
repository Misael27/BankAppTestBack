using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using BankAppTestBack.Domain.Repositories;
using Moq;
using BankAppTestBack.Infrastructure.Repositories;

namespace BankAppTestBack.IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
        where TProgram : class
    {
        public Mock<IClientRepository> ClientRepositoryMock { get; private set; } = new Mock<IClientRepository>();
        public Mock<IUnitOfWork> UnitOfWorkMock { get; private set; } = new Mock<IUnitOfWork>();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IClientRepository));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddScoped(sp => ClientRepositoryMock.Object);

                var uowDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IUnitOfWork));
                if (uowDescriptor != null) services.Remove(uowDescriptor);
                services.AddScoped(sp => UnitOfWorkMock.Object);
            });
        }

    }
}