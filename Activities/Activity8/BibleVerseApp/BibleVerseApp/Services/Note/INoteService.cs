using BibleVerseApp.Models.Note;

namespace BibleVerseApp.Services.Comment
{
    public interface INoteService
    {
        Task<IEnumerable<NoteViewModel>> GetAllNotesAsync();
        Task<NoteViewModel> GetNoteByIdAsync(int id);
        Task<NoteViewModel> GetNoteAsync(int bookId, int chapterNumber, int verseNumber);
        Task<IEnumerable<NoteViewModel>> GetNotesByBookIdAsync(int bookId);
        Task<int> AddNoteAsync(NoteViewModel noteViewModel);
        Task UpdateNoteAsync(NoteViewModel noteViewModel);
        Task DeleteNoteAsync(int noteId);
        Task DeleteNoteAsync(NoteViewModel noteViewModel);
    }
}
