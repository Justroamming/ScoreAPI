using System;
using System.Collections.Generic;

namespace ScoreAPI.ModelScore2;

public partial class TblCohort
{
    public Guid CohortId { get; set; }

    public string CohortName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<TblClassLesson> TblClassLessons { get; set; } = new List<TblClassLesson>();

    public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();
}
