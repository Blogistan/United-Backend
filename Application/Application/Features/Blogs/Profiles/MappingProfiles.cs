using Application.Features.Blogs.Commands.CreateBlog;
using Application.Features.Blogs.Commands.DeleteBlog;
using Application.Features.Blogs.Commands.UpdateBlog;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Blogs.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateBlogCommand, Blog>().ReverseMap();

            CreateMap<UpdateBlogCommand, Blog>().ReverseMap();

            CreateMap<Blog, CreateBlogCommandResponse>().ForMember(opt => opt.CategoryName, src => src.MapFrom(x => x.Category.CategoryName))
                .ForMember(opt => opt.WriterName, src => src.MapFrom(x => x.Writer.FirstName + ' ' + x.Writer.LastName))
                .ReverseMap();

            CreateMap<Blog, DeleteBlogCommandResponse>().ForMember(opt => opt.CategoryName, src => src.MapFrom(x => x.Category.CategoryName))
                .ForMember(opt => opt.WriterName, src => src.MapFrom(x => x.Writer.FirstName + ' ' + x.Writer.LastName))
                .ReverseMap();

            CreateMap<Blog, UpdateBlogCommand>().ReverseMap();
            CreateMap<Blog, UpdateBlogCommandResponse>().ReverseMap();

        }
    }
}
