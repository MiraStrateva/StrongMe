namespace StrongMe.Web.ViewModels.Trainee.Trainings
{
    using AutoMapper;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class TrainingInputModel : BaseTrainingInputModel, IMapFrom<Training>, IMapTo<Training>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string TraineeId { get; set; }

        public string PersonalProgramName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Training, TrainingInputModel>()
                .ForMember(x => x.PersonalProgramName, opt =>
                    opt.MapFrom(x =>
                        x.PersonalProgram.TemplateProgram.Name));
        }
    }
}
