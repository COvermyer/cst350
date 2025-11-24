namespace BibleVerseApp.Models.Note
{
    public interface INoteMapper
    {
        NoteViewModel ToViewModel(NoteModel model);
        NoteModel ToModel(NoteViewModel viewModel);
    }
}
