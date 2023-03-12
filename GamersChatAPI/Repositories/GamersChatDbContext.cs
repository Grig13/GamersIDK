using GamersChatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Repositories
{
    public class GamersChatDbContext : DbContext
    {
        public GamersChatDbContext(DbContextOptions<GamersChatDbContext> options) : base(options)
        {

        }

        DbSet<Cart> Carts { get; set; }
        DbSet<News> News { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<PostComment> PostComments { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductComment> ProductComments { get; set; }
        DbSet<Timeline> Timelines { get; set; }
        DbSet<User> Users { get; set; }

    }

}
