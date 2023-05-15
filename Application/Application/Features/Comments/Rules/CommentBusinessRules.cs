using Application.Features.Categories.Dtos;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Bookmarks.Rules
{
    public class CreateBookmarkCommand
    {
        public readonly ICommentRepository commentRepository;
        public CreateBookmarkCommand(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

    }
}
