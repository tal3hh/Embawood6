using Microsoft.AspNetCore.Mvc;

namespace Web.UI.ViewComponents
{
    public class ProductSearch : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
