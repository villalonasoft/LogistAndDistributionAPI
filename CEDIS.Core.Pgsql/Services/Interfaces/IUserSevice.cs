using CEDIS.Core.Pgsql.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services.Interfaces
{
    public interface IUserSevice
    {
        Task<IEnumerable<UserViewDto>> GetAll();
        Task<UserViewDto> GetById(int userId);
        Task<UserViewDto> AddAsync(UserCreateDto newUser);
        Task<bool> UpdateAsync(int userId,UserCreateDto updateUser);
    }
}
