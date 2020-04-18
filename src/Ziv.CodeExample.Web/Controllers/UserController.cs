using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ziv.CodeExample.Abstractions;
using Ziv.CodeExample.Dto;
using Ziv.CodeExample.Web.Contracts;

namespace Ziv.CodeExample.Web.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]long? userId, [FromQuery]bool loadComments) =>
            userId.HasValue ?
                Ok(_mapper.Map<UserModel>(await _service.Get(userId.Value, loadComments))) :
                Ok(_mapper.Map<List<UserModel>>(await _service.Get(loadComments)));

        [HttpPost]
        public async Task<ActionResult> Add([FromBody]UserModel model)
        {
            var res = await _service.Create(_mapper.Map<UserDto>(model));
            return Ok(_mapper.Map<UserModel>(res));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute]long id,[FromBody]UserModel model)
        {
            await _service.Put(id,_mapper.Map<UserDto>(model));
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