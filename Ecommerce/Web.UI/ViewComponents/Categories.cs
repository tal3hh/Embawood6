using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace Web.UI.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public Categories(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var list = _categoryService.GetCategories();
            return View(list);
        }
    }
}
