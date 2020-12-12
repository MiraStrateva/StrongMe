namespace StrongMe.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using StrongMe.Data.Models;
    using StrongMe.Services.Data;

    public class BodyPartsController : AdministrationController
    {
        private readonly IBodyPartsService bodyPartsService;

        public BodyPartsController(IBodyPartsService bodyPartsService)
        {
            this.bodyPartsService = bodyPartsService;
        }

        public IActionResult Index()
        {
            return this.View(this.bodyPartsService.GetAllWithDeleted());
        }

        public IActionResult Details(int id)
        {
            var bodyPart = this.bodyPartsService.GetById(id);
            if (bodyPart == null)
            {
                return this.NotFound();
            }

            return this.View(bodyPart);
        }

        public IActionResult Create()
        {
            return this.View(new BodyPart());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BodyPart input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.bodyPartsService.CreateAsync(input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult Edit(int id)
        {
            var bodyPart = this.bodyPartsService.GetById(id);

            if (bodyPart == null)
            {
                return this.NotFound();
            }

            return this.View(bodyPart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BodyPart input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.bodyPartsService.UpdateAsync(id, input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult Delete(int id)
        {
            var bodyPart = this.bodyPartsService.GetById(id);
            if (bodyPart == null)
            {
                return this.NotFound();
            }

            return this.View(bodyPart);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.bodyPartsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
