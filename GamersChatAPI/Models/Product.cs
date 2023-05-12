using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Models;

public partial class Product
{
    [Key]
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int Price { get; set; }

    public string? Category { get; set; }

    public string? ImageUrl { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();
}
