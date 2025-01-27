using System;
using System.Collections.Generic;

namespace Gostinka.Models;

public partial class Service
{
    public int IdService { get; set; }

    public string? ServiceName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public bool? InHour { get; set; }

    public virtual ICollection<BookingsService> BookingsServices { get; set; } = new List<BookingsService>();
}
