using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        public UserService(IUnitOfWork unitOfWork, Mapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserModel>> GetAllAsync()=>
            _mapper.Map<IEnumerable<User>, List<UserModel>>(await _unitOfWork.Users.GetAllAsync());

        public async Task<UserModel> GetByIdAsync(string id)=>
            _mapper.Map<UserModel>(await _unitOfWork.Users.GetByIdAsync(id));

        public Task AddAsync(UserModel model)
        {
            throw new System.NotImplementedException();
        }


        public Task UpdateAsync(UserModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteByIdAsync(int modelId)
        {
            throw new System.NotImplementedException();
        }
    }
}