using System;
using System.Collections.Generic;

namespace Gostinka.Models;

public partial class Status
{
    public int IdStatus { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
