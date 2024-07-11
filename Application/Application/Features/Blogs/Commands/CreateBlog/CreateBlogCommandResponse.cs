using Core.Application.Responses;

namespace Application.Features.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommandResponse:IResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string BannerImageUrl { get; set; }
        public string WriterName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ReactionSuprisedCount { get; set; }
        public int ReactionLovelyCount { get; set; }
        public int ReactionSadCount { get; set; }
        public int ReactionKEKWCount { get; set; }
        public int ReactionTriggeredCount { get; set; }
        public int ShareCount { get; set; }
        public int ReadCount { get; set; }

        public CreateBlogCommandResponse()
        {
            
        }
        public CreateBlogCommandResponse(int ıd, string title, string categoryName, string bannerImageUrl, DateTime createdDate, int reactionSuprisedCount, int reactionLovelyCount, int reactionSadCount, int reactionKEKWCount, int reactionTriggeredCount, int shareCount, int readCount)
        {
            Id = ıd;
            Title = title;
            CategoryName = categoryName;
            BannerImageUrl = bannerImageUrl;
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
