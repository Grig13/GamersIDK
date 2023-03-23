using GamersChatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamersChatAPI.Repositories
{
    public class GamersChatDbContext : DbContext
    {
        public GamersChatDbContext(DbContextOptions<GamersChatDbContext> options) : base(options)
        {

        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<Timeline> Timelines { get; set; }
        public DbSet<User> Users { get; set; }

    }

}
