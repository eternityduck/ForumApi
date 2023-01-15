using System;

namespace Forum_Web_API.ViewModels.PostViewModel
{
    public class CreatePostViewModel
    {
        public string Title { get; set; }
        public int TopicId { get; set; }
        public string Content { get; set; }
        public string AuthorEmail { get; set; }
    }
}