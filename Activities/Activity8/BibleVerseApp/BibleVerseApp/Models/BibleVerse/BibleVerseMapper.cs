namespace BibleVerseApp.Models.BibleVerse
{
    public class BibleVerseMapper : IBibleVerseMapper
    {
        public BibleVerseDTO ToDTO(BibleVerseModel model)
        {
            return new BibleVerseDTO
            {
                BookId = model.BookId,
                VerseId = model.VerseId,
                BookName = model.BookName,
                ChapterNumber = model.ChapterNumber,
                VerseNumber = model.VerseNumber,
                VerseText = model.VerseText,
                FormattedTitle = $"{model.BookName.Substring(0, 3).ToUpper()} {model.ChapterNumber}:{model.VerseNumber} KJV" // FIXME: "1 John" -> "1 J" SHOULD BE "1JO"
            };
        }

        public BibleVerseDTO ToDTO(BibleVerseViewModel viewModel)
        {
            return new BibleVerseDTO
            {
                BookId = viewModel.BookId,
                VerseId = viewModel.VerseId,
                BookName = viewModel.BookName,
                ChapterNumber = viewModel.ChapterNumber,
                VerseNumber = viewModel.VerseNumber,
                VerseText = viewModel.VerseText,
                FormattedTitle = viewModel.FormattedTitle
            };
        }

        public BibleVerseModel ToModel(BibleVerseDTO dto)
        {
            return new BibleVerseModel
            {
                BookId = dto.BookId,
                VerseId = dto.VerseId,
                BookName = dto.BookName,
                ChapterNumber = dto.ChapterNumber,
                VerseNumber = dto.VerseNumber,
                VerseText = dto.VerseText
            };
        }

        public BibleVerseViewModel ToViewModel(BibleVerseDTO dto)
        {
            return new BibleVerseViewModel
            {
                BookId = dto.BookId,
                VerseId = dto.VerseId,
                BookName = dto.BookName,
                ChapterNumber = dto.ChapterNumber,
                VerseNumber = dto.VerseNumber,
                VerseText = dto.VerseText,
                FormattedTitle = dto.FormattedTitle
            };
        }
    }
}
