using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Models;

public partial class ProductComment
{
    [Key]
    public Guid Id { get; set; }

    public string? CommentContent { get; set; }

    public Guid? ProductId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? Rating { get; set; }

    public Guid? UserId { get; set; }

    public User? User { get; set; }


}
