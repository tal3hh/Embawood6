using FluentValidation;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Validations.ProductComment
{
    public class ProductCommentCreateValidation : AbstractValidator<ProductCommentCreateDto>
    {
        public ProductCommentCreateValidation()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Adinizi daxil edin.").NotEmpty().WithMessage("Adinizi daxil edin.")
                .Length(2, 100).WithMessage("2-100 arasi simvol daxil edin.");

            RuleFor(x => x.Description).NotNull().WithMessage("Comment daxil edin.").NotEmpty().WithMessage("Comment daxil edin.")
                .Length(5, 300).WithMessage("5-300 arasi simvol daxil edin.");

            RuleFor(x => x.Email).EmailAddress().WithMessage("E-mail formatinda '@' yazi daxil edin.")
                .NotEmpty().WithMessage("Email yazin.")
                .NotNull().WithMessage("Email yazin.")
                .Length(8,150).WithMessage("8-150 arasi simvol daxil edin.");
        }
    }
}
