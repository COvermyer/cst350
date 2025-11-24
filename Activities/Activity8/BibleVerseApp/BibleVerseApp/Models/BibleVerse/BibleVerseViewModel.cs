namespace BibleVerseApp.Models.BibleVerse
{
    public class BibleVerseViewModel
    {
        // Shared between models
        public int BookId { get; set; }

        // From `KJV_verses` table - VerseModel
        public int VerseId { get; set; }
        // From `KJV_books` table - BookModel
        public string BookName { get; set; }

        public int ChapterNumber { get; set; }
        public int VerseNumber { get; set; }
        public string VerseText { get; set; }

        // Formats as {BookName[3 Letter Code]} {ChapterNumber}:{VerseNumber}
        public string FormattedTitle { get; set; }
    }
}
