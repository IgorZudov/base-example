using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ziv.CodeExample.Abstractions;
using Ziv.CodeExample.Dto;
using Ziv.CodeExample.Entities;
using Ziv.CodeExample.Helpers;

namespace Ziv.CodeExample.Services
{
    public class UserService : IUserService
    {
        private readonly IDbRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IDbRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<UserDto> Get(long id, bool loadComments)
        {
            var query = _repository.Query<User>().Where(x => x.Id == id);
            if (loadComments)
                query = query.Include(x => x.Comments);

            var user = await query.FirstOrDefaultAsync();
            Guard.NotNull(user, "User not found");
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> Get(bool loadComments)
        {
            var query = _repository.Query<User>();
            if (loadComments)
                query = query.Include(x => x.Comments);

            var user = await query.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(user);
        }

        public async Task Put(long id, UserDto model)
        {
            Guard.IsTrue(await _repository.IsExistByMember<User>(x => x.Id == id),
                "User not found");
            
            _repository.Update(_mapper.Map<User>(model));
            await _repository.Commit();
        }

        public async Task<UserDto> Create(UserDto model)
        {
            Guard.NotNull(model, "Data not found");
            var user = _mapper.Map<User>(model);
            await _repository.Create(user);
            await _repository.Commit();
            return _mapper.Map<UserDto>(user);
        }

        public async Task Delete(long id)
        {
            Guard.IsTrue(await _repository.IsExistByMember<User>(x => x.Id == id),
                "User not found");
            
            _repository.Delete(new User
            {
                Id = id
            });
            await _repository.Commit();
        }
    }
}