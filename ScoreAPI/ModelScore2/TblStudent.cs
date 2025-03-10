using System;
using System.Collections.Generic;

namespace ScoreAPI.ModelScore2;

public partial class TblStudent
{
    public Guid StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Password { get; set; } = null!;

    public Guid? CohortId { get; set; }

    public virtual TblCohort? Cohort { get; set; }

    public virtual ICollection<TblGrade> TblGrades { get; set; } = new List<TblGrade>();
}
