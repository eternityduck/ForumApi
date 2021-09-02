using System;
using System.Collections.Generic;

namespace BLL.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt{ get; set; }
        public int AuthorId { get; set; }
        public ICollection<int> CommentIds { get; set; }
    }
}