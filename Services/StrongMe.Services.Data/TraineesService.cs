namespace StrongMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;
    using StrongMe.Web.ViewModels.Instructor.Trainees;
    using StrongMe.Web.ViewModels.Trainee.PersonalPrograms;

    public class TraineesService : ITraineesService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUserRepository;
        private readonly IPersonalProgramsService personalProgramsService;

        public TraineesService(
            IDeletableEntityRepository<ApplicationUser> applicationUserRepository,
            IPersonalProgramsService personalProgramsService)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.personalProgramsService = personalProgramsService;
        }

        public IEnumerable<TraineePersonalProgramsViewModel> GetAll(string userId)
        {
            var trainer = this.applicationUserRepository.AllAsNoTracking()
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var trainees = this.applicationUserRepository.AllAsNoTracking()
                .Where(x => x.Code == trainer.Code && x.Id != userId)
                .OrderBy(x => x.UserName)
                .ToList();

            var traineePersonalPrograms = new List<TraineePersonalProgramsViewModel>();
            foreach (var trainee in trainees)
            {
                traineePersonalPrograms.Add(new TraineePersonalProgramsViewModel()
                {
                    TraineeId = trainee.Id,
                    TraineeName = trainee.UserName,
                    PersonalPrograms = this.personalProgramsService.GetAll<PersonalProgramViewModel>(trainee.Id).ToList(),
                });
            }

            return traineePersonalPrograms;
        }
    }
}
