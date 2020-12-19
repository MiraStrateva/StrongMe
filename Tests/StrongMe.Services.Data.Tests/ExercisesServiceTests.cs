namespace StrongMe.Services.Data.Tests
{
    using System;
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
            var userId = Guid.NewGuid().ToString();
            var recordsCount = 3;
            exerciseRepository.Setup(r => r.AllAsNoTracking()).Returns(this.GetExercises(recordsCount, userId).AsQueryable());

            var service = new ExercisesService(exerciseRepository.Object, imageRepository.Object);

            // Act & Assert
            Assert.Equal(recordsCount, service.GetCount(userId));
            exerciseRepository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        [Fact]
        public void GetCountShouldReturnZeroWhenNoRecordsInTable()
        {
            // Arrange
            var exerciseRepository = new Mock<IDeletableEntityRepository<Exercise>>();
            var imageRepository = new Mock<IRepository<Image>>();
            var userId = Guid.NewGuid().ToString();
            exerciseRepository.Setup(r => r.AllAsNoTracking()).Returns(new List<Exercise>().AsQueryable());

            var service = new ExercisesService(exerciseRepository.Object, imageRepository.Object);

            // Act & Assert
            Assert.Equal(0, service.GetCount(userId));
            exerciseRepository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        [Fact]
        public void GetCountShouldReturnZeroWhenNoRecordsForCurrentUser()
        {
            // Arrange
            var exerciseRepository = new Mock<IDeletableEntityRepository<Exercise>>();
            var imageRepository = new Mock<IRepository<Image>>();
            var userId = Guid.NewGuid().ToString();
            var recordCount = 2;
            exerciseRepository.Setup(r => r.AllAsNoTracking()).Returns(this.GetExercises(recordCount, userId).AsQueryable());

            var service = new ExercisesService(exerciseRepository.Object, imageRepository.Object);

            // Act & Assert
            Assert.Equal(0, service.GetCount(Guid.NewGuid().ToString()));
            exerciseRepository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        private IEnumerable<Exercise> GetExercises(int count, string userId)
        {
            List<Exercise> exercises = new List<Exercise>();

            for (int i = 1; i <= count; i++)
            {
                exercises.Add(new Exercise()
                {
                    Id = i,
                    Name = "Name " + i,
                    TrainerId = userId,
                });
            }

            return exercises;
        }
    }
}
