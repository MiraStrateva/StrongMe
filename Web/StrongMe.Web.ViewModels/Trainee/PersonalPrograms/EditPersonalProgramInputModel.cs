namespace StrongMe.Web.ViewModels.Trainee.PersonalPrograms
{
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class EditPersonalProgramInputModel : BasePersonalProgramInputModel, IMapFrom<PersonalProgram>, IMapTo<PersonalProgram>
    {
        public int Id { get; set; }
    }
}
