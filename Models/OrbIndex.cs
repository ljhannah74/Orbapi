using System;
using System.Collections.Generic;

namespace orbapi.Models;

public partial class OrbIndex
{
    public long? OrdIndexId { get; set; }

    public long? StateId { get; set; }

    public long? CountyId { get; set; }

    public long? LocalityId { get; set; }

    public long? OrbIndexTypeId { get; set; }

    public string? IndexName { get; set; }

    public string? Url { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
}
