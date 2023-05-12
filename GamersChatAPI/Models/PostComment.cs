using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Models;
public partial class PostComment
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? PostId { get; set; }

    public string CommentContent { get; set; } = null!;

    [ForeignKey("PostId")]
    [InverseProperty("PostComments")]
    public virtual Post? Post { get; set; }
}
