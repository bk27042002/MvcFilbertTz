using System;
using System.Collections.Generic;

namespace MvcFilbertTz.Models;

public partial class Person
{
    public int Id { get; set; }

    public string? F { get; set; }

    public string? I { get; set; }

    public string? O { get; set; }

    public DateOnly BirthDate { get; set; }

    public ICollection<Passport>? Passports { get; set; }
    public ICollection<Debt>? Debts { get; set; }
}
