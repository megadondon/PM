using System;
using System.Collections.Generic;

namespace Gostinka.Models;

public partial class Booking
{
    public int IdBooking { get; set; }

    public int? RoomId { get; set; }

    public int? ClientId { get; set; }

    public DateOnly? ArrivalDate { get; set; }

    public DateOnly? DepartureDate { get; set; }

    public decimal? Amount { get; set; }

    public virtual ICollection<BookingsService> BookingsServices { get; set; } = new List<BookingsService>();

    public virtual User? Client { get; set; }

    public virtual Room? Room { get; set; }
}
