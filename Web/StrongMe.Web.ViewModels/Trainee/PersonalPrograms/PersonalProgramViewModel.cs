namespace StrongMe.Web.ViewModels.Trainee.PersonalPrograms
{
    using AutoMapper;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class PersonalProgramViewModel : IMapFrom<PersonalProgram>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string TemplateProgramName { get; set; }

        public int ExerciseCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PersonalProgram, PersonalProgramViewModel>()
                .ForMember(x => x.ExerciseCount, opt =>
                    opt.MapFrom(x =>
                        x.TemplateProgram.Details.Count));
        }
    }
}
