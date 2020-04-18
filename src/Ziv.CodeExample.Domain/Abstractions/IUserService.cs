using System.Collections.Generic;
using System.Threading.Tasks;
using Ziv.CodeExample.Dto;

namespace Ziv.CodeExample.Abstractions
{
    public interface IUserService
    {
        Task<UserDto> Get(long id, bool loadComments);

        Task<IEnumerable<UserDto>> Get(bool loadComments);
        
        Task Put(long id, UserDto model);

        Task<UserDto> Create(UserDto model);

        Task Delete(long id);
    }
}