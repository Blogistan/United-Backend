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

        public async Task VideoCannotBeDuplicatedWhenInserted(string videoName)
        {
            Video video = await videoRepository.GetAsync(x => x.Title == videoName);
            if (video is not null)
                throw new BusinessException("Video is exist");
        }
        public async Task VideoCannotBeDuplicatedWhenUpdated(string videoName)
        {
            Video video = await videoRepository.GetAsync(x => x.Title == videoName);
            if (video is not null)
                throw new BusinessException("Video is exist");
        }
        public async Task<Video> CategoryCheckById(int id)
        {
            Video video = await videoRepository.GetAsync(x => x.Id == id);
            if (video == null) throw new BusinessException("Video is not exist");

            return video;
        }
    }
}
