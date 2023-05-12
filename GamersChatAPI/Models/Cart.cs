using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Models;

public partial class Cart
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    [InverseProperty("Cart")]
    public virtual ICollection<CartItem> CartItems { get; } = new List<CartItem>();
}
