namespace BibleVerseApp.Models.BibleVerse
{
    public interface IBibleVerseMapper
    {
        BibleVerseDTO ToDTO(BibleVerseModel model);
        BibleVerseModel ToModel(BibleVerseDTO dto);
        BibleVerseDTO ToDTO(BibleVerseViewModel viewMoidel);
        BibleVerseViewModel ToViewModel(BibleVerseDTO dto);
    }
}
