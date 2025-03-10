using System;
using System.Collections.Generic;

namespace ScoreAPI.ModelScore2;

public partial class TblClassLesson
{
    public Guid ClassLessonId { get; set; }

    public Guid CohortId { get; set; }

    public Guid LessonClassId { get; set; }

    public virtual TblCohort Cohort { get; set; } = null!;

    public virtual TblLessonClass LessonClass { get; set; } = null!;
}
