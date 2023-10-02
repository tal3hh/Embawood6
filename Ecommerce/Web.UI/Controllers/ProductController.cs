using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceLayer.DTOs;
using ServiceLayer.Services.Interfaces;

namespace Web.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productservice;
        private readonly IProductCommentService _commentservice;
        private readonly ICategoryService _categoryservice;

        public ProductController(IProductService productservice, IProductCommentService commentservice, ICategoryService categoryservice)
        {
            _productservice = productservice;
            _commentservice = commentservice;
            _categoryservice = categoryservice;
        }



        public async Task<IActionResult> AllList(string search, string sort, int page = 1, int take = 12)
        {
            ViewBag.Name = String.IsNullOrEmpty(sort) ? "name_desc" : "name_asc";
            ViewBag.Price = (sort == "price_desc") ? "price_asc" : "price_desc";
            ViewBag.Count = (sort == "count_desc") ? "count_asc" : "count_desc";

            //View send
            TempData["sort"] = sort;

            var products = await _productservice.AllHomeFilterAsync(search, sort, page, take);

            return View(products);
        }


        #region ProductCategory

        public async Task<IActionResult> ProductCategories(int id)
        {
            var list = await _categoryservice.CategoryProducts(id);

            return View(list);
        }


        #endregion


        #region DeatilandComment
        public async Task<IActionResult> ProductDetail(int id)
        {
            TempData["ProId"] = id;

            var product = await _productservice.GetByIdAsync(id);
            var comments = await _commentservice.ProductComment(id);

            return View((product, new ProductCommentCreateDto(), comments));
        }

        [HttpPost]
        public async Task<IActionResult> ProductDetail([Bind(Prefix = "Item2")] ProductCommentCreateDto dto)
        {
            if (TempData["ProId"] == null) return RedirectToAction("Index", "Home");

            dto.ProductId = (int)TempData["ProId"];

            var product = await _productservice.GetByIdAsync(dto.ProductId);
            var comments = await _commentservice.ProductComment(dto.ProductId);

            if (!ModelState.IsValid) return RedirectToAction("ProductDetail");

            await _commentservice.CreateAsync(dto);

            return RedirectToAction("ProductDetail");
        }

        #endregion

    }
}
