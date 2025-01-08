using System;
using System.Collections.Generic;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class NotificationType
{
    public int NotificationTypeId { get; set; }

    public string? NotificationTypeCode { get; set; }

    public string? Icon { get; set; }

    public string? NotificationType1 { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
