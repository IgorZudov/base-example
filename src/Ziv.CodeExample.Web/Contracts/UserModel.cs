using System.Collections.Generic;
namespace Ziv.CodeExample.Web.Contracts
{
    public class UserModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<CommentModel> Comments { get; set; }
    }
}