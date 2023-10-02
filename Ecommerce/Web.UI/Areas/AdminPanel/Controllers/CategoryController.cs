using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using ServiceLayer.Services.Interfaces;
using System.Reflection.Metadata;

namespace Web.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryservice;

        public CategoryController(ICategoryService categoryservice)
        {
            _categoryservice = categoryservice;
        }


        public async Task<IActionResult> List()
        {
            var categories = await _categoryservice.GetAllAsync();

            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            var list = await _categoryservice.GetAllAsync();

            return View((new CategoryCreateDto(), list));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] CategoryCreateDto dto)
        {
            var list = await _categoryservice.GetAllAsync();
            if (!ModelState.IsValid) return View((dto, list));
            

            await _categoryservice.CreateAsync(dto);

            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Update(int id)
        {
            var dto = await _categoryservice.GetByIdUpdate(id);
            var list = await _categoryservice.GetAllAsync();

            return View((dto, list));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item1")] CategoryUpdateDto dto)
        {
            var list = await _categoryservice.GetAllAsync();
            if (!ModelState.IsValid) return View((dto, list));

            await _categoryservice.Update(dto);

            return RedirectToAction("Update");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                await _categoryservice.Remove(id);
                return RedirectToAction("List");
            }

            return RedirectToAction("ErrorPage", "Home", new { area = "" });
        }

    }
}
