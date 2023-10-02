using AutoMapper;
using DomainLayer.Entities;
using MimeKit.Cryptography;
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
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public ContactService(IMapper mapper, IUow uow)
        {
            _mapper = mapper;
            _uow = uow;
        }


        public async Task<List<ContactDto>> GetAllAsync()
        {
            var list = await _uow.GetRepository<Contact>().AllAsync();

            return _mapper.Map<List<ContactDto>>(list);
        }

        public async Task CreateAsync(ContactCreateDto dto)
        {
            var entity = _mapper.Map<Contact>(dto);

            if (entity != null)
            {
                await _uow.GetRepository<Contact>().CreateAsync(entity);

                await _uow.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<Contact>().FindAsync(id);

            if (entity != null)
            {
                _uow.GetRepository<Contact>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
