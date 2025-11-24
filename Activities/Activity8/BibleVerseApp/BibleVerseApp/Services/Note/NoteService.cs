using BibleVerseApp.Models.Note;
using BibleVerseApp.Services.Comment;

namespace BibleVerseApp.Services.Note
{
    public class NoteService : INoteService
    {
        private readonly INoteDAO _noteDAO;
        private readonly INoteMapper _noteMapper;

        public NoteService(INoteDAO noteDAO, INoteMapper noteMapper)
        {
            _noteDAO = noteDAO;
            _noteMapper = noteMapper;
        }

        public async Task<int> AddNoteAsync(NoteViewModel noteViewModel)
        {
            return await _noteDAO.AddNoteAsync(_noteMapper.ToModel(noteViewModel));
        }

        public async Task DeleteNoteAsync(int noteId)
        {
            await _noteDAO.DeleteNoteAsync(noteId);
        }

        public async Task DeleteNoteAsync(NoteViewModel noteViewModel)
        {
            await DeleteNoteAsync(noteViewModel.Id);
        }

        public async Task<IEnumerable<NoteViewModel>> GetAllNotesAsync()
        {
            IEnumerable<NoteModel> models = await _noteDAO.GetAllNotesAsync();
            List<NoteViewModel> vms = new List<NoteViewModel>();
            foreach (var model in models)
            {
                vms.Add(_noteMapper.ToViewModel(model));
            }
            return vms;
        }

        public async Task<NoteViewModel> GetNoteAsync(int bookId, int chapterNumber, int verseNumber)
        {
            NoteModel model = await _noteDAO.GetNoteAsync(bookId, chapterNumber, verseNumber);
            return _noteMapper.ToViewModel(model);
        }

        public async Task<NoteViewModel> GetNoteByIdAsync(int id)
        {
            NoteModel model = await _noteDAO.GetNoteByIdAsync(id);
            return _noteMapper.ToViewModel(model);
        }

        public async Task<IEnumerable<NoteViewModel>> GetNotesByBookIdAsync(int bookId)
        {
            IEnumerable<NoteModel> models = await _noteDAO.GetAllNotesAsync();
            List<NoteViewModel> vms = new List<NoteViewModel>();
            foreach (var model in models)
            {
                vms.Add(_noteMapper.ToViewModel(model));
            }
            return vms;
        }

        public async Task UpdateNoteAsync(NoteViewModel noteViewModel)
        {
            NoteModel model = _noteMapper.ToModel(noteViewModel);
            await _noteDAO.UpdateNoteAsync(model);
        }
    }
}
