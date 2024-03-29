using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using BLL.Validation;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Web_Api_Tests
{
    public class PostServiceTests
    {
        [Test]
        public async Task Create_Post_Creates_New_Post_Via_Context()
        {

            var options = new DbContextOptionsBuilder<ForumContext>()
                .UseInMemoryDatabase("Add_Post_Writes_Post_To_Database").Options;
            
            await using (var ctx = new ForumContext(options))
            {
                var postService = new PostService(ctx);

                var post = new Post
                {
                    Title = "writing functional javascript",
                    Text = "some post content"
                };

                await postService.AddAsync(post);
            }
            
            await using (var ctx = new ForumContext(options))
            {
                Assert.AreEqual(1, ctx.Posts.CountAsync().Result);
                Assert.AreEqual("writing functional javascript", ctx.Posts.SingleAsync().Result.Title);
            }
        }

        [Test]
        public void Get_Post_By_Id_Returns_Correct_Post()
        {
            var options = new DbContextOptionsBuilder<ForumContext>()
                .UseInMemoryDatabase("Get_Post_By_Id_Db").Options;

            using (var ctx = new ForumContext(options))
            {
                ctx.Posts.Add(new Post {Id = 1986, Title = "first post"});
                ctx.Posts.Add(new Post {Id = 223, Title = "second post"});
                ctx.Posts.Add(new Post {Id = 12, Title = "third post"});
                ctx.SaveChanges();
            }

            using (var ctx = new ForumContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetById(223);
                Assert.AreEqual(result.Title, "second post");
            }
        }
        
        [Test]
        public async Task Checking_Reply_Count_Returns_Number_Of_Replies()
        {
            var options = new DbContextOptionsBuilder<ForumContext>()
                .UseInMemoryDatabase("Comments_Check").Options;

            await using (var ctx = new ForumContext(options))
            {
                ctx.Posts.Add(new Post
                {
                    Id = 21341,
                });

                await ctx.SaveChangesAsync();
            }

            await using (var ctx = new ForumContext(options))
            {
                var postService = new PostService(ctx);
                var post = await postService.GetByIdAsync(21341);

                await postService.AddCommentAsync(new Comment()
                {
                    Post = post,
                    Text = "Here's a post reply."
                });
            }

            await using (var ctx = new ForumContext(options))
            {
                var postService = new PostService(ctx);
                var replyCount = postService.GetCommentsCount(21341);
                Assert.AreEqual(replyCount, 1);
            }
        }

        [Test]
        public async Task Edit_Post_Edits_Post_Correctly()
        {
            var options = new DbContextOptionsBuilder<ForumContext>()
                .UseInMemoryDatabase("Edit_DataBase").Options;
            await using (var ctx = new ForumContext(options))
            {
                ctx.Posts.Add(new Post {Id = 21342, Title = "first post"});
                await ctx.SaveChangesAsync();
            }

            await using (var ctx = new ForumContext(options))
            {
                var postService = new PostService(ctx);
                await postService.UpdateContentAsync(21342, "new post content");
                Assert.AreEqual((await ctx.Posts.FindAsync(21342)).Text, "new post content");
            }
        }

        [Test]
        public async Task GetPostsByTopicId_Returns_CorrectPosts()
        {
            var options = new DbContextOptionsBuilder<ForumContext>()
                .UseInMemoryDatabase("GetPostsByTopic_DataBase").Options;

            await using (var ctx = new ForumContext(options))
            {
                var topic = ctx.Topics.Add(new Topic() {Id = 21, Title = "New Topic"});
                ctx.Posts.Add(new Post()
                {
                    Id = 1234, Title = "Post 1", Topic = topic.Entity
                });
                ctx.Posts.Add(new Post()
                {
                    Id = 1235, Title = "Post 1", Topic = topic.Entity
                });

                await ctx.SaveChangesAsync();
            }

            await using (var ctx = new ForumContext(options))
            {
                var postService = new PostService(ctx);
                var result = await postService.GetPostsByTopicId(21);
                Assert.AreEqual(2, result.ToList().Count);
                Assert.AreEqual("Post 1", result.ToList()[0].Title);
            }
        }
        
        [Test]
        public async Task GetPostsByUserId_Returns_CorrectPosts()
        {
            var options = new DbContextOptionsBuilder<ForumContext>()
                .UseInMemoryDatabase("GetPostsByUser_DataBase").Options;

            await using (var ctx = new ForumContext(options))
            {
                var user = ctx.Users.Add(new User() {Id = "21", Email = "123@gmail.com"});
                ctx.Posts.Add(new Post()
                {
                    Id = 1234, Title = "Post 1", Author = user.Entity
                });
                ctx.Posts.Add(new Post()
                {
                    Id = 1235, Title = "Post 1", Author = user.Entity
                });

                await ctx.SaveChangesAsync();
            }

            await using (var ctx = new ForumContext(options))
            {
                var postService = new PostService(ctx);
                var result = await postService.GetPostsByUserEmail("123@gmail.com");
                Assert.AreEqual(2, result.ToList().Count);
                Assert.AreEqual("Post 1", result.ToList()[0].Title);
            }
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllPosts()
        {
            var options = new DbContextOptionsBuilder<ForumContext>()
                .UseInMemoryDatabase(databaseName: "GetAllAsync").Options;

            await using (var ctx = new ForumContext(options))
            {
                ctx.Posts.Add(new Post {Id = 21341, Title = "first post"});
                ctx.Posts.Add(new Post {Id = 8144, Title = "second post"});
                ctx.Posts.Add(new Post {Id = 1245, Title = "third post"});
                await ctx.SaveChangesAsync();
            }

            await using (var ctx = new ForumContext(options))
            {
                var postService = new PostService(ctx);
                var result = await postService.GetAllAsync();
                Assert.AreEqual(3, result.Count());
            }
        }

        [Test]
        public async Task GetLatestPost_Returns_Correct_Posts()
        {
            var options = new DbContextOptionsBuilder<ForumContext>()
                .UseInMemoryDatabase(databaseName: "GetLatestPosts").Options;
            await using (var ctx = new ForumContext(options))
            {
                ctx.Posts.Add(new Post {Id = 9090, Title = "first post"});
                ctx.Posts.Add(new Post {Id = 9999, Title = "second post", CreatedAt = DateTime.Parse("11/11/2000")});
                ctx.Posts.Add(new Post {Id = 3333, Title = "third post", CreatedAt = DateTime.Now, Text = "Test"});
                await ctx.SaveChangesAsync();
            }

            await using (var ctx = new ForumContext(options))
            {
                var postService = new PostService(ctx);
                var result = await postService.GetLatestPosts(1);
                Assert.AreEqual(result.ToList()[0].Text, "Test");
            }
        }

        [Test]
        public async Task GetAllUsers_Returns_Users_Correctly()
        {
            var options = new DbContextOptionsBuilder<ForumContext>()
                .UseInMemoryDatabase(databaseName: "GetAllUsersFromPost").Options;

            await using (var ctx = new ForumContext(options))
            {
                ctx.Posts.Add(new Post
                    {Id = 121, Title = "first post", Author = new User() {Id = "2223", Name = "Ivan"}});
                ctx.Posts.Add(new Post
                    {Id = 2121, Title = "second post", Author = new User() {Id = "1122", Name = "Bob"}});
                ctx.Posts.Add(new Post
                {
                    Id = 21212121, Title = "third post", Text = "Test", Author = new User() {Id = "3333", Name = "Test"}
                });
                await ctx.SaveChangesAsync();
            }

            await using (var ctx = new ForumContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetAllUsers(ctx.Posts.Include(x => x.Author)).ToList();
                Assert.AreEqual(3, result.Count);
            }
        }

        [Test]
        public void GetCommentsCount_Returns_CorrectNumber_Of_Comments()
        {
            var options = new DbContextOptionsBuilder<ForumContext>()
                .UseInMemoryDatabase(databaseName: "GetComments_Count_Database").Options;

            using (var ctx = new ForumContext(options))
            {
                ctx.Posts.Add(new Post
                    {Id = 121, Title = "first post", Comments = new List<Comment>() {new Comment() {Id = 2121}}});
                ctx.Posts.Add(new Post
                    {Id = 2121, Title = "second post", Author = new User() {Id = "1122", Name = "Bob"}});
                ctx.Posts.Add(new Post
                {
                    Id = 21212121, Title = "third post", Text = "Test", Author = new User() {Id = "3333", Name = "Test"}
                });
                ctx.SaveChanges();
            }

            using (var ctx = new ForumContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetCommentsCount(121);
                Assert.AreEqual(1, result);
            }
        }
        
    }
}