using Application.Features.Blogs.Dtos;
using Application.Features.Bookmarks.Queries.GetListBookmarks;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Bookmarks.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Bookmark, BlogListViewDto>().ForMember(opt => opt.Id, src => src.MapFrom(x => x.Blog.Id))
                .ForMember(opt => opt.CategoryName, src => src.MapFrom(x => x.Blog.Category.CategoryName))
                .ForMember(opt => opt.BannerImageUrl, src => src.MapFrom(x => x.Blog.BannerImageUrl))
                .ForMember(opt => opt.ReactionKEKWCount, src => src.MapFrom(x => x.Blog.ReactionKEKWCount))
                .ForMember(opt => opt.ReactionLovelyCount, src => src.MapFrom(x => x.Blog.ReactionLovelyCount))
                .ForMember(opt => opt.ReactionSadCount, src => src.MapFrom(x => x.Blog.ReactionSadCount))
                .ForMember(opt => opt.ReactionSuprisedCount, src => src.MapFrom(x => x.Blog.ReactionSuprisedCount))
                .ForMember(opt => opt.ReactionTriggeredCount, src => src.MapFrom(x => x.Blog.ReactionTriggeredCount))
                .ForMember(opt => opt.Title, src => src.MapFrom(x => x.Blog.Title))
                .ForMember(opt => opt.ShareCount, src => src.MapFrom(x => x.Blog.ShareCount))
                .ForMember(opt => opt.WriterName, src => src.MapFrom(x => x.Blog.Writer.FirstName + ' ' + x.Blog.Writer.LastName))
                .ForMember(opt => opt.CategoryName, src => src.MapFrom(x => x.Blog.Category.CategoryName)).ReverseMap();


            CreateMap<SiteUser, GetListBookmarkQueryResponse>().ForMember(opt => opt.Items, src => src.MapFrom(x => x.Bookmarks)).ReverseMap();
        }
    }
}
