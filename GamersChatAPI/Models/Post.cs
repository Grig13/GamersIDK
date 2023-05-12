using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Models;

public partial class Post
{
    [Key]
    public Guid Id { get; set; }

    public string PostContent { get; set; } = null!;

    public string? PostImage { get; set; }

    public Guid UserId { get; set; }

    public Guid? TimelineId { get; set; }

    [InverseProperty("Post")]
    public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();

    [ForeignKey("TimelineId")]
    [InverseProperty("Posts")]
    public virtual Timeline? Timeline { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Posts")]
    public virtual User User { get; set; } = null!;
}
