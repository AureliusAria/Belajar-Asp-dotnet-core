using Bloggie.Web.Controllers.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> SubmitTag(AddTagRequest addTagRequest)
        {
            if(addTagRequest.Name != null &&  addTagRequest.DisplayName != null)
            {
                var tag = new Tag
                {
                    Name = addTagRequest.Name,
                    DisplayName = addTagRequest.DisplayName,
                };

                await tagRepository.AddAsync(tag);

                return RedirectToAction("List");

            }
            
            TempData["err"] = "Semua Data Harus Terisi!!!";
            return RedirectToAction("Add");
            
        }

        [HttpGet]
        public async Task<IActionResult> List() 
        {
            //use dbcontext to read tags
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // 1st method
            //var tag = bloggieDbContext.Tags.Find(id);

            //2nd method
            var tag = await tagRepository.GetAsync(id);

            if (tag != null) 
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,

                };

                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest) 
        {
            if(editTagRequest.Name == null ||  editTagRequest.DisplayName == null)
            {
                TempData["err"] = "Semua Data Harus Terisi!!!";
                return RedirectToAction("Edit", new { id = editTagRequest.Id });

            }
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var updated = await tagRepository.UpdateAsync(tag);

            if (updated != null) 
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deleted = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deleted != null) 
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new {id =  editTagRequest.Id});
        }
    }
}
