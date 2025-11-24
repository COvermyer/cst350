using BibleVerseApp.Models.BibleVerse;
using BibleVerseApp.Models.Note;
using BibleVerseApp.Services.BibleVerse;
using BibleVerseApp.Services.Comment;
using Microsoft.AspNetCore.Mvc;

namespace BibleVerseApp.Controllers
{
    public class BibleController : Controller
    {
        private readonly IBibleVerseService _bibleVerseService;
        private readonly INoteService _noteService;

        public BibleController(IBibleVerseService bibleVerseService, INoteService noteService)
        {
            _bibleVerseService = bibleVerseService;
            _noteService = noteService;
        }

        public IActionResult Index()
        {
            return View("ReferenceSearch");
        }

        public IActionResult ShowSearch()
        {
            return View();
        }

        public IActionResult NotateVerse(int bookId = 1, int chapterNumber = 1, int verseNumber = 1)
        {
            return View();
        }
    }
}
