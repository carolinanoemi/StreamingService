using System;
using System.Collections.Generic;

namespace SubscriptionService.Domain.Entities;

public partial class Plan
{
    public int PlanId { get; set; }

    public string Name { get; set; } = null!;

    public decimal PricePerMonth { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
