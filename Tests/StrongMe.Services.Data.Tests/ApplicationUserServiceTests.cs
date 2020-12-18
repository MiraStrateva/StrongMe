namespace StrongMe.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using StrongMe.Data;
    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;
    using StrongMe.Data.Repositories;
    using Xunit;

    public class ApplicationUserServiceTests
    {
        [Fact]
        public void IsInstructorCodeValidShouldReturnFalseWhenNoRecords()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "StrongMeTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);

            using var repository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new ApplicationUserService(repository);
            var codeToCheck = "12AB34";

            // Act & Assert
            Assert.False(service.IsInstructorCodeValid(codeToCheck));
        }

        [Fact]
        public async Task IsInstructorCodeValidShouldReturnFalseWhenNoRecordsWithThisCode()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "StrongMeTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Users.Add(new ApplicationUser());
            dbContext.Users.Add(new ApplicationUser());
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new ApplicationUserService(repository);
            var codeToCheck = "12AB34";

            // Act & Assert
            Assert.False(service.IsInstructorCodeValid(codeToCheck));
        }

        [Fact]
        public void IsInstructorCodeValidShouldReturnTrueWhenRecordsWithThisCodeIsFound()
        {
            // Arrange
            var repository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            repository.Setup(r => r.AllAsNoTracking()).Returns(new List<ApplicationUser>
                                                        {
                                                            new ApplicationUser()
                                                            {
                                                                UserName = "User Name 1",
                                                                Code = "123456",
                                                            },
                                                            new ApplicationUser()
                                                            {
                                                                UserName = "User Name 2",
                                                                Code = "789098",
                                                            },
                                                            new ApplicationUser()
                                                            {
                                                                UserName = "User Name 3",
                                                                Code = "12AB34",
                                                            },
                                                        }.AsQueryable());
            var service = new ApplicationUserService(repository.Object);
            var codeToCheck = "12AB34";

            // Act & Assert
            Assert.True(service.IsInstructorCodeValid(codeToCheck));
            repository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }
    }
}
