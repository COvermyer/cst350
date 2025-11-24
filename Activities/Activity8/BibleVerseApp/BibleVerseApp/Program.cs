using BibleVerseApp.Models.BibleVerse;
using BibleVerseApp.Models.Note;
using BibleVerseApp.Services.BibleVerse;
using BibleVerseApp.Services.Comment;
using BibleVerseApp.Services.Note;

namespace BibleVerseApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // BibleVerseService
            builder.Services.AddScoped<IBibleVerseDAO>(serviceProvider =>
                new BibleVerseDAO(builder.Configuration["SQLConnection:DefaultConnection"])
            );
            builder.Services.AddScoped<IBibleVerseService, BibleVerseService>();
            builder.Services.AddSingleton<IBibleVerseMapper, BibleVerseMapper>();

            // NoteService
            builder.Services.AddScoped<INoteDAO>(serviceProvider =>
                new NoteDAO(builder.Configuration["SQLConnection:DefaultConnection"])
            );
            builder.Services.AddScoped<INoteService, NoteService>();
            builder.Services.AddSingleton<INoteMapper, NoteMapper>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
