using System.Collections.Generic;

namespace Ziv.CodeExample.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<Comment> Comments { get; set; }
    }
}