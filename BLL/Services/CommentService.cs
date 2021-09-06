using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;
        private readonly ForumContext _context;
        public CommentService(IUnitOfWork unitOfWork, Mapper mapper, ForumContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        

        public async Task<IEnumerable<CommentModel>> GetAllAsync()
        {
            var result = _mapper.Map<IEnumerable<Comment>, List<CommentModel>>(await _unitOfWork.Comments.GetAllAsync());
            
            return result;
        }

        public async Task<CommentModel> GetByIdAsync(int id)=>
            _mapper.Map<CommentModel>(await _unitOfWork.Comments.GetByIdAsync(id));

        public async Task AddAsync(CommentModel model)
        {
            if (model == null)
                throw new ForumException("Can not be null");
            if (string.IsNullOrEmpty(model.Text))
                throw new ForumException("The title can not be empty");

            var item = _mapper.Map<Comment>(model);
            await _unitOfWork.Comments.AddAsync(item);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(CommentModel model)
        {
            if (model == null)
                throw new ForumException("Can not be null");
            if (string.IsNullOrEmpty(model.Text))
                throw new ForumException("The post can not be null");
          
            
            _unitOfWork.Comments.Update(_mapper.Map<Comment>(model));
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _mapper.Map<Task>(_unitOfWork.Comments.RemoveByIdAsync(modelId));
            await _unitOfWork.SaveAsync();
        }
    }
}