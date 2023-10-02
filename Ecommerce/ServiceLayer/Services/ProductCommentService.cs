using AutoMapper;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.DTOs;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.Utilities.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ProductCommentService : IProductCommentService
    {
        private readonly IUow _uow;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductCommentService(IUow uow, IMapper mapper, AppDbContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Paginate<ProductCommentDto>> AllComments(string search, int page, int take)
        {
            var entities = from p in _context.ProductComments
                          .Include(x => x.Product)
                           select p;

            //Seacrh Filter
            if (!String.IsNullOrEmpty(search))
            {
                entities = from p in _context.ProductComments
                           .Include(x => x.Product)
                           .Where(x => x.Product.Name.Contains(search))
                           select p;
            }


            //Paginate
            var allCount = await entities.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities.Skip((page - 1) * take).Take(take).OrderByDescending(x=> x.Id).ToListAsync();

            //Auto Mapper
            var dtos = _mapper.Map<List<ProductCommentDto>>(entities2);

            var Dtos = new Paginate<ProductCommentDto>(dtos, page, Totalpage);

            return Dtos;
        }

        public async Task<List<ProductCommentDto>> ProductComment(int id)
        {
            var comments = await _context.ProductComments.Include(x => x.Product).Where(x => x.ProductId == id).OrderByDescending(x=> x.Id).ToListAsync();

            return _mapper.Map<List<ProductCommentDto>>(comments);
        }

        public async Task CreateAsync(ProductCommentCreateDto dto)
        {
            var entity = _mapper.Map<ProductComment>(dto);

            if (entity != null)
            {
                await _uow.GetRepository<ProductComment>().CreateAsync(entity);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<ProductComment>().FindAsync(id);

            if (entity != null)
            {
                _uow.GetRepository<ProductComment>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
