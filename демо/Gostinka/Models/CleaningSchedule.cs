using System;
using System.Collections.Generic;

namespace Gostinka.Models;

public partial class CleaningSchedule
{
    public int IdCleaning { get; set; }

    public DateOnly? CleaningDate { get; set; }

    public int? Floor { get; set; }

    public int? CleanerId { get; set; }

    public virtual User? Cleaner { get; set; }
}
