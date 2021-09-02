using System;

namespace BLL.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }
    }
}