using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Extension
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductCommentService, ProductCommentService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IProductDetailService, ProductDetailService>();
            services.AddScoped<IMessageSend, MessageSend>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IBasketService, BasketService>();
        }
    }
}
