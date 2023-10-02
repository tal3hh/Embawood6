using ServiceLayer.DTOs;
using ServiceLayer.Utilities.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<Paginate<ProductDto>> AllFilterAsync(string search, string sortOrder, int page, int take);
        Task<Paginate<ProductDto>> AllHomeFilterAsync(string search, string sortOrder, int page, int take);
        Task<List<ProductDto>> AllAsync();
        Task<ProductUpdateDto> GetByUpdateIdAsync(int id);
        Task<ProductDto> GetByIdAsync(int id);
        Task CreateAsync(ProductCreateDto dto);
        Task UpdateAsync(ProductUpdateDto dto);
        Task RemoveAsync(int id);
    }
}
