using Application.Features.Comments.Dtos;
using Application.Features.Comments.Queries.GetBlogCommentsQuery;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Comments.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Comment, CommentViewDto>().ForMember(opt => opt.Id, src => src.MapFrom(x => x.Id))
                .ForMember(opt => opt.UserName, src => src.MapFrom(x => x.SiteUser.User.FirstName + ' ' + x.SiteUser.User.LastName))
                .ForMember(opt => opt.CommentContent, src => src.MapFrom(x => x.CommentContent))
                .ForMember(opt => opt.GuestName, src => src.MapFrom(x => x.GuestName))
                .ForMember(opt => opt.Dislikes, src => src.MapFrom(x => x.Dislikes))
                .ForMember(opt => opt.Likes, src => src.MapFrom(x => x.Likes))
                .ForMember(opt => opt.CreateDate, src => src.MapFrom(x => x.CreatedDate))
                .ForMember(opt => opt.ProfileImageUrl, src => src.MapFrom(x => x.SiteUser.ProfileImageUrl))
                .ForMember(opt => opt.CommentResponses, src => src.MapFrom(x => x.CommentResponses)).ReverseMap();

            CreateMap<IPaginate<Comment>, GetBlogCommentsQueryResponse>().ReverseMap();
        }
    }
}
