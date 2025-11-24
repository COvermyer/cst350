using BibleVerseApp.Models;
using BibleVerseApp.Models.BibleVerse;
using Microsoft.AspNetCore.SignalR;

namespace BibleVerseApp.Services.BibleVerse
{
    public interface IBibleVerseDAO
    {
        Task<IEnumerable<BibleVerseModel>> GetAllVersesAsync();
        Task<BibleVerseModel> GetVerseByIdAsync(int verseId);
        Task<IEnumerable<BibleVerseModel>> GetVersesByBookAndChapterAsync(int bookId, int chapterNumber);
        Task<IEnumerable<BibleVerseModel>> GetVersesByBookAndChapterAsync(int bookId, int chapterNumber, int page, int pageSize);
        Task<BibleVerseModel> GetVerseAsync(int bookId, int chapterNumber, int verseNumber);
        Task<IEnumerable<BibleVerseModel>> SearchForVersesAsync(SearchFor searchFor, int page, int pageSize);
        Task<int> CountSearchResults(SearchFor searchFor);
        Task<Dictionary<int, string>> GetAllBooksAsync();
        Task<int> GetNumberOfChaptersPerBookAsync(int bookId);
        Task<int> GetNumberOfVersesAsync(int bookId, int chapterNumber);
    }
}
