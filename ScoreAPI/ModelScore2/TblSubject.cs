using System;
using System.Collections.Generic;

namespace ScoreAPI.ModelScore2;

public partial class TblSubject
{
    public Guid SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;

    public virtual ICollection<TblLessonClass> TblLessonClasses { get; set; } = new List<TblLessonClass>();

    public virtual ICollection<TblTeacherSubject> TblTeacherSubjects { get; set; } = new List<TblTeacherSubject>();

    public virtual ICollection<TblTest> TblTests { get; set; } = new List<TblTest>();
}
