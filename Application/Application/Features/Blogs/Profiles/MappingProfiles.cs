﻿using Application.Features.Blogs.Commands.CreateBlog;
using Application.Features.Blogs.Commands.DeleteBlog;
using Application.Features.Blogs.Commands.UpdateBlog;
using Application.Features.Blogs.Dtos;
using Application.Features.Blogs.Queries.BlogExtendedList;
using Application.Features.Blogs.Queries.GetListBlog;
using Application.Features.Blogs.Queries.GetListBlogDynamic;
using Application.Features.Blogs.Queries.Reports.MostReaded;
using AutoMapper;
using Core.Persistence.Paging;
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
                .ForMember(opt => opt.WriterName, src => src.MapFrom(x => x.Writer.User.FirstName + ' ' + x.Writer.User.LastName))
                .ReverseMap();

            CreateMap<Blog, DeleteBlogCommandResponse>().ForMember(opt => opt.CategoryName, src => src.MapFrom(x => x.Category.CategoryName))
                .ForMember(opt => opt.WriterName, src => src.MapFrom(x => x.Writer.User.FirstName + ' ' + x.Writer.User.LastName))
                .ReverseMap();

            CreateMap<Blog, UpdateBlogCommand>().ReverseMap();
            CreateMap<Blog, UpdateBlogCommandResponse>().ReverseMap();

            CreateMap<Blog, BlogListViewDto>().ForMember(opt => opt.CategoryName, src => src.MapFrom(x => x.Category.CategoryName))
                .ForMember(opt => opt.WriterName, src => src.MapFrom(x => x.Writer.User.FirstName + ' ' + x.Writer.User.LastName))
                .ReverseMap();

            CreateMap<Blog, MostReadedBlogDto>().ForMember(opt => opt.CategoryName, src => src.MapFrom(x => x.Category.CategoryName))
                .ForMember(opt => opt.WriterName, src => src.MapFrom(x => x.Writer.User.FirstName + ' ' + x.Writer.User.LastName))
                .ReverseMap();

            CreateMap<Blog, MostSharedBlogDto>().ForMember(opt => opt.CategoryName, src => src.MapFrom(x => x.Category.CategoryName))
                .ForMember(opt => opt.WriterName, src => src.MapFrom(x => x.Writer.User.FirstName + ' ' + x.Writer.User.LastName))
                .ReverseMap();

            CreateMap<Blog, BlogExtendedViewDto>().ForMember(opt=>opt.Category,src=>src.MapFrom(x=>x.Category))
                .ForMember(opt=>opt.Content,src=>src.MapFrom(x=>x.Content))
                .ForMember(opt=>opt.WriterName,src=>src.MapFrom(x=>x.Writer.User.FirstName+' '+ x.Writer.User.LastName));
                

            CreateMap<IPaginate<Blog>, GetListBlogQueryResponse>().ReverseMap();
            CreateMap<IPaginate<Blog>, MostReadedBlogQueryResponse>().ReverseMap();
            CreateMap<IPaginate<Blog>, MostReadedBlogQueryResponse>().ReverseMap();
            CreateMap<IPaginate<Blog>, GetListBlogDynamicQueryResponse>().ReverseMap();
            CreateMap<IPaginate<Blog>, BlogExtendedListQueryResponse>().ReverseMap();

            



        }
    }
}
