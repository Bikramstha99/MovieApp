using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Repository.Interface;
using MovieApplication.Repository.Implementations;
using Microsoft.AspNetCore.Identity;
using MovieApplication.Repository.Interfaces;
using MovieListing.Areas.Identity.Data;
using MovieApp.Repository.Implementation;
using MovieApp.Repository.SPImplementation;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseSqlServer(builder.Configuration
    .GetConnectionString("MvcConnectionString")));



builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MovieDbContext>();

if (builder.Configuration.GetValue<bool>("UseSP")) //To either use EF or SP 
{
    builder.Services.AddScoped<ICommentRepository, SPCommentRepository>();
    builder.Services.AddScoped<IRatingRepository, SPRatingRepository>();
    builder.Services.AddScoped<IMovieRepository, SPMovieRepository>();
}
else
{
    builder.Services.AddScoped<ICommentRepository, CommentRepository>();
    builder.Services.AddScoped<IRatingRepository, RatingRepository>();
    builder.Services.AddScoped<IMovieRepository, MovieRepository>();

}
builder.Services.AddScoped<IDbInitializerRepository, DbInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movie}/{action=Index}/{id?}");

SeedDatabase();

app.Run();
void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializerRepository>();
        dbInitializer.Initalizer();
    }
}
