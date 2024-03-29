﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Validation;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class PostService : IPostService
    {
        private readonly ForumContext _context;
        
        public PostService( ForumContext context)
        {
            _context = context;
        }
        
        public async Task AddCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateContentAsync(int id, string content)
        {
            try
            {
                var post = await GetByIdAsync(id);
                post.Text = content;
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
            }
            catch (ForumException)
            {
                throw new ForumException("Invalid post id");
            }
        }

        public int GetCommentsCount(int id)
        {
            try
            {
                return GetById(id).Comments.Count;
            }
            catch (ForumException)
            {
                throw new ForumException("Invalid id of post");
            }
        }

        public async Task<IEnumerable<Post>> GetPostsByUserEmail(string userEmail) =>
            await _context.Posts.Where(x => x.Author.Email == userEmail).ToListAsync();

        public async Task<IEnumerable<Post>> GetPostsByTopicId(int id) 
        {
            var result = await _context.Topics.Include(x => x.Posts).FirstOrDefaultAsync(x => x.Id == id);
            return result.Posts;
        }
        

        public async Task<IEnumerable<Post>> GetAllAsync() => 
            await _context.Posts
                .Include(x => x.Topic).Include(x => x.Author)
                .Include(x => x.Comments).ThenInclude(x => x.Author)
                .ToListAsync();
        
        
        public async Task<Post> GetByIdAsync(int id) =>
            await _context.Posts.Where(x => x.Id == id)
                .Include(x => x.Author)
                .Include(post => post.Comments).ThenInclude(x => x.Author)
                .Include(post => post.Topic).FirstOrDefaultAsync();
        
        public Post GetById(int id) => 
            _context.Posts.Where(post=>post.Id == id)
                .Include(post=>post.Author)
                .Include(post=>post.Comments).ThenInclude(reply => reply.Author)
                .Include(post=>post.Topic)
                .First();
        
        public async Task<IEnumerable<Post>> GetLatestPosts(int count)
        {
            var result = await GetAllAsync();
            return result.OrderByDescending(p => p.CreatedAt).Take(count).ToList();
        }
        public IEnumerable<User> GetAllUsers(IEnumerable<Post> posts)
        {
            var users = new List<User>();
        
            foreach(var post in posts)
            {
                users.Add(post.Author);
        
                if (post.Comments == null) continue;
        
                users.AddRange(post.Comments.Select(reply => reply.Author));
            }
        
            return users.Distinct();
        }
        
        public async Task AddAsync(Post model)
        {
            await _context.Posts.AddAsync(model);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteByIdAsync(int modelId)
        {
            try
            {
                var model = await _context.Posts.Include(x => x.Comments).SingleOrDefaultAsync(x => x.Id == modelId);
                _context.Remove(model);
                await _context.SaveChangesAsync();
            }
            catch (ForumException)
            {
                throw new ForumException("Invalid id");
            }
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            var query = searchQuery.ToLower();

            return _context.Posts
                .Include(post => post.Topic)
                .Include(post => post.Author)
                .Include(post => post.Comments)
                .Where(post => 
                    post.Title.ToLower().Contains(query) 
                    || post.Text.ToLower().Contains(query));
        }
    }
}