using GamersChatAPI.Repositories;
using GamersChatAPI.Repositories.Interfaces;
using GamersChatAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GamersChatAPI.Data;
using GamersChatAPI.Areas.Identity.Data;

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddDbContext<GamersChatDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<GamersChatAPIContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<GamersChatAPIUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<GamersChatAPIContext>();
builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowOrigin");

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
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();
app.UseRouting();


app.UseCors();

app.MapControllers();

app.Run();
