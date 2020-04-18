using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ziv.CodeExample.Abstractions;
using Ziv.CodeExample.Dto;
using Ziv.CodeExample.Web.Contracts;

namespace Ziv.CodeExample.Web.Controllers
{
    [Route("comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;
        private readonly IMapper _mapper;

        public CommentController(ICommentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]long? userId, [FromQuery]bool loadUser) =>
            userId.HasValue ?
                Ok(_mapper.Map<CommentModel>(await _service.Get(userId.Value, loadUser))) :
                Ok(_mapper.Map<List<CommentModel>>(await _service.Get(loadUser)));

        [HttpPost]
        public async Task<ActionResult> Add([FromBody]CommentModel model)
        {
            var res = await _service.Create(_mapper.Map<CommentDto>(model));
            return Ok(_mapper.Map<CommentModel>(res));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute]long id,[FromBody]CommentModel model)
        {
            await _service.Put(id,_mapper.Map<CommentDto>(model));
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]long id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}