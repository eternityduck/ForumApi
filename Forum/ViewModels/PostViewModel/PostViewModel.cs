using System;
using System.Collections.Generic;
using DAL.Models;

namespace Forum.ViewModels.PostViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt{ get; set; }
        public int AuthorId { get; set; }
        
    }
}