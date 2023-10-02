using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IProductDetailService
    {
        Task<List<ProductDto>> AllProDetaIsNull();
        Task CreateAsync(ProductDetailCreateDto dto);
        Task UpdateAsync(ProductDetailUpdateDto dto);
        Task<ProductDetailUpdateDto> GetByUpdateIdAsync(int id);
        Task RemoveAsync(int id);
    }
}
