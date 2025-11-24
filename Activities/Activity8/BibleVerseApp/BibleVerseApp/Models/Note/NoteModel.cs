namespace BibleVerseApp.Models.Note
{
    public class NoteModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ChapterNumber { get; set; }
        public int VerseNumber { get; set; }
        public string NoteText { get; set; }
    }
}
