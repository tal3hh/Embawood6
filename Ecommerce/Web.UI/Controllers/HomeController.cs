using DomainLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using ServiceLayer.Services.Interfaces;
using System.Diagnostics;

namespace Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productservice;
        private readonly IContactService _contactService;
        private readonly IFavoriteService _favoriteService;
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _usermanager;
        public HomeController(IProductService productservice, IContactService contactService, UserManager<AppUser> usermanager, IFavoriteService favoriteService, IBasketService basketService)
        {
            _productservice = productservice;
            _contactService = contactService;
            _usermanager = usermanager;
            _favoriteService = favoriteService;
            _basketService = basketService;
        }

        List<ProductDto> basket = new List<ProductDto>();

        public async Task<IActionResult> Index()
        {
            var products = await _productservice.AllAsync();

            return View(products);
        }


        #region Basket

        public async Task<IActionResult> BasketAdd(int id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var user = await _usermanager.GetUserAsync(User);
            var dto = await _productservice.GetByIdAsync(id);

            await _basketService.CreateAsync(user.Id, dto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BasketList()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var user = await _usermanager.GetUserAsync(User);
            var list = await _basketService.GetAllAsync(user.Id);

            return View(list);
        }

        public async Task<IActionResult> BasketRemove(int id)
        {
            await _basketService.RemoveAsync(id);

            return RedirectToAction("BasketList");
        }

        #endregion


        #region Favorite

        public async Task<IActionResult> Favorite(int id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var user = await _usermanager.GetUserAsync(User);
            var dto = await _productservice.GetByIdAsync(id);

            await _favoriteService.CreateAsync(user.Id, dto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> FavoriteList()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var user = await _usermanager.GetUserAsync(User);

            var list = await _favoriteService.GetAllAsync(user.Id);

            return View(list);
        }

        public async Task<IActionResult> FavoriteRemove(int id)
        {
            await _favoriteService.RemoveAsync(id);

            return RedirectToAction("FavoriteList");
        }

        #endregion


        #region Contact

        public IActionResult Contact()
        {
            return View(new ContactCreateDto());
        }
        [HttpPost]
        public async Task<IActionResult> Contact(ContactCreateDto dto)
        {
            await _contactService.CreateAsync(dto);

            return RedirectToAction("Contact");
        }

        #endregion


        public IActionResult About()
        {
            return View();
        }


        public IActionResult ErrorPage()
        {
            return View();
        }


    }
}