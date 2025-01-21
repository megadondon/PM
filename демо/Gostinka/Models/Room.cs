using System;
using System.Collections.Generic;

namespace Gostinka.Models;

public partial class Room
{
    public int IdRoom { get; set; }

    public int? RoomNumber { get; set; }

    public int? CategoryId { get; set; }

    public int? Floor { get; set; }

    public int? StatusId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Category? Category { get; set; }

    public virtual Status? Status { get; set; }
}
