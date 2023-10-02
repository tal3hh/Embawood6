using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.DTOs;
using ServiceLayer.Validations.Account;
using ServiceLayer.Validations.Category;
using ServiceLayer.Validations.Product;
using ServiceLayer.Validations.ProductComment;
using ServiceLayer.Validations.ProductDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Extension
{
    public static class ValidationExtension
    {
        public static void AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ProductCommentCreateDto>, ProductCommentCreateValidation>();

            services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateValidation>();
            services.AddScoped<IValidator<ProductCreateDto>, ProductCreateValidation>();

            services.AddScoped<IValidator<UserCreateDto>, UserCreateValidation>();
            services.AddScoped<IValidator<UserLoginDto>, UserLoginValidation>();

            services.AddScoped<IValidator<CategoryUpdateDto>, CategoryUpdateValidation>();
            services.AddScoped<IValidator<CategoryCreateDto>, CategoryCreateValidation>();

            services.AddScoped<IValidator<ProductDetailUpdateDto>, ProductDetailUpdateValidation>();
            services.AddScoped<IValidator<ProductDetailCreateDto>, ProductDetailCreateValidation>();
        }
    }
}
