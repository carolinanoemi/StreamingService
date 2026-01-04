using System;
using System.Collections.Generic;

namespace SubscriptionService.Domain.Entities;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public int UserId { get; set; }

    public int PlanId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool IsActive { get; set; }

    public virtual Plan Plan { get; set; } = null!;
}
