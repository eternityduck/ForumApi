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
        private readonly Mapper _mapper;

        public PostService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostModel>> GetAllAsync() =>
            _mapper.Map<IEnumerable<Post>, List<PostModel>>(await _unitOfWork.Posts.GetAllAsync());
        
        public async Task<PostModel> GetByIdAsync(int id) =>
            _mapper.Map<PostModel>(await _unitOfWork.Posts.GetByIdAsync(id));
        
        public async Task AddAsync(PostModel model)
        {
            if (model == null)
                throw new ForumException("Can not be null");
            if (string.IsNullOrEmpty(model.Text))
                throw new ForumException("The post can not be null");
            if (string.IsNullOrEmpty(model.Title))
                throw new ForumException("The title can not be empty");

            await _unitOfWork.Posts.AddAsync(_mapper.Map<Post>(model));
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
            
            await _unitOfWork.Posts.UpdateAsync(_mapper.Map<Post>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            
            await _mapper.Map<Task>(_unitOfWork.Posts.RemoveByIdAsync(modelId));
            await _unitOfWork.SaveAsync();
        }
    }
}