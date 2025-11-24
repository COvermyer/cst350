using BibleVerseApp.Models.Note;

namespace BibleVerseApp.Services.Comment
{
    public interface INoteDAO
    {
        Task<IEnumerable<NoteModel>> GetAllNotesAsync();
        Task<NoteModel> GetNoteByIdAsync(int id);
        Task<NoteModel> GetNoteAsync(int bookId, int chapterNumber, int verseNumber);
        Task<IEnumerable<NoteModel>> GetNotesByBookIdAsync(int bookId);
        Task<int> AddNoteAsync(NoteModel note);
        Task UpdateNoteAsync(NoteModel note);
        Task DeleteNoteAsync(int noteId);
        Task DeleteNoteAsync(NoteModel note);
    }
}
