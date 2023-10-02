using FluentValidation;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Validations.ProductDetail
{
    public class ProductDetailUpdateValidation : AbstractValidator<ProductDetailUpdateDto>
    {
        public ProductDetailUpdateValidation()
        {
            RuleFor(x => x.ProductId).Must(x => x != 0).WithMessage("Mehsul secin! Yoxdursa yeni bir mehsul elave olunanadek gozleyin.");

            RuleFor(x => x.Design).NotEmpty().WithMessage("Dizayn adini daxil edin.")
                .NotNull().WithMessage("Dizayn adini daxil edin.");
            RuleFor(x => x.Material).NotEmpty().WithMessage("Material adini daxil edin.")
                .NotNull().WithMessage("Material adini daxil edin.");
            RuleFor(x => x.About).NotEmpty().WithMessage("Mehsul haqqinda melumat daxil edin.")
                .NotNull().WithMessage("Mehsul haqqinda melumat daxil edin.");
        }
    }
}
