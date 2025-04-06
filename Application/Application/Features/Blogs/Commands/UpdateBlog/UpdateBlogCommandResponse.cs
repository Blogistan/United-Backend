using Core.Application.Responses;

namespace Application.Features.Blogs.Commands.UpdateBlog
{
    public record UpdateBlogCommandResponse :IResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string BannerImageUrl { get; set; }
        public int WriterId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ReactionSuprisedCount { get; set; }
        public int ReactionLovelyCount { get; set; }
        public int ReactionSadCount { get; set; }
        public int ReactionKEKWCount { get; set; }
        public int ReactionTriggeredCount { get; set; }
        public int ShareCount { get; set; }
        public int ReadCount { get; set; }


        public UpdateBlogCommandResponse()
        {
            
        }
        public UpdateBlogCommandResponse(int ıd, string title, int categoryId, string bannerImageUrl, int writerId, DateTime createdDate, int reactionSuprisedCount, int reactionLovelyCount, int reactionSadCount, int reactionKEKWCount, int reactionTriggeredCount, int shareCount, int readCount)
        {
            Id = ıd;
            Title = title;
            CategoryId = categoryId;
            BannerImageUrl = bannerImageUrl;
            WriterId = writerId;
            CreatedDate = createdDate;
            ReactionSuprisedCount = reactionSuprisedCount;
            ReactionLovelyCount = reactionLovelyCount;
            ReactionSadCount = reactionSadCount;
            ReactionKEKWCount = reactionKEKWCount;
            ReactionTriggeredCount = reactionTriggeredCount;
            ShareCount = shareCount;
            ReadCount = readCount;
        }


    }
}
