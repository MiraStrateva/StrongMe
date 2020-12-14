namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;
    using StrongMe.Web.ViewModels.Trainee.Measurements;

    public class MeasurementsService : IMeasurementsService
    {
        private readonly IDeletableEntityRepository<Measurement> measurementsRepository;
        private readonly IMapper mapper;

        public MeasurementsService(IDeletableEntityRepository<Measurement> measurementsRepository)
        {
            this.measurementsRepository = measurementsRepository;
            this.mapper = AutoMapperConfig.MapperInstance;
        }

        public async Task CreateAsync(CreateMeasurmentInputModel input, string userId)
        {
            var measurement = this.mapper.Map<Measurement>(input);
            measurement.TraineeId = userId;

            await this.measurementsRepository.AddAsync(measurement);
            await this.measurementsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var measurement = this.measurementsRepository.All().FirstOrDefault(x => x.Id == id);
            this.measurementsRepository.Delete(measurement);
            await this.measurementsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage, string userId)
        {
            var measurements = this.measurementsRepository.AllAsNoTracking()
                .Where(x => x.TraineeId == userId)
                .OrderByDescending(x => x.Date)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return measurements;
        }

        public T GetById<T>(int id)
        {
            var measurement = this.measurementsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return measurement;
        }

        public int GetCount()
        {
            return this.measurementsRepository.All().Count();
        }

        public async Task UpdateAsync(MeasurementInputModel input)
        {
            var measurement = this.mapper.Map<Measurement>(input);

            this.measurementsRepository.Update(measurement);
            await this.measurementsRepository.SaveChangesAsync();
        }
    }
}
