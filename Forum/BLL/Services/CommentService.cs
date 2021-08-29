using BLL.Interfaces;
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
    }
}