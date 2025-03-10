using System;
using System.Collections.Generic;

namespace ScoreAPI.ModelScore2;

public partial class TblTeacher
{
    public Guid TeacherId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<TblGrade> TblGrades { get; set; } = new List<TblGrade>();

    public virtual ICollection<TblLessonClass> TblLessonClasses { get; set; } = new List<TblLessonClass>();

    public virtual ICollection<TblTeacherSubject> TblTeacherSubjects { get; set; } = new List<TblTeacherSubject>();
}
