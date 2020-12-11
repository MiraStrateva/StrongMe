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

            var detailSortOrder = 1;
            foreach (var detail in input.Details)
            {
                templateProgram.Details.Add(new TemplateProgramDetail
                {
                    SortOrder = detailSortOrder,
                    ExerciseId = detail.ExerciseId,
                    SeriesCount = detail.SeriesCount,
                    Repetitions = detail.Repetitions,
                    Break = detail.Break,
                });
                detailSortOrder++;
            }

            await this.templateProgramsRepository.AddAsync(templateProgram);
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
    }
}
