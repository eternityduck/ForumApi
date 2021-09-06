using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Models;
using Forum.ViewModels.PostViewModel;
using Forum.ViewModels.TopicViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public IActionResult Index()
        {
            var forums = _topicService.GetAllAsync().Result.Select(f => new TopicListViewModel
            {
                Id = f.Id,
                Name = f.Title,
                Description = f.Description,
                NumberOfPosts = f.Posts?.Count() ?? 0,

            });

            var forumListingModels = forums as IList<TopicListViewModel> ?? forums.ToList();

            var model = new TopicIndexViewModel()
            {
                TopicList = forumListingModels.OrderBy(forum=>forum.Name),
                NumberOfTopics = forumListingModels.Count()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new {id, searchQuery});
        }
        public async Task<IActionResult> Topic(int id, string searchQuery)
        {
            var topic = await _topicService.GetByIdAsync(id);
            var posts = _topicService.GetFilteredPosts(id, searchQuery).ToList();
            var noResults = (!string.IsNullOrEmpty(searchQuery) && !posts.Any());

            var postListings = posts.Select(post => new PostListViewModel()
            {
                Id = post.Id,
                Topic = BuildForumListing(post),
                Author = post.Author.UserName,
                AuthorId = post.Author.Id,
                Title = post.Title,
                DatePosted = post.CreatedAt.ToString(CultureInfo.InvariantCulture),
                RepliesCount = post.Comments.Count()
            }).OrderByDescending(post=>post.DatePosted);

            var model = new TopicResultViewModel()
            {
                EmptySearchResults = noResults,
                Posts = postListings,
                SearchQuery = searchQuery,
                Topic = BuildForumListing(topic)
            };

            return View(model);
        }
        private static TopicListViewModel BuildForumListing(Topic topic)
        {
            return new TopicListViewModel
            {
                Id = topic.Id,
                Name = topic.Title,
                Description = topic.Description
            };
        }

        private static TopicListViewModel BuildForumListing(Post post)
        {
            var topic = post.Topic;
            return BuildForumListing(topic);
        }

    }
}