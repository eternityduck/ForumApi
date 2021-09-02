using System;
using System.Collections.Generic;
using DAL.Models;

namespace Forum.ViewModels.PostViewModel
{
    public class PostViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt{ get; set; }
        public int Author { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}