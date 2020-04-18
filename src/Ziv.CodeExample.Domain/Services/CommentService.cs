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
    public class CommentService : ICommentService
    {
        private readonly IDbRepository _repository;
        private readonly IMapper _mapper;

        public CommentService(IDbRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CommentDto> Get(long id, bool loadUser)
        {
            var query = _repository.Query<Comment>().Where(x => x.Id == id);
            if (loadUser)
                query = query.Include(x => x.User);

            var user = await query.FirstOrDefaultAsync();

            return _mapper.Map<CommentDto>(user);
        }

        public async Task<IEnumerable<CommentDto>> Get(bool loadUser)
        {
            var query = _repository.Query<Comment>();
            if (loadUser)
                query = query.Include(x => x.User);

            var comment = await query.ToListAsync();
            Guard.NotNull(comment, "User not found");

            return _mapper.Map<IEnumerable<CommentDto>>(comment);
        }

        public async Task Put(long id, CommentDto model)
        {
            Guard.IsTrue(await _repository.IsExistByMember<Comment>(x => x.Id == id),
                "Comment not found");
            
            _repository.Update(_mapper.Map<Comment>(model));
            await _repository.Commit();
        }

        public async Task<CommentDto> Create(CommentDto model)
        {
            Guard.NotNull(model, "Data not found");
            var comment = _mapper.Map<Comment>(model);
            await _repository.Create(comment);
            await _repository.Commit();
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task Delete(long id)
        {
            Guard.IsTrue(await _repository.IsExistByMember<Comment>(x => x.Id == id),
                "Comment not found");
            
            _repository.Delete(new User
            {
                Id = id
            });
            await _repository.Commit();
        }
    }
}