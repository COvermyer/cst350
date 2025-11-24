namespace BibleVerseApp.Models.BibleVerse
{
    public class BibleVerseModel
    {
        public int BookId { get; set; }
        public int VerseId { get; set; }


        public string BookName { get; set; }
        public int ChapterNumber { get; set; }
        public int VerseNumber { get; set; }

        public string VerseText { get; set; }
    }
}
