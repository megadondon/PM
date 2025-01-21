using System;
using System.Collections.Generic;

namespace Gostinka.Models;

public partial class BookingsService
{
    public int? ServiceId { get; set; }

    public int? BookingId { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Service? Service { get; set; }
}
