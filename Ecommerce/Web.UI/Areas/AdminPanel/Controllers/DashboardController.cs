using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using ServiceLayer.ModelVM.Charts;
using ServiceLayer.Services.Interfaces;
using System.Data;

namespace Web.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class DashboardController : Controller
    {
        private readonly IProductService _productservice;
        private readonly ICategoryService _categoryservice;

        public DashboardController(IProductService productservice, ICategoryService categoryservice)
        {
            _productservice = productservice;   
            _categoryservice = categoryservice;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productservice.AllAsync();
            var categories = await _categoryservice.GetAllAsync();

            return View((categories,products));
        }

        

    }
}
