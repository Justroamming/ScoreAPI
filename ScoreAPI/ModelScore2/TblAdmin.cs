using System;
using System.Collections.Generic;

namespace ScoreAPI.ModelScore2;

public partial class TblAdmin
{
    public Guid AdminId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

   
    public string Password { get; set; } = null!;
}
