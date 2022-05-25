using AutoMapper;
using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.DTOs;
using CEDIS.Core.Pgsql.Persistences;
using CEDIS.Core.Pgsql.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services
{
    public class UserService : IUserSevice
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserViewDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<UserViewDto>>(await _dbContext.Users.ToListAsync());
        }

        public async Task<UserViewDto> GetById(int userId)
        {
            return _mapper.Map<UserViewDto>(await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId));
        }

        public async Task<UserViewDto> AddAsync(UserCreateDto newUser)
        {
            if (UserExists(newUser.UserName))
                throw new Exception("El nombre de usuario ya está en uso.");

            var user = _mapper.Map<UserCreateDto, User>(newUser, opt => opt.AfterMap((src, dest) =>
               {
                   dest.Salt = BCrypt.Net.BCrypt.GenerateSalt();
                   dest.Password = BCrypt.Net.BCrypt.HashPassword(src.Password, dest.Salt);
                   dest.Status = true;
               }));

            var result = _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserViewDto>(result.Entity);
        }

        public async Task<bool> UpdateAsync(int userId,UserCreateDto updateUser)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (updateUser.ChangePassword)
            {
                var isValidPass = BCrypt.Net.BCrypt.Verify(updateUser.Password, user.Password);
                if (!isValidPass)
                    throw new Exception("La contraseña antigua no es correcta.");

                user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
                user.Password = BCrypt.Net.BCrypt.HashPassword(updateUser.NewPassword, user.Salt);
            }
            user.Name = updateUser.Name;
            user.Status = updateUser.Status;

            return await _dbContext.SaveChangesAsync()>0;
        }

        private bool UserExists(string username) => _dbContext.Users.Any(x => x.UserName.Trim().ToUpper() == username.Trim().ToUpper());
    }
}
