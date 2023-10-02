using AutoMapper.Execution;
using AutoMapper;
using RepositoryLayer.Contexts;
using RepositoryLayer.UniteOfWork;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.Utilities.Paginations;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DTOs;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Hosting;

namespace ServiceLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductService(IUow uow, IMapper mapper, AppDbContext context, IWebHostEnvironment env)
        {
            _uow = uow;
            _mapper = mapper;
            _context = context;
            _env = env;
        }

        public async Task<Paginate<ProductDto>> AllHomeFilterAsync(string search, string sortOrder, int page, int take)
        {

            var entities = from p in _context.Products
                           .Include(x => x.Category)
                           .Include(x => x.ProductDetail)
                           select p;

            //Seacrh Filter
            if (!String.IsNullOrEmpty(search))
            {
                entities = from p in _context.Products
                           .Where(x => x.Name.Contains(search))
                           .Include(x => x.Category)
                           .Include(x => x.ProductDetail)
                           select p;
            }

            //Sort Filter
            switch (sortOrder)
            {
                case "name_desc":
                    entities = entities.OrderByDescending(x => x.Name);
                    break;
                case "name_asc":
                    entities = entities.OrderBy(x => x.Name);
                    break;
                case "count_desc":
                    entities = entities.OrderByDescending(x => x.Count);
                    break;
                case "count_asc":
                    entities = entities.OrderBy(x => x.Count);
                    break;
                case "price_desc":
                    entities = entities.OrderByDescending(x => x.Price);
                    break;
                case "price_asc":
                    entities = entities.OrderBy(x => x.Price);
                    break;
                default:
                    entities = entities.OrderByDescending(x => x.Id);
                    break;
            }


            //Paginate
            var allCount = await entities.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities.Skip((page - 1) * take).Take(take).ToListAsync();

            //Auto Mapper
            var dtos = _mapper.Map<List<ProductDto>>(entities2);

            var Dtos = new Paginate<ProductDto>(dtos, page, Totalpage);

            return Dtos;
        }

        public async Task<Paginate<ProductDto>> AllFilterAsync(string search, string sortOrder, int page, int take)
        {

            var entities = from p in _context.Products
                           .Include(x => x.Category)
                           .Include(x => x.ProductDetail)
                           select p;

            //Seacrh Filter
            if (!String.IsNullOrEmpty(search))
            {
                entities = from p in _context.Products
                           .Where(x => x.Name.Contains(search))
                           .Include(x => x.Category)
                           .Include(x => x.ProductDetail)
                           select p;
            }

            //Sort Filter
            switch (sortOrder)
            {
                case "count_desc":
                    entities = entities.OrderByDescending(x => x.Count);
                    break;
                case "count_asc":
                    entities = entities.OrderBy(x => x.Count);
                    break;
                case "price_desc":
                    entities = entities.OrderByDescending(x => x.Price);
                    break;
                default:
                    entities = entities.OrderBy(x => x.Price);
                    break;
            }


            //Paginate
            var allCount = await entities.CountAsync();
            var Totalpage = (int)Math.Ceiling((decimal)allCount / take);

            var entities2 = await entities.Skip((page - 1) * take).Take(take).ToListAsync();

            //Auto Mapper
            var dtos = _mapper.Map<List<ProductDto>>(entities2);

            var Dtos = new Paginate<ProductDto>(dtos, page, Totalpage);

            return Dtos;
        }

        public async Task<List<ProductDto>> AllAsync()
        {
            var list = await _context.Products.Include(x=> x.Category).Include(x=> x.ProductDetail).OrderByDescending(x=> x.Id).ToListAsync();

            return _mapper.Map<List<ProductDto>>(list);
        }

        public async Task<ProductUpdateDto> GetByUpdateIdAsync(int id)
        {
            var entity = await _uow.GetRepository<Product>().FindAsync(id);

            return _mapper.Map<ProductUpdateDto>(entity);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var a = await _context.Products.Include(x=> x.ProductDetail).Include(x=> x.Category).SingleOrDefaultAsync(x=> x.Id == id);

            var entity = await _uow.GetRepository<Product>().FindAsync(id);

            return _mapper.Map<ProductDto>(a);
        }

        public async Task CreateAsync(ProductCreateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            if (entity != null)
            {
                string fileName = Guid.NewGuid().ToString()+ "_" + entity.Photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await entity.Photo.CopyToAsync(stream);
                }
                entity.Image = fileName;

                await _uow.GetRepository<Product>().CreateAsync(entity);
                await _uow.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(ProductUpdateDto dto)
        {
            var DbEntity = await _uow.GetRepository<Product>().FindAsync(dto.Id);
            var basket = await _context.Baskets.Where(x => x.ProductName == DbEntity.Name).FirstOrDefaultAsync();
            var favorite = await _context.Favorites.Where(x => x.ProductName == DbEntity.Name).FirstOrDefaultAsync();
            if ((DbEntity != null) || (dto != null))
            {
                string oldPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", DbEntity.Image);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                string fileName = Guid.NewGuid().ToString() + "_" + dto.Photo.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", fileName);
                using (FileStream stream = new FileStream(newPath, FileMode.Create))
                {
                    await dto.Photo.CopyToAsync(stream);
                }
                dto.Image = fileName;

                var entity = _mapper.Map<Product>(dto);
                _uow.GetRepository<Product>().Update(entity, DbEntity);
           

                
                if (basket != null)
                {
                    var newbasket = new Basket();
                    newbasket.Id = basket.Id;
                    newbasket.ProductName = entity.Name;
                    newbasket.Image = entity.Image;
                    newbasket.AppUserId = basket.AppUserId;
                    newbasket.Price = entity.Price;
                    newbasket.Count = entity.Count;
                    _uow.GetRepository<Basket>().Update(newbasket, basket);
              
                }

              
                if (favorite != null)
                {
                    var newfav = new Favorite();
                    newfav.Id = favorite.Id;
                    newfav.AppUserId = favorite.AppUserId;
                    newfav.ProductName = entity.Name;
                    newfav.Image = entity.Image;
                    newfav.Price = entity.Price;
                    _uow.GetRepository<Favorite>().Update(newfav, favorite);
                
                }
                await _uow.SaveChangesAsync();
            }

        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<Product>().FindAsync(id);

            if (entity != null)
            {
                if (entity.ProductDetail != null)
                {
                    var detail = await _uow.GetRepository<ProductDetail>().SingleOrDefaultAsync(x=> x.ProductId == id);
                    _uow.GetRepository<ProductDetail>().Remove(detail);
                    await _uow.SaveChangesAsync();
                }
                var path = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", entity.Image);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                _uow.GetRepository<Product>().Remove(entity);

                var basket = await _context.Baskets.Where(x => x.ProductName == entity.Name).FirstOrDefaultAsync();
                var favorite = await _context.Favorites.Where(x => x.ProductName == entity.Name).FirstOrDefaultAsync();
                if(basket != null) _uow.GetRepository<Basket>().Remove(basket);
                if(favorite != null) _uow.GetRepository<Favorite>().Remove(favorite);

                await _uow.SaveChangesAsync();
            }
        }
    }
}
