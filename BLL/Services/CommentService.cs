using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using DAL;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Get()
        {
            _unitOfWork.Comments.GetAllAsync();
        }

        public Task<IEnumerable<CommentModel>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<CommentModel> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task AddAsync(CommentModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(CommentModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteByIdAsync(int modelId)
        {
            throw new System.NotImplementedException();
        }
    }
}