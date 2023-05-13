using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Videos.Rules
{
    public class VideoBusinessRules
    {
        private readonly IVideoRepository videoRepository;
        public VideoBusinessRules(IVideoRepository videoRepository)
        {
            this.videoRepository = videoRepository;
        }

        public async Task VideoCannotBeDuplicatedWhenInserted(string videoName, string videoUrl)
        {
            Video video = await videoRepository.GetAsync(x => x.Title == videoName && x.VideoUrl == videoUrl);
            if (video is not null)
                throw new BusinessException($"Video Title:{videoName} , Url:{videoUrl} is exist");
        }
        public async Task VideoCannotBeDuplicatedWhenUpdated(string videoUrl)
        {
            Video video = await videoRepository.GetAsync(x=>x.VideoUrl==videoUrl);
            if (video is not null)
                throw new BusinessException("Video is exist");
        }
        public async Task<Video> VideoCheckById(int id)
        {
            Video video = await videoRepository.GetAsync(x => x.Id == id);
            if (video == null) throw new BusinessException("Video is not exist");

            return video;
        }
    }
}
