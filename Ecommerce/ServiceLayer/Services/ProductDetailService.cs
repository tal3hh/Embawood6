using AutoMapper;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.DTOs;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ProductDetailService(IUow uow, IMapper mapper, AppDbContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<ProductDto>> AllProDetaIsNull()
        {
            var list = await _uow.GetRepository<Product>().AllFilterAsync(x => x.ProductDetail == null, false);

            return _mapper.Map<List<ProductDto>>(list);
        }


        public async Task<ProductDetailUpdateDto> GetByUpdateIdAsync(int id)
        {
            var dto = await _uow.GetRepository<ProductDetail>().SingleOrDefaultAsync(x => x.ProductId == id);

            return _mapper.Map<ProductDetailUpdateDto>(dto);
        }

        public async Task CreateAsync(ProductDetailCreateDto dto)
        {
            var entity = _mapper.Map<ProductDetail>(dto);

            if (entity != null)
            {
                await _uow.GetRepository<ProductDetail>().CreateAsync(entity);
                await _uow.SaveChangesAsync();
            }
        }


        public async Task UpdateAsync(ProductDetailUpdateDto dto)
        {
            var DbEntity = await _uow.GetRepository<ProductDetail>().FindAsync(dto.Id);

            if ((DbEntity != null) || (dto != null))
            {
                var entity = _mapper.Map<ProductDetail>(dto);
                _uow.GetRepository<ProductDetail>().Update(entity, DbEntity);

                await _uow.SaveChangesAsync();
            }
        }


        public async Task RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<ProductDetail>().SingleOrDefaultAsync(x => x.ProductId == id);

            if (entity != null)
            {
                _uow.GetRepository<ProductDetail>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
