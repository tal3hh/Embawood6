using FluentValidation;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Validations.Category
{
    public class CategoryUpdateValidation : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kateqoriya yazin.").NotNull().WithMessage("Kateqoriya yazin.")
                .Length(2, 100).WithMessage("Kateqoriya 2-100 simvol olmalidir.");
        }
    }
}
