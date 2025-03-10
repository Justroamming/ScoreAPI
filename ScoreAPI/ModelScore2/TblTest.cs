using System;
using System.Collections.Generic;

namespace ScoreAPI.ModelScore2;

public partial class TblTest
{
    public Guid TestId { get; set; }

    public Guid SubjectId { get; set; }

    public string TestType { get; set; } = null!;

    public DateOnly? TestDate { get; set; }

    public virtual TblSubject Subject { get; set; } = null!;

    public virtual ICollection<TblGrade> TblGrades { get; set; } = new List<TblGrade>();

    public virtual ICollection<TblTestWeight> TblTestWeights { get; set; } = new List<TblTestWeight>();
}
