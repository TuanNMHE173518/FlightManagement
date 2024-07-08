using System;
using System.Collections.Generic;

namespace BussinessObjects;

public partial class Airline
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
