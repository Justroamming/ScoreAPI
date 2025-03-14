using System;
using System.Collections.Generic;

namespace ScoreAPI.ModelScore2;

public partial class TblLessonClass
{
    public Guid LessonClassId { get; set; }

    public Guid SubjectId { get; set; }

    public Guid TeacherId { get; set; }

    public DateTime? LessonDate { get; set; }

    public string? Location { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public string? DayOfWeek { get; private set; }
    
    public virtual TblSubject Subject { get; set; } = null!;

    public virtual ICollection<TblClassLesson> TblClassLessons { get; set; } = new List<TblClassLesson>();

    public virtual TblTeacher Teacher { get; set; } = null!;
}
