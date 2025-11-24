using BibleVerseApp.Models.Note;
using BibleVerseApp.Services.Comment;
using MySql.Data.MySqlClient;
using System.Linq.Expressions;

namespace BibleVerseApp.Services.Note
{
    public class NoteDAO : INoteDAO
    {
        private readonly string CONNECTION_STRING;

        public NoteDAO(string SQLConnection)
        {
            CONNECTION_STRING = SQLConnection;
        }
        public async Task<int> AddNoteAsync(NoteModel note)
        {
            string sql = @"INSERT INTO `notes` (book_id, chapter, verse, note) VALUES (@bookId, @chapterNumber, @verseNumber, @noteText);";

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bookId", note.BookId);
                cmd.Parameters.AddWithValue("@chapterNumber", note.ChapterNumber);
                cmd.Parameters.AddWithValue("@verseNumber", note.VerseNumber);
                cmd.Parameters.AddWithValue("@noteText", note.NoteText);
                await cmd.ExecuteNonQueryAsync();
                return (int)cmd.LastInsertedId;
            }
        }

        public async Task DeleteNoteAsync(int noteId)
        {
            string sql = @"DELETE FROM `notes` WHERE id = @id;";

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", noteId);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteNoteAsync(NoteModel note)
        {
            await DeleteNoteAsync(note.Id);
        }

        public async Task<NoteModel> GetNoteByIdAsync(int id)
        {
            string sql = @"SELECT * FROM `notes` WHERE id = @id;";

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new NoteModel
                        {
                            Id = reader.GetInt32("id"),
                            BookId = reader.GetInt32("book_id"),
                            ChapterNumber = reader.GetInt32("chapter"),
                            VerseNumber = reader.GetInt32("verse"),
                            NoteText = reader.IsDBNull(reader.GetOrdinal("note")) ? "" : reader.GetString("note")
                        };
                    }
                    return null;
                }
            }
        }

        public async Task<IEnumerable<NoteModel>> GetAllNotesAsync()
        {
            List<NoteModel> notes = new List<NoteModel>();
            string sql = @"SELECT * FROM `notes` WHERE 1;";
            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        notes.Add(new NoteModel
                        {
                            Id = reader.GetInt32("id"),
                            BookId = reader.GetInt32("book_id"),
                            ChapterNumber = reader.GetInt32("chapter"),
                            VerseNumber = reader.GetInt32("verse"),
                            NoteText = reader.IsDBNull(reader.GetOrdinal("note")) ? "" : reader.GetString("note")
                        });
                    }
                    return notes;
                }
            }
        }

        public async Task<IEnumerable<NoteModel>> GetNotesByBookIdAsync(int bookId)
        {
            List<NoteModel> notes = new List<NoteModel>();
            string sql = "SELECT * FROM `notes` WHERE book_id = @bookId;";
            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        notes.Add(new NoteModel
                        {
                            Id = reader.GetInt32("id"),
                            BookId = reader.GetInt32("book_id"),
                            ChapterNumber = reader.GetInt32("chapter"),
                            VerseNumber = reader.GetInt32("verse"),
                            NoteText = reader.IsDBNull(reader.GetOrdinal("note")) ? "" : reader.GetString("note")
                        });
                    }
                    return notes;
                }
            }
        }

        public async Task UpdateNoteAsync(NoteModel note)
        {
            string sql = @"UPDATE `notes` SET 
                        book_id = @bookId,
                        chapter = @chapterNumber,
                        verse = @verseNumber,
                        note = @noteText 
                        WHERE id = @id";
            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bookId", note.BookId);
                cmd.Parameters.AddWithValue("@chapterNumber", note.ChapterNumber);
                cmd.Parameters.AddWithValue("@verseNumber", note.VerseNumber);
                cmd.Parameters.AddWithValue("@noteText", note.NoteText);
                cmd.Parameters.AddWithValue("@id", note.Id);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<NoteModel> GetNoteAsync(int bookId, int chapterNumber, int verseNumber)
        {
            string sql = @"SELECT * FROM `notes` WHERE `book_id` = @bookId AND `chapter` = @chapterNumber AND `verse` = @verseNumber;";
            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@chapterNumber", chapterNumber);
                cmd.Parameters.AddWithValue("@verseNumber", verseNumber);
                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new NoteModel()
                        {
                            Id = reader.GetInt32("id"),
                            BookId = reader.GetInt32("book_id"),
                            ChapterNumber = reader.GetInt32("chapter"),
                            VerseNumber = reader.GetInt32("verse"),
                            NoteText = reader.IsDBNull(reader.GetOrdinal("note")) ? "" : reader.GetString("note")
                        };
                    }
                    return null;
                }
            }
        }
    }
}
