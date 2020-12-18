namespace StrongMe.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;
    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;
    using Xunit;

    public class ExercisesServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            // Arrange
            var exerciseRepository = new Mock<IDeletableEntityRepository<Exercise>>();
            var imageRepository = new Mock<IRepository<Image>>();
            exerciseRepository.Setup(r => r.AllAsNoTracking()).Returns(new List<Exercise>
                                                        {
                                                            new Exercise(),
                                                            new Exercise(),
                                                            new Exercise(),
                                                        }.AsQueryable());

            var service = new ExercisesService(exerciseRepository.Object, imageRepository.Object);

            // Act & Assert
            Assert.Equal(3, service.GetCount());
            exerciseRepository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        [Fact]
        public void GetCountShouldReturnZeroWhenNoRecords()
        {
            // Arrange
            var exerciseRepository = new Mock<IDeletableEntityRepository<Exercise>>();
            var imageRepository = new Mock<IRepository<Image>>();
            exerciseRepository.Setup(r => r.AllAsNoTracking()).Returns(new List<Exercise>().AsQueryable());

            var service = new ExercisesService(exerciseRepository.Object, imageRepository.Object);

            // Act & Assert
            Assert.Equal(0, service.GetCount());
            exerciseRepository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }
    }
}
