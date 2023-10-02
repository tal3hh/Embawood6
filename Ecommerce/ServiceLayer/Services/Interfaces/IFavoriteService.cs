using DomainLayer.Entities;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task CreateAsync(int userId, ProductDto dto);
        Task RemoveAsync(int id);
        Task<List<Favorite>> GetAllAsync(int userID);
    }
}
