using System;
using System.Collections.Generic;

namespace orbapi.Models;

public partial class County
{
    public long? CountyId { get; set; }

    public long? StateId { get; set; }

    public string? CountyName { get; set; }
}
