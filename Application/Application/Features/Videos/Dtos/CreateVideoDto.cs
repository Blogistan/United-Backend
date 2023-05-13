﻿namespace Application.Features.Videos.Dtos
{
    public class CreateVideoDto
    {
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }

        public int WatchCount => 0;
    }
}
