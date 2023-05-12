using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Models;

public partial class Timeline
{
    [Key]
    public Guid Id { get; set; }

    [InverseProperty("Timeline")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
