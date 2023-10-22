using System;
using System.Collections.Generic;

namespace orbapi.Models;

public partial class Locality
{
    public long? LocalityId { get; set; }

    public long? StateId { get; set; }

    public long? CountyId { get; set; }

    public string? LocalityName { get; set; }
}
