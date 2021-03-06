﻿namespace StrongMe.Web.ViewModels.Instructor.TemplatePrograms
{
    using System.Collections.Generic;

    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;

    public class SingleTemplateProgramViewModel : IMapFrom<TemplateProgram>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TemplateProgramDetailInputModel> Details { get; set; }
    }
}
