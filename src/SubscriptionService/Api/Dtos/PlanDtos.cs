using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Api.Dtos
{
    public class CreatePlanDto
    {
        public string Name { get; set; } = "";
        public decimal PricePerMonth { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class PlanResponseDto
    {
        public int PlanId { get; set; }
        public string Name { get; set; } = "";
        public decimal PricePerMonth { get; set; }
        public bool IsActive { get; set; }
    }



}
