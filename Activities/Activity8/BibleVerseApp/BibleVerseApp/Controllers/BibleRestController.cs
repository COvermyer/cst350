using BibleVerseApp.Models;
using BibleVerseApp.Models.Note;
using BibleVerseApp.Services.BibleVerse;
using BibleVerseApp.Services.Comment;
using Microsoft.AspNetCore.Mvc;

namespace BibleVerseApp.Controllers
{
    [ApiController]
    [Route("api/v1/Bible")]
    public class BibleRestController : ControllerBase
    {
        private readonly IBibleVerseService _bibleVerseService;

        public BibleRestController(IBibleVerseService bibleVerseService)
        {
            _bibleVerseService = bibleVerseService;
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bibleVerseService.GetAllBooksAsync();
            var results = books.Select(b => new { bookId = b.Key, bookName = b.Value });
            return Ok(results);
        }

        [HttpGet("books/{bookId}/chapters")]
        public async Task<IActionResult> GetNumberOfChapters(int bookId)
        {
            var chapters = await _bibleVerseService.GetNumberOfChaptersPerBookAsync(bookId);
            return Ok(Enumerable.Range(1, chapters));
        }

        [HttpGet("books/{bookId}/chapters/{chapterNumber}/verses")]
        public async Task<IActionResult> GetNumberOfVerses(int bookId, int chapterNumber)
        {
            var verses = await _bibleVerseService.GetNumberOfVersesAsync(bookId, chapterNumber);
            return Ok(Enumerable.Range(1, verses));
        }

        [HttpGet("books/{bookId}/chapters/{chapterNumber}/verses/{verseNumber}")]
        public async Task<IActionResult> GetVerseText(int bookId, int chapterNumber, int verseNumber)
        {
            var verse = await _bibleVerseService.GetVerseAsync(bookId, chapterNumber, verseNumber);
            return Ok(new
            {
                bookName = verse.BookName,
                chapter = verse.ChapterNumber,
                verse = verse.VerseNumber,
                text = verse.VerseText
            });
        }

        [HttpGet("books/{bookId}/chapters/{chapterNumber}/allverses")]
        public async Task<IActionResult> GetAllVersesInChapter(int bookId, int chapterNumber, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var total = await _bibleVerseService.GetNumberOfVersesAsync(bookId, chapterNumber);
            var verses = await _bibleVerseService.GetVersesByBookAndChapterAsync(bookId, chapterNumber, page, pageSize);
            
            var results = verses.Select(v => new
            {
                bookId = v.BookId,
                bookName = v.BookName,
                chapterNumber = v.ChapterNumber,
                verseNumber = v.VerseNumber,
                verseText = v.VerseText
            });

            var response = new
            {
                totalResults = total,
                page,
                pageSize,
                results
            };

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchVerses(
            [FromQuery] string query,
			[FromQuery] bool inNewTestament = false,
			[FromQuery] bool inOldTestament = false,
			[FromQuery] int page = 1,
			[FromQuery] int pageSize = 25)
        {
            SearchFor searchFor = new SearchFor
            {
                SearchTerm = query,
                InNewTestament = inNewTestament,
                InOldTestament = inOldTestament
            };
            var results = await _bibleVerseService.SearchForVersesAsync(searchFor, page, pageSize);

            if (results == null || !results.Any())
                return NotFound("No matches found");

            int total = await _bibleVerseService.CountSearchResults(searchFor);

            var response = new
            {
                totalResults = total,
                page,
                pageSize,
                results
            };

            return Ok(response);
        }
    }
}
