namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;
    using StrongMe.Web.ViewModels.Trainee.PersonalPrograms;

    public class PersonalProgramService : IPersonalProgramService
    {
        private readonly IDeletableEntityRepository<PersonalProgram> personalProgramsRepository;

        public PersonalProgramService(IDeletableEntityRepository<PersonalProgram> personalProgramsRepository)
        {
            this.personalProgramsRepository = personalProgramsRepository;
        }

        public async Task CreateAsync(CreatePersonalProgramInputModel input, string userId)
        {
            var personalProgram = new PersonalProgram
            {
                Description = input.Description,
                TraineeId = userId,
            };

            await this.personalProgramsRepository.AddAsync(personalProgram);
            await this.personalProgramsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var personalProgram = this.personalProgramsRepository.All().FirstOrDefault(x => x.Id == id);
            this.personalProgramsRepository.Delete(personalProgram);
            await this.personalProgramsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(string userId)
        {
            var personalPrograms = this.personalProgramsRepository.AllAsNoTracking()
                .Where(x => x.TraineeId == userId)
                .OrderBy(x => x.TemplateProgram.Name)
                .To<T>().ToList();

            return personalPrograms;
        }

        public T GetById<T>(int id)
        {
            var personalProgram = this.personalProgramsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return personalProgram;
        }

        public async Task UpdateAsync(int id, EditPersonalProgramInputModel input)
        {
            var personalProgram = this.personalProgramsRepository.All().FirstOrDefault(x => x.Id == id);
            personalProgram.Description = input.Description;

            await this.personalProgramsRepository.SaveChangesAsync();
        }
    }
}
