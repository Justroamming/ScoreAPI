using System;
using System.Collections.Generic;

namespace ScoreAPI.ModelScore2;

public partial class TblGrade
{
    public Guid GradeId { get; set; }

    public Guid StudentId { get; set; }

    public Guid TestId { get; set; }

    public decimal Score { get; set; }

    public Guid TeacherId { get; set; }

    public DateTime? GradeDate { get; set; }

    public virtual TblStudent Student { get; set; } = null!;

    public virtual TblTeacher Teacher { get; set; } = null!;

    public virtual TblTest Test { get; set; } = null!;
}
