using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.Api.Dtos
{
    public class CreateRatingDto
    {
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public byte Score { get; set; } // 1-5
        public string? Comment { get; set; }
    }

    public class RatingResponseDto
    {
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public byte Score { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }


    public class VideoRatingStatsDto
    {
        public int VideoId { get; set; }
        public double AvgScore { get; set; }
        public int RatingCount { get; set; }
    }

   


}
