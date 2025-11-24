using BibleVerseApp.Models;
using BibleVerseApp.Models.BibleVerse;

namespace BibleVerseApp.Services.BibleVerse
{
    public interface IBibleVerseService
    {
        Task<IEnumerable<BibleVerseViewModel>> GetAllVersesAsync();
        Task<BibleVerseViewModel> GetVerseByIdAsync(int verseId);
        Task<IEnumerable<BibleVerseViewModel>> GetVersesByBookAndChapterAsync(int bookId, int chapterNumber);
        Task<IEnumerable<BibleVerseViewModel>> GetVersesByBookAndChapterAsync(int bookId, int chapterNumber, int page, int pageSize);
        Task<BibleVerseViewModel> GetVerseAsync(int bookId, int chapterNumber, int verseNumber);
        Task<IEnumerable<BibleVerseViewModel>> SearchForVersesAsync(SearchFor searchFor, int page, int pageSize);
        Task<int> CountSearchResults(SearchFor searchFor);
        Task<Dictionary<int, string>> GetAllBooksAsync();
        Task<int> GetNumberOfChaptersPerBookAsync(int bookId);
        Task<int> GetNumberOfVersesAsync(int bookId, int chapterNumber);
    }
}
