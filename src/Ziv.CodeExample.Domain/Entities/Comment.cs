namespace Ziv.CodeExample.Entities
{
    public class Comment
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public string Text { get; set; }
    }
}