using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using RandomUserService.Domain.Repositories.Interfaces;
using RandomUserService.Infrastructure.Clients.Interfaces;
using RandomUserService.Infrastructure.Configuration.Providers.Interfaces;
using RandomUserService.Infrastructure.Persistence;
using RandomUserService.Infrastructure.Repositories;
using RandomUserService.Infrastructure.Schedulers;
using RandomUserService.Tests.Builders;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RandomUserService.Tests
{
    public class RandomUserServiceSchedulerTests
    {
        private RandomUserServiceScheduler CreateScheduler()
        {
            var logger = NullLogger<RandomUserServiceScheduler>.Instance;

            var scopeFactory = new Mock<IServiceScopeFactory>().Object;

            return new RandomUserServiceScheduler(logger, scopeFactory);
        }

        [Fact]
        public void GetStatus_ShouldReturnRunning_WhenSchedulerNotPaused()
        {
            // Arrange
            var scheduler = CreateScheduler();

            // Act
            var status = scheduler.GetStatus();

            //Assert
            Assert.Equal("Status: Running.", status);
        }

        [Fact]
        public void GetStatus_ShouldReturnStopped_WhenSchedulerPaused()
        {
            // Arrange
            var scheduler = CreateScheduler();

            //Act
            scheduler.Pause();
            var status = scheduler.GetStatus();

            //Assert
            Assert.Equal("Status: Stopped.", status);
        }

        [Fact]
        public void Resume_ShouldSetStatusToRunning()
        {
            // Arrange
            var scheduler = CreateScheduler();

            // Act
            scheduler.Pause();

            var pausedStatus = scheduler.GetStatus();

            scheduler.Resume();

            var resumedStatus = scheduler.GetStatus();

            // Assert
            Assert.Equal("Status: Stopped.", pausedStatus);
            Assert.Equal("Status: Running.", resumedStatus);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldFetchUserAndSaveToRepository_WhenNotPaused()
        {
            // Arrange
            var location = new LocationBuilder()
                .WithStreet(2, "street")
                .WithCoordinates(23.00, 21.00)
                .WithTimezone("000", "desc")
                .Build();

            var user = new UserBuilder()
                .WithName("John", "Doe", "Mr")
                .WithLocation(location)
                .WithExternalId("ddd", "123")
                .WithPicture("large", "medium", "thumbnail")
                .WithDateOfBirth(DateTime.Now, 20)
                .Build();

            var mockClient = new Mock<IRandomUserClient>();
            mockClient.Setup(c => c.GetRandomUserAsync()).ReturnsAsync(user);

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.AddAsync(user)).Returns(Task.CompletedTask);

            var mockConfig = new Mock<ISchedulerConfigurationProvider>();
            mockConfig.Setup(c => c.GetSchedulerInterval()).Returns(1);

            var services = new ServiceCollection();
            services.AddSingleton(mockClient.Object);
            services.AddSingleton(mockRepo.Object);
            services.AddSingleton(mockConfig.Object);

            var serviceProvider = services.BuildServiceProvider();

            var logger = NullLogger<RandomUserServiceScheduler>.Instance;
            var scopeFactory = new TestScopeFactory(serviceProvider);
            var scheduler = new RandomUserServiceScheduler(logger, scopeFactory);

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(500);

            // Act
            await scheduler.StartAsync(cts.Token);

            // Assert
            mockClient.Verify(c => c.GetRandomUserAsync(), Times.AtLeastOnce);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Domain.Entities.User>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldNotSaveUser_WhenSchedulerPaused()
        {
            // Arrange
            var location = new LocationBuilder()
                .WithStreet(2, "street")
                .WithCoordinates(23.00, 21.00)
                .WithTimezone("000", "desc")
                .Build();

            var user = new UserBuilder()
                .WithName("John", "Doe", "Mr")
                .WithLocation(location)
                .WithExternalId("ddd", "123")
                .WithPicture("large", "medium", "thumbnail")
                .WithDateOfBirth(DateTime.Now, 20)
                .Build();

            var mockClient = new Mock<IRandomUserClient>();
            mockClient.Setup(c => c.GetRandomUserAsync()).ReturnsAsync(user);

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.User>())).Returns(Task.CompletedTask);

            var mockConfig = new Mock<ISchedulerConfigurationProvider>();
            mockConfig.Setup(c => c.GetSchedulerInterval()).Returns(1);

            var services = new ServiceCollection();
            services.AddSingleton(mockClient.Object);
            services.AddSingleton(mockRepo.Object);
            services.AddSingleton(mockConfig.Object);

            var scopeFactory = new TestScopeFactory(services.BuildServiceProvider());
            var logger = NullLogger<RandomUserServiceScheduler>.Instance;
            var scheduler = new RandomUserServiceScheduler(logger, scopeFactory);

            // Act
            scheduler.Pause();

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(500);

            await scheduler.StartAsync(cts.Token);

            // Assert
            mockClient.Verify(c => c.GetRandomUserAsync(), Times.Never);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Domain.Entities.User>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldSaveUserToDatabase_WhenNotPaused()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RandomUserServiceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            await using var context = new RandomUserServiceDbContext(options);

            var location = new LocationBuilder()
                .WithStreet(2, "Street")
                .WithCoordinates(23.00, 21.00)
                .WithTimezone("000", "desc")
                .Build();

            var user = new UserBuilder()
                .WithName("John", "Doe", "Mr")
                .WithLocation(location)
                .WithExternalId("ddd", "123")
                .WithPicture("large", "medium", "thumbnail")
                .WithDateOfBirth(DateTime.Now, 20)
                .Build();

            var mockClient = new Mock<IRandomUserClient>();
            mockClient.Setup(c => c.GetRandomUserAsync()).ReturnsAsync(user);

            var mockConfig = new Mock<ISchedulerConfigurationProvider>();
            mockConfig.Setup(c => c.GetSchedulerInterval()).Returns(1);

            var repo = new UserRepository(context);

            var services = new ServiceCollection();
            services.AddSingleton(mockClient.Object);
            services.AddSingleton<IUserRepository>(repo);
            services.AddSingleton(mockConfig.Object);

            var provider = services.BuildServiceProvider();
            var scopeFactory = new TestScopeFactory(provider);
            var logger = NullLogger<RandomUserServiceScheduler>.Instance;

            var scheduler = new RandomUserServiceScheduler(logger, scopeFactory);

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(500);

            // Act
            try
            {
                await scheduler.StartAsync(cts.Token);
            }
            catch (TaskCanceledException)
            {
                
            }

            // Assert
            var savedUser = context.Users.Include(u => u.Location).FirstOrDefault();
            Assert.NotNull(savedUser);
            Assert.Equal("John", savedUser.Name.First);
            Assert.Equal("Doe", savedUser.Name.Last);
            Assert.Equal("Mr", savedUser.Name.Title);
        }
    }
    public class TestScopeFactory : IServiceScopeFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public TestScopeFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IServiceScope CreateScope() => _serviceProvider.CreateScope();
    }
}
