namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;
    using StrongMe.Web.ViewModels.Instructor.TemplatePrograms;

    public class TemplateProgramsService : ITemplateProgramsService
    {
        private readonly IDeletableEntityRepository<TemplateProgram> templateProgramsRepository;

        public TemplateProgramsService(IDeletableEntityRepository<TemplateProgram> templateProgramsRepository)
        {
            this.templateProgramsRepository = templateProgramsRepository;
        }

        public async Task CreateAsync(CreateTemplateProgramInputModel input, string userId)
        {
            var templateProgram = new TemplateProgram
            {
                Name = input.Name,
                TrainerId = userId,
            };

            foreach (var detail in input.Details)
            {
                templateProgram.Details.Add(new TemplateProgramDetail
                {
                    ExerciseId = detail.ExerciseId,
                    SeriesCount = detail.SeriesCount,
                    Repetitions = detail.Repetitions,
                    Break = detail.Break,
                });
            }

            await this.templateProgramsRepository.AddAsync(templateProgram);
            await this.templateProgramsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var templateProgram = this.templateProgramsRepository.All().FirstOrDefault(x => x.Id == id);
            this.templateProgramsRepository.Delete(templateProgram);
            await this.templateProgramsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage, string userId)
        {
            var templatePrograms = this.templateProgramsRepository.AllAsNoTracking()
                .Where(x => x.TrainerId == userId)
                .OrderBy(x => x.Name)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();

            return templatePrograms;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.templateProgramsRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public T GetById<T>(int id)
        {
            var templateProgram = this.templateProgramsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return templateProgram;
        }

        public int GetCount()
        {
            return this.templateProgramsRepository.All().Count();
        }

        public async Task UpdateAsync(int id, EditTemplateProgramInputModel input)
        {
            var templateProgram = this.templateProgramsRepository.All().FirstOrDefault(x => x.Id == id);
            templateProgram.Name = input.Name;

            if (input.Details != null)
            {
                foreach (var detail in input.Details)
                {
                    if (detail.Id == 0)
                    {
                        templateProgram.Details.Add(new TemplateProgramDetail
                        {
                            ExerciseId = detail.ExerciseId,
                            SeriesCount = detail.SeriesCount,
                            Repetitions = detail.Repetitions,
                            Break = detail.Break,
                        });
                    }
                }
            }

            await this.templateProgramsRepository.SaveChangesAsync();
        }
    }
}
