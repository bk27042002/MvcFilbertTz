using System;
using System.Collections.Generic;

namespace MvcFilbertTz.Models;

public partial class Passport
{
    public int Id { get; set; }

    public int RPersonId { get; set; }

    public string? Series { get; set; }

    public string? Number { get; set; }

    public Person? Person { get; set; }
}
