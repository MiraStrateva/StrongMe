namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;
    using StrongMe.Web.ViewModels.Trainee.Trainings;

    public class TrainingsService : ITrainingsService
    {
        private readonly IDeletableEntityRepository<Training> trainingsRepository;
        private readonly IMapper mapper;

        public TrainingsService(IDeletableEntityRepository<Training> trainingsRepository)
        {
            this.trainingsRepository = trainingsRepository;
            this.mapper = AutoMapperConfig.MapperInstance;
        }

        public async Task CreateAsync(CreateTrainingInputModel input, string userId)
        {
            var training = this.mapper.Map<Training>(input);
            training.TraineeId = userId;

            await this.trainingsRepository.AddAsync(training);
            await this.trainingsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var training = this.trainingsRepository.All().FirstOrDefault(x => x.Id == id);
            this.trainingsRepository.Delete(training);
            await this.trainingsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage, string userId)
        {
            var trainings = this.trainingsRepository.AllAsNoTracking()
                .Where(x => x.TraineeId == userId)
                .OrderByDescending(x => x.Date)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return trainings;
        }

        public T GetById<T>(int id)
        {
            var training = this.trainingsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return training;
        }

        public int GetCount()
        {
            return this.trainingsRepository.AllAsNoTracking().Count();
        }

        public async Task UpdateAsync(TrainingInputModel input)
        {
            var training = this.mapper.Map<Training>(input);

            this.trainingsRepository.Update(training);
            await this.trainingsRepository.SaveChangesAsync();
        }
    }
}
