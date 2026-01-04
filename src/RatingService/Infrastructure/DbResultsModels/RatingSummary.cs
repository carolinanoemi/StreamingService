using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.Infrastructure.DbResultModels
{

    // Result-model til output fra stored procedure (ikke en database-entity)
    // Eksisterer ikke i vores entity, da de kun bruges til at "holde" data fra GetRatingSummary SP
    public class RatingSummary
    {
        public int TotalRatings { get; set; } 
        public double AverageScore { get; set; } 
    }


}
