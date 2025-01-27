using System;
using System.Collections.Generic;

namespace Gostinka.Models;

public partial class Status
{
    public int IdStatus { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<RoomsStatus> RoomsStatuses { get; set; } = new List<RoomsStatus>();
}
