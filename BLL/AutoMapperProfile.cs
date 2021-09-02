using System.Linq;
using AutoMapper;
using BLL.Models;
using DAL.Models;

namespace BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostModel>()
                .ForMember(p => p.CommentIds, c =>
                    c.MapFrom(posts => posts.Comments.Select(x => x.Id)))
                .ForMember(p => p.AuthorId, c => 
                    c.MapFrom(x => x.AuthorId))
                .ForMember(p => p.Author, c=>c.MapFrom(x=> x.Author))
                .ReverseMap();
            CreateMap<Comment, CommentModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}