using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace RatingService.Domain.Entities;


public partial class Rating
{
    public int RatingId { get; set; }

    public int UserId { get; set; }

    public int VideoId { get; set; }

    public byte Score { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

   
}
