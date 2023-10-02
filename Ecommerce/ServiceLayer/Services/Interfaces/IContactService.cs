using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IContactService
    {
        Task CreateAsync(ContactCreateDto dto);
        Task<List<ContactDto>> GetAllAsync();
        Task RemoveAsync(int id);
    }
}
