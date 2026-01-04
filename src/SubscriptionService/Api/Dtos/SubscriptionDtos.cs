using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Api.Dtos
{

    public class CreateSubscriptionDto
    {
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public DateOnly StartDate { get; set; }
    }


    public class SubscriptionResponseDto
    {
        public int SubscriptionId { get; set; }
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class ActiveSubscriptionsPerPlanDto
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; } = default!;
        public int ActiveCount { get; set; }
    }

}
