using System;
using System.Collections.Generic;

namespace MvcFilbertTz.Models;

public partial class Debt
{
    public int Id { get; set; }

    public int RPersonId { get; set; }

    public string? ContractNumber { get; set; }

    public DateOnly ContractDt { get; set; }

    public string? DebtSum { get; set; }

    public Person? Person { get; set; }
}
