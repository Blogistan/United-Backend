using Application.Features.Videos.Dtos;
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
        public async Task VideoCannotBeDuplicatedWhenInsertedRange(List<CreateVideoDto> createVideoDtos)
        {
            foreach (var item in createVideoDtos)
            {
                Video video = await videoRepository.GetAsync(x => x.Title == item.Title && x.VideoUrl == item.VideoUrl);
                if (video is not null)
                    throw new BusinessException($"Video Title:{item.Title} , Url:{item.VideoUrl} is exist");
            }
        }
        public async Task VideoCannotBeDuplicatedWhenUpdated(string videoUrl)
        {
            Video video = await videoRepository.GetAsync(x => x.VideoUrl == videoUrl);
            if (video is not null)
                throw new BusinessException("Video is exist");
        }
        public async Task VideoCannotBeDuplicatedWhenUpdated(List<UpdateVideoDto> updateVideoDtos)
        {
            foreach (var item in updateVideoDtos)
            {
                Video video = await videoRepository.GetAsync(x => x.VideoUrl == item.VideoUrl);
                if (video is not null)
                    throw new BusinessException($"Video Title:{item.Title} , Url:{item.VideoUrl} is exist");
            }
        }
        public async Task<Video> VideoCheckById(int id)
        {
            Video video = await videoRepository.GetAsync(x => x.Id == id);
            if (video == null) throw new BusinessException("Video is not exist");

            return video;
        }
        public async Task<List<Video>> VideoCheckById(List<DeleteRangeDto> deleteRangeDtos)
        {
            List<Video> videos = new();

            foreach (var item in deleteRangeDtos)
            {
                Video video = await videoRepository.GetAsync(x => x.Id == item.VideoIds);
                if (video == null) throw new BusinessException($"Video Id:{item.VideoIds} is not exist");

                videos.Add(video);
            }
            return videos;


        }
    }
}
