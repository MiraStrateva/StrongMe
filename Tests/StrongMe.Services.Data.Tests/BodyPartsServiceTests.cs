namespace StrongMe.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;
    using Xunit;

    public class BodyPartsServiceTests
    {
        [Fact]
        public void GetAllAsKeyValuePairsShoulsReturnOrderedByNameCollection_WhenDataAvailable()
        {
            // Arrange
            var repository = new Mock<IDeletableEntityRepository<BodyPart>>();

            IEnumerable<BodyPart> bodyParts = this.GetBodyParts(3);
            repository.Setup(c => c.AllAsNoTracking()).Returns(bodyParts.AsQueryable());

            var service = new BodyPartsService(repository.Object);
            var expectedKeyValuePairs = bodyParts.Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));

            // Act
            var resultKeyValuePairs = service.GetAllAsKeyValuePairs();

            // Assert
            Assert.Equal(expectedKeyValuePairs, resultKeyValuePairs);
        }

        [Fact]
        public void GetAllAsKeyValuePairsShoulsReturnEmptyCollection_WhenNoData()
        {
            // Arrange
            var repository = new Mock<IDeletableEntityRepository<BodyPart>>();

            IEnumerable<BodyPart> bodyParts = this.GetBodyParts(0);
            repository.Setup(c => c.AllAsNoTracking()).Returns(bodyParts.AsQueryable());

            var service = new BodyPartsService(repository.Object);

            // Act
            var resultKeyValuePairs = service.GetAllAsKeyValuePairs();

            // Assert
            Assert.Empty(resultKeyValuePairs);
        }

        [Fact]
        public void GetByIdShould_ReturnCorrectItem_WhenValidBodyPartIdIsPassed()
        {
            // Arrange
            var repository = new Mock<IDeletableEntityRepository<BodyPart>>();

            IEnumerable<BodyPart> bodyParts = this.GetBodyParts(3);
            repository.Setup(c => c.AllAsNoTrackingWithDeleted()).Returns(bodyParts.AsQueryable());

            var service = new BodyPartsService(repository.Object);
            var testId = 1;
            var expectedBodyPart = bodyParts.Where(b => b.Id == testId).FirstOrDefault();

            // Act
            var bodyPart = service.GetById(testId);

            // Assert
            Assert.Equal(expectedBodyPart, bodyPart);
        }

        [Fact]
        public void GetByIdShould_ReturnNoBodyPart_WhenBodyPartIsNotFound()
        {
            // Arrange
            var repository = new Mock<IDeletableEntityRepository<BodyPart>>();

            IEnumerable<BodyPart> bodyParts = this.GetBodyParts(3);
            repository.Setup(c => c.AllAsNoTrackingWithDeleted()).Returns(bodyParts.AsQueryable());

            var service = new BodyPartsService(repository.Object);
            var testId = 4;

            // Act
            var bodyPart = service.GetById(testId);

            // Assert
            Assert.Null(bodyPart);
        }

        private IEnumerable<BodyPart> GetBodyParts(int count)
        {
            List<BodyPart> bodyParts = new List<BodyPart>();

            for (int i = 1; i <= count; i++)
            {
                bodyParts.Add(new BodyPart()
                {
                    Id = i,
                    Name = "Name " + i,
                });
            }

            return bodyParts;
        }
    }
}
