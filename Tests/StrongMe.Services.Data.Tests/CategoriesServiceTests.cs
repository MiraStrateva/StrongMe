using Moq;
using StrongMe.Data.Common.Repositories;
using StrongMe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StrongMe.Services.Data.Tests
{
    public class CategoriesServiceTests
    {
        [Fact]
        public void GetAllAsKeyValuePairsShoulsReturnOrderedByNameCollection_WhenDataAvailable()
        {
            // Arrange
            var repository = new Mock<IDeletableEntityRepository<Category>>();

            IEnumerable<Category> categories = this.GetCategories(3);
            repository.Setup(c => c.AllAsNoTracking()).Returns(categories.AsQueryable());

            var service = new CategoriesService(repository.Object);
            var expectedKeyValuePairs = categories.Select(x => new
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
            var repository = new Mock<IDeletableEntityRepository<Category>>();

            IEnumerable<Category> categories = this.GetCategories(0);
            repository.Setup(c => c.AllAsNoTracking()).Returns(categories.AsQueryable());

            var service = new CategoriesService(repository.Object);

            // Act
            var resultKeyValuePairs = service.GetAllAsKeyValuePairs();

            // Assert
            Assert.Empty(resultKeyValuePairs);
        }

        [Fact]
        public void GetByIdShould_ReturnCorrectItem_WhenValidCategoryIdIsPassed()
        {
            // Arrange
            var repository = new Mock<IDeletableEntityRepository<Category>>();

            IEnumerable<Category> categories = this.GetCategories(3);
            repository.Setup(c => c.AllAsNoTrackingWithDeleted()).Returns(categories.AsQueryable());

            var service = new CategoriesService(repository.Object);
            var testId = 1;
            var expectedBodyPart = categories.Where(b => b.Id == testId).FirstOrDefault();

            // Act
            var bodyPart = service.GetById(testId);

            // Assert
            Assert.Equal(expectedBodyPart, bodyPart);
        }

        [Fact]
        public void GetByIdShould_ReturnNoBodyPart_WhenBodyPartIsNotFound()
        {
            // Arrange
            var repository = new Mock<IDeletableEntityRepository<Category>>();

            IEnumerable<Category> categories = this.GetCategories(3);
            repository.Setup(c => c.AllAsNoTrackingWithDeleted()).Returns(categories.AsQueryable());

            var service = new CategoriesService(repository.Object);
            var testId = 4;

            // Act
            var bodyPart = service.GetById(testId);

            // Assert
            Assert.Null(bodyPart);
        }

        private IEnumerable<Category> GetCategories(int count)
        {
            List<Category> categories = new List<Category>();

            for (int i = 1; i <= count; i++)
            {
                categories.Add(new Category()
                {
                    Id = i,
                    Name = "Name " + i,
                });
            }

            return categories;
        }
    }
}
