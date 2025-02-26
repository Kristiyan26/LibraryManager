using LibraryManager.Core.Entities;
using LibraryManager.Data;
using LibraryManager.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<LibraryManagerDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                .UseLazyLoadingProxies();
            });
            builder.Services.AddSession();


            builder.Services.AddScoped<AuthorsRepository>();
            builder.Services.AddScoped<BooksRepository>();
            builder.Services.AddScoped<GenresRepository>();
            builder.Services.AddScoped<BookAuthorsRepository>();
            builder.Services.AddScoped<BorrowingsRepository>();
            builder.Services.AddScoped<MembersRepository>();
            builder.Services.AddScoped<IPasswordHasher<Member>, PasswordHasher<Member>>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
