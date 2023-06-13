using MediatR;

namespace Application.Features.Bookmarks.Queries.AddToBookmarks
{
    public class AddToBookmarksQuery:IRequest<Unit>
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }


        public class AddToBookmarksQueryHandler : IRequestHandler<AddToBookmarksQuery, Unit>
        {
            public async Task<Unit> Handle(AddToBookmarksQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
