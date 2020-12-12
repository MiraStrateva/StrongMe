namespace StrongMe.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;
    using StrongMe.Web.ViewModels.Instructor.TemplatePrograms;

    public class TemplateProgramDetailsService : ITemplateProgramDetailsService
    {
        private readonly IDeletableEntityRepository<TemplateProgramDetail> templateProgramDetailRepository;

        public TemplateProgramDetailsService(IDeletableEntityRepository<TemplateProgramDetail> templateProgramDetailRepository)
        {
            this.templateProgramDetailRepository = templateProgramDetailRepository;
        }

        public async Task DeleteAsync(int id)
        {
            var templateProgramDetail = this.templateProgramDetailRepository.All().FirstOrDefault(x => x.Id == id);
            this.templateProgramDetailRepository.Delete(templateProgramDetail);
            await this.templateProgramDetailRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            var templateProgramDetail = this.templateProgramDetailRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return templateProgramDetail;
        }

        public async Task UpdateAsync(int id, EditTemplateProgramDetailInputModel input)
        {
            var templateProgramDetail = this.templateProgramDetailRepository.All().FirstOrDefault(x => x.Id == id);
            templateProgramDetail.SeriesCount = input.SeriesCount;
            templateProgramDetail.Repetitions = input.Repetitions;
            templateProgramDetail.Break = input.Break;

            await this.templateProgramDetailRepository.SaveChangesAsync();
        }
    }
}
