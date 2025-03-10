using System;
using System.Collections.Generic;

namespace ScoreAPI.ModelScore2;

public partial class TblTeacherSubject
{
    public Guid TeacherSubjectId { get; set; }

    public Guid TeacherId { get; set; }

    public Guid SubjectId { get; set; }

    public virtual TblSubject Subject { get; set; } = null!;

    public virtual TblTeacher Teacher { get; set; } = null!;
}
