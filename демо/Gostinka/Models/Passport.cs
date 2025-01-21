using System;
using System.Collections.Generic;

namespace Gostinka.Models;

public partial class Passport
{
    public int IdPassport { get; set; }

    public string? Serial { get; set; }

    public string? Number { get; set; }

    public string? Address { get; set; }

    public string? WhoIssue { get; set; }

    public DateOnly? IssueDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
