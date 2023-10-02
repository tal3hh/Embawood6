using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;
using System.Data;

namespace Web.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class ProductCommentController : Controller
    {
        private readonly IProductCommentService _commentservice;

        public ProductCommentController(IProductCommentService commentservice)
        {
            _commentservice = commentservice;
        }


        public async Task<IActionResult> List(string search, int page = 1, int take = 15)
        {
            var list = await _commentservice.AllComments(search, page, take);

            return View(list);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _commentservice.RemoveAsync(id);

            return RedirectToAction("List");
        }
    }
}
