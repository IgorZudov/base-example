using System.Collections.Generic;
using System.Threading.Tasks;
using Ziv.CodeExample.Dto;

namespace Ziv.CodeExample.Abstractions
{
    public interface ICommentService
    {
        Task<CommentDto> Get(long id, bool loadUser);

        Task<IEnumerable<CommentDto>> Get(bool loadUser);
        
        Task Put(long id, CommentDto model);

        Task<CommentDto> Create(CommentDto model);

        Task Delete(long id);
    }
}