using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Models;

public partial class News
{
    [Key]
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public string? Image { get; set; }

    public string? Attachment { get; set; }

    public string? Title { get; set; }
}
