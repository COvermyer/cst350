using BibleVerseApp.Models.Note;
using BibleVerseApp.Services.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BibleVerseApp.Controllers
{
    [ApiController]
    [Route("api/v1/Note")]
    public class NoteRestController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteRestController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("{bookId}/{chapterNumber}/{verseNumber}")]
        public async Task<IActionResult> GetNote(int bookId, int chapterNumber, int verseNumber)
        {
            var note = await _noteService.GetNoteAsync(bookId, chapterNumber, verseNumber);

            if (note == null)
            {
                note = new NoteViewModel
                {
                    BookId = bookId,
                    ChapterNumber = chapterNumber,
                    VerseNumber = verseNumber,
                    NoteText = ""
                };
            }
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] NoteViewModel note)
        {
            if (note == null)
                return BadRequest("Note cannot be null");

            int id = await _noteService.AddNoteAsync(note);
            return CreatedAtAction(nameof(GetNote), 
                new {
                    bookId = note.BookId,
                    chapterNumber = note.ChapterNumber,
                    verseNumber = note.VerseNumber
                }, 
                note);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNote([FromBody] NoteViewModel note)
        {
            if (note == null)
                return BadRequest("Note cannot be null");

            var existingNote = await _noteService.GetNoteAsync(note.BookId, note.ChapterNumber, note.VerseNumber);
            if (existingNote == null)
                return NotFound($"Note not found");

            existingNote.NoteText = note.NoteText;
            await _noteService.UpdateNoteAsync(existingNote);
            return Ok(existingNote);
        }

        [HttpPost("SaveNote")] // create-or-update method
        public async Task<IActionResult> SaveNote([FromBody] NoteViewModel note)
        {
            if (note == null)
                return BadRequest("Note cannot be null");

            // check if note exists
            var existingNote = await _noteService.GetNoteAsync(note.BookId, note.ChapterNumber, note.VerseNumber);

            if (existingNote == null)
            { // note does not exist, create it
                await _noteService.AddNoteAsync(note);
                return CreatedAtAction(nameof(GetNote),
                    new { 
                        bookId = note.BookId,
                        chapterNumber = note.ChapterNumber,
                        verseNumber = note.VerseNumber
                    },
                    note);
            } else
            { // note exists, update it
                existingNote.NoteText = note.NoteText;
                await _noteService.UpdateNoteAsync(existingNote);
                return Ok(existingNote);
            }
        }

        [HttpDelete("{bookId}/{chapterNumber}/{verseNumber}")]
        public async Task<IActionResult> DeleteNote(int bookId, int chapterNumber, int verseNumber)
        {
            // fetch the note first
            var existingNote = await _noteService.GetNoteAsync(bookId, chapterNumber, verseNumber);
            if (existingNote == null)
                return NotFound("Note not found");

            // Delete the note
            await _noteService.DeleteNoteAsync(existingNote);
            return Ok(new { message = "Note deleted successfully." });
        }
    }
}
