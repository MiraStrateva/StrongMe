namespace StrongMe.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Moq;
    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;
    using StrongMe.Web.ViewModels.Trainee.Trainings;
    using Xunit;

    public class TrainingsServiceTests
    {
        [Fact]
        public void GetAllShould_ReturnExpectedObjectCount_WhenCorrectParametersArePassed()
        {
            // Arrange
            var trainingRepository = new Mock<IDeletableEntityRepository<Training>>();

            var userId = Guid.NewGuid().ToString();
            var count = 10;
            var trainings = this.GetTrainings(count, userId);
            trainingRepository.Setup(r => r.AllAsNoTracking()).Returns(trainings.AsQueryable());

            var service = new TrainingsService(trainingRepository.Object);
            var pageSize = 2;
            var page = 1;
            int expectedCount = Math.Min(pageSize, count - (page * pageSize));
            AutoMapperConfig.RegisterMappings(typeof(BaseTrainingInputModel).GetTypeInfo().Assembly);

            // Act
            var resultExercises = service.GetAll<TrainingInputModel>(page, pageSize, userId);

            // Assert
            Assert.Equal(expectedCount, resultExercises.Count());
        }

        [Fact]
        public void GetAllShould_ReturnExpectedResult_WhenCorrectParametersArePassed()
        {
            // Arrange
            var trainingRepository = new Mock<IDeletableEntityRepository<Training>>();

            var userId = Guid.NewGuid().ToString();
            var count = 10;
            var trainings = this.GetTrainings(count, userId);
            trainingRepository.Setup(r => r.AllAsNoTracking()).Returns(trainings.AsQueryable());

            var service = new TrainingsService(trainingRepository.Object);
            var pageSize = 2;
            var page = 1;
            int toSkip = (page - 1) * pageSize;

            var expectedResult = trainings
                       .OrderByDescending(x => x.Date)
                       .Skip(toSkip)
                       .Take(pageSize)
                       .Select(t => new TrainingInputModel()
                        {
                           Date = t.Date,
                           PersonalProgramId = t.PersonalProgramId,
                           Id = t.Id,
                           TraineeId = t.TraineeId,
                           PersonalProgramName = t.PersonalProgram.TemplateProgram.Name,
                        })
                       .ToList();

            // Act
            AutoMapperConfig.RegisterMappings(typeof(BaseTrainingInputModel).GetTypeInfo().Assembly);
            var resultTrainings = service.GetAll<TrainingInputModel>(page, pageSize, userId);

            // Assert
            Assert.Equal(expectedResult.FirstOrDefault().Date, resultTrainings.FirstOrDefault().Date);
        }

        [Fact]
        public void GetByID_ShouldReturnNull_WhenCalledWithNonExistingId()
        {
            // Arrange
            var trainingRepository = new Mock<IDeletableEntityRepository<Training>>();
            var userId = Guid.NewGuid().ToString();
            var count = 5;
            var trainings = this.GetTrainings(count, userId);
            trainingRepository.Setup(r => r.AllAsNoTracking()).Returns(trainings.AsQueryable());

            var service = new TrainingsService(trainingRepository.Object);

            // Act
            var result = service.GetById<TrainingInputModel>(6);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetByID_ShouldReturnCorrectTraining_WhenCalledWithExistingId()
        {
            // Arrange
            var trainingRepository = new Mock<IDeletableEntityRepository<Training>>();
            var userId = Guid.NewGuid().ToString();
            var count = 5;
            var trainings = this.GetTrainings(count, userId);
            trainingRepository.Setup(r => r.AllAsNoTracking()).Returns(trainings.AsQueryable());

            var service = new TrainingsService(trainingRepository.Object);
            var testId = 3;
            var expected = trainings
                .Where(t => t.Id == testId)
                .Select(t => new TrainingInputModel()
                {
                    Date = t.Date,
                    PersonalProgramId = t.PersonalProgramId,
                    Id = t.Id,
                    TraineeId = t.TraineeId,
                    PersonalProgramName = t.PersonalProgram.TemplateProgram.Name,
                })
                .FirstOrDefault();

            // Act
            var result = service.GetById<TrainingInputModel>(testId);

            // Assert
            Assert.Equal(expected.Id, result.Id);
        }

        [Fact]
        public void GetCount_ShouldReturnCorrectNumber()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            int recordsCount = 3;
            var trainingRepository = new Mock<IDeletableEntityRepository<Training>>();
            trainingRepository.Setup(r => r.AllAsNoTracking()).Returns(this.GetTrainings(recordsCount, userId).AsQueryable());

            var service = new TrainingsService(trainingRepository.Object);

            // Act & Assert
            Assert.Equal(3, service.GetCount());
            trainingRepository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        [Fact]
        public void GetCountShouldReturnZeroWhenNoRecords()
        {
            // Arrange
            var trainingRepository = new Mock<IDeletableEntityRepository<Training>>();
            trainingRepository.Setup(r => r.AllAsNoTracking()).Returns(new List<Training>().AsQueryable());

            var service = new TrainingsService(trainingRepository.Object);

            // Act & Assert
            Assert.Equal(0, service.GetCount());
            trainingRepository.Verify(x => x.AllAsNoTracking(), Times.Once);
        }

        private IEnumerable<Training> GetTrainings(int count, string userId)
        {
            List<Training> trainings = new List<Training>();

            for (int i = 1; i <= count; i++)
            {
                trainings.Add(new Training()
                {
                    Id = i,
                    TraineeId = userId,
                    Date = DateTime.Now,
                    PersonalProgramId = 1, 
                    PersonalProgram = new PersonalProgram()
                    {
                        Id = 1, 
                        TemplateProgram = new TemplateProgram()
                        {
                            Id = 1, 
                            Name = "TemplateProgram",
                        }
                    }
                });
            }

            return trainings;
        }
    }
}
