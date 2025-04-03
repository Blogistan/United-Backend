using Core.Application.Responses;

namespace Application.Features.Blogs.Commands.DeleteBlog
{
    public record DeleteBlogCommandResponse : IResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string BannerImageUrl { get; set; } = string.Empty;
        public string WriterName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int ReactionSuprisedCount { get; set; }
        public int ReactionLovelyCount { get; set; }
        public int ReactionSadCount { get; set; }
        public int ReactionKEKWCount { get; set; }
        public int ReactionTriggeredCount { get; set; }
        public int ShareCount { get; set; }
        public int ReadCount { get; set; }


        public DeleteBlogCommandResponse()
        {

        }

        public DeleteBlogCommandResponse(int ıd, string title, string categoryName, string bannerImageUrl, string writerName, DateTime createdDate, int reactionSuprisedCount, int reactionLovelyCount, int reactionSadCount, int reactionKEKWCount, int reactionTriggeredCount, int shareCount, int readCount)
        {
            Id = ıd;
            Title = title;
            CategoryName = categoryName;
            BannerImageUrl = bannerImageUrl;
            WriterName = writerName;
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
