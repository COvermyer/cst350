using BibleVerseApp.Models;
using BibleVerseApp.Models.BibleVerse;
using BibleVerseApp.Models.Note;
using BibleVerseApp.Services.BibleVerse;
using BibleVerseApp.Services.Comment;
using BibleVerseApp.Services.Note;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Diagnostics;

namespace BibleVerseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBibleVerseService _bibleVerseService;
        private readonly INoteService _noteService;

        public HomeController(ILogger<HomeController> logger, IBibleVerseService bibleVerseService, INoteService noteService)
        {
            _logger = logger;
            _bibleVerseService = bibleVerseService;
            _noteService = noteService;
        }

        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Bible");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
