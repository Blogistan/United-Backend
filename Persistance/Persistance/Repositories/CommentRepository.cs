using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class CommentRepository : EfRepositoryBase<Comment, int,EFDbContext>, ICommentRepository
    {
        public CommentRepository(EFDbContext context) : base(context)
        {
        }
    }
}
