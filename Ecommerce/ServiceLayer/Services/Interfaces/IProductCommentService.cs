using ServiceLayer.DTOs;
using ServiceLayer.Utilities.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IProductCommentService
    {
        Task<List<ProductCommentDto>> ProductComment(int id);
        Task CreateAsync(ProductCommentCreateDto dto);
        Task<Paginate<ProductCommentDto>> AllComments(string search, int page, int take);
        Task RemoveAsync(int id);
    }
}
