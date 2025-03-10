using System;
using System.Collections.Generic;

namespace ScoreAPI.ModelScore2;

public partial class TblTestWeight
{
    public Guid TestWeightId { get; set; }

    public Guid TestId { get; set; }

    public decimal Weight { get; set; }

    public virtual TblTest Test { get; set; } = null!;
}
