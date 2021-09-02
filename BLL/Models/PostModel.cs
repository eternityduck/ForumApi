using System;
using System.Collections.Generic;
using DAL.Models;

namespace BLL.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string AuthorId { get; set; }
        public DateTime CreatedAt{ get; set; }
        public string AuthorName { get; set; }
        public ICollection<int> CommentIds { get; set; }
    }
}