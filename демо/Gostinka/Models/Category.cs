using System;
using System.Collections.Generic;

namespace Gostinka.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string? CategoryName { get; set; }

    public decimal? PricePerDay { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    
    public string PricePerDayWithValue
    {
        get
        {
            return this.PricePerDay + " рублей";
        }
    }
}
