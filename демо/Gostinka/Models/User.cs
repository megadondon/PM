using System;
using System.Collections.Generic;

namespace Gostinka.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Patronymic { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? PassportId { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<CleaningSchedule> CleaningSchedules { get; set; } = new List<CleaningSchedule>();

    public virtual Passport? Passport { get; set; }
}
