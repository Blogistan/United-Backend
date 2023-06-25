using Application.Features.Comments.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Comments.Profiles
{
    public class Proiles:Profile
    {
        public Proiles()
        {
            CreateMap<Comment, CommentViewDto>().ForMember(opt=>opt.Id,src=>src.MapFrom(x=>x.Id))
                .ForMember(opt=>opt.UserName,src=>src.MapFrom(x=>x.User.FirstName+' '+x.User.LastName))
                .ForMember(opt=>opt.CommentContent,src=>src.MapFrom(x=>x.CommentContent))
                .ForMember(opt=>opt.GuestName,src=>src.MapFrom(x=>x.GuestName))
                .ForMember(opt=>opt.Dislikes,src=>src.MapFrom(x=>x.Dislikes))
                .ForMember(opt=>opt.Likes,src=>src.MapFrom(x=>x.Likes))
                .ForMember(opt=>opt.SubComments,src=>src.MapFrom(x=>x.CommentResponses)).ReverseMap();
        }
    }
}
