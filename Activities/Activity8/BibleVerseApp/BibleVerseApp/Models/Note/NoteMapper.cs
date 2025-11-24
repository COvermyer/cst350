namespace BibleVerseApp.Models.Note
{
    public class NoteMapper : INoteMapper
    {
        public NoteModel ToModel(NoteViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new NoteModel
            {
                Id = viewModel.Id,
                BookId = viewModel.BookId,
                ChapterNumber = viewModel.ChapterNumber,
                VerseNumber = viewModel.VerseNumber,
                NoteText = viewModel.NoteText
            };

        }

        public NoteViewModel ToViewModel(NoteModel model)
        {
            if (model == null)
                return null;

            return new NoteViewModel
            {
                Id = model.Id,
                BookId = model.BookId,
                ChapterNumber = model.ChapterNumber,
                VerseNumber = model.VerseNumber,
                NoteText = model.NoteText
            };
        }
    }
}
