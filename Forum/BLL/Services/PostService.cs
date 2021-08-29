using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<PostModel>> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Post>, List<PostModel>>(await _unitOfWork.Posts.GetAllAsync());
        }

        public async Task<PostModel> GetByIdAsync(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostModel>()).CreateMapper();
            return mapper.Map<PostModel>(await _unitOfWork.Posts.GetByIdAsync(id));
        }

        public async Task AddAsync(PostModel model)
        {
            if (model == null)
                throw new ForumException("Can not be null");
            if (string.IsNullOrEmpty(model.Text))
                throw new ForumException("The post can not be null");
            if (string.IsNullOrEmpty(model.Title))
                throw new ForumException("The title can not be empty");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostModel>()).CreateMapper();
            await _unitOfWork.Posts.AddAsync(mapper.Map<Post>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(PostModel model)
        {
            if (model == null)
                throw new ForumException("Can not be null");
            if (string.IsNullOrEmpty(model.Text))
                throw new ForumException("The post can not be null");
            if (string.IsNullOrEmpty(model.Title))
                throw new ForumException("The title can not be empty");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostModel>()).CreateMapper();
            await _unitOfWork.Posts.UpdateAsync(mapper.Map<Post>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Post, PostModel>()).CreateMapper();
            await mapper.Map<Task>(_unitOfWork.Posts.RemoveByIdAsync(modelId));
            await _unitOfWork.SaveAsync();
        }
    }
}