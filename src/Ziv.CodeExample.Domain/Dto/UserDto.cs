using System.Collections.Generic;

namespace Ziv.CodeExample.Dto
{
    public class UserDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<CommentDto> Comments { get; set; }
    }
}