using System.ComponentModel.DataAnnotations;

namespace BibleVerseApp.Models.Note
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ChapterNumber { get; set; }
        public int VerseNumber { get; set; }

        [Required]
        public string NoteText { get; set; }
    }
}
