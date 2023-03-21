using GamersChatAPI.Repositories;
using GamersChatAPI.Repositories.Interfaces;
using GamersChatAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Add services to the container.
builder.Services.AddScoped<NewsService>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();

builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddScoped<PostCommentService>();
builder.Services.AddScoped<IPostCommentRepository, PostCommentRepository>();

builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddScoped<ProductCommentService>();
builder.Services.AddScoped<IProductCommentRepository, ProductCommentRepository>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<TimelineService>();
builder.Services.AddScoped<ITimelineRepository, TimelineRepository>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();

builder.Services.AddDbContext<GamersChatDbContext>(options =>
    options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(a =>
    {
        a.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
