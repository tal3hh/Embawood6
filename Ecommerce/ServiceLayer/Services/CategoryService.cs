using AutoMapper;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.DTOs;
using ServiceLayer.Services.Interfaces;


namespace ServiceLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public CategoryService(IUow uow, IMapper mapper, AppDbContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
        }

        public List<CategoryDto> GetCategories()
        {
            var list = _context.Categories.OrderBy(x => x.Id).ToList();

            return _mapper.Map<List<CategoryDto>>(list);
        }

        public async Task<List<ProductDto>> CategoryProducts(int id)
        {
            var list = await _context.Products.Include(x => x.Category).Where(x => x.CategoryId == id).OrderByDescending(x => x.Id).ToListAsync();
           
            return _mapper.Map<List<ProductDto>>(list);
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var entities = await _context.Categories.Include(x => x.Products).OrderByDescending(x => x.Id).ToListAsync();

            return _mapper.Map<List<CategoryDto>>(entities);
        }

        public async Task CreateAsync(CategoryCreateDto dto)
        {
            var entity = _mapper.Map<Category>(dto);

            if (entity != null)
            {
                await _uow.GetRepository<Category>().CreateAsync(entity);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var entity = await _uow.GetRepository<Category>().FindAsync(id);

            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task<CategoryUpdateDto> GetByIdUpdate(int id)
        {
            var entity = await _uow.GetRepository<Category>().FindAsync(id);

            return _mapper.Map<CategoryUpdateDto>(entity);
        }

        public async Task Update(CategoryUpdateDto dto)
        {
            var unchangedentity = await _uow.GetRepository<Category>().FindAsync(dto.Id);

            if (unchangedentity != null)
            {
                var entity = _mapper.Map<Category>(dto);
                _uow.GetRepository<Category>().Update(entity, unchangedentity);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task Remove(int id)
        {
            var entity = await _uow.GetRepository<Category>().FindAsync(id);

            if (entity != null)
            {
                _uow.GetRepository<Category>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
