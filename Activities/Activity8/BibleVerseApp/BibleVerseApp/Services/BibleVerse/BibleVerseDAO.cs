using BibleVerseApp.Models;
using BibleVerseApp.Models.BibleVerse;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System.Data;

namespace BibleVerseApp.Services.BibleVerse
{
    public class BibleVerseDAO : IBibleVerseDAO
    {
        private readonly string CONNECTION_STRING;

        public BibleVerseDAO(string SQLConnection)
        {
            CONNECTION_STRING = SQLConnection;
        }

        public async Task<IEnumerable<BibleVerseModel>> GetAllVersesAsync()
        {
            List<BibleVerseModel> verses = new List<BibleVerseModel>();
            string query = @"SELECT
                v.book_id AS BookId,
                v.id AS VerseId,
                b.name AS BookName,
                v.chapter AS ChapterNumber,
                v.verse AS VerseNumber,
                v.text AS VerseText 
                FROM KJV_verses v 
                JOIN KJV_books b 
                ON v.book_id = b.id";

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        verses.Add(new BibleVerseModel
                        {
                            BookId = reader.GetInt32("BookId"),
                            VerseId = reader.GetInt32("VerseId"),
                            BookName = reader.GetString("BookName"),
                            ChapterNumber = reader.GetInt32("ChapterNumber"),
                            VerseNumber = reader.GetInt32("VerseNumber"),
                            VerseText = reader.GetString("VerseText")
                        });

                    }
                    return verses;
                }
            }
        }

        public async Task<BibleVerseModel> GetVerseAsync(int bookId, int chapterNumber, int verseNumber)
        {
            string query = @"SELECT
                v.book_id AS BookId,
                v.id AS VerseId,
                b.name AS BookName,
                v.chapter AS ChapterNumber,
                v.verse AS VerseNumber,
                v.text AS VerseText 
                FROM KJV_verses v 
                JOIN KJV_books b 
                ON v.book_id = b.id 
                WHERE v.book_id = @bookId AND v.chapter = @chapterNumber AND v.verse = @verseNumber";

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@chapterNumber", chapterNumber);
                cmd.Parameters.AddWithValue("@verseNumber", verseNumber);
                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new BibleVerseModel
                        {
                            BookId = reader.GetInt32("BookId"),
                            VerseId = reader.GetInt32("VerseId"),
                            BookName = reader.GetString("BookName"),
                            ChapterNumber = reader.GetInt32("ChapterNumber"),
                            VerseNumber = reader.GetInt32("VerseNumber"),
                            VerseText = reader.GetString("VerseText")
                        };
                    }
                    return null;
                }
            }
        }

        public async Task<IEnumerable<BibleVerseModel>> GetVersesByBookAndChapterAsync(int bookId, int chapterNumber)
        {
            List<BibleVerseModel> verses = new List<BibleVerseModel>();
            string query = @"SELECT
                v.book_id AS BookId,
                v.id AS VerseId,
                b.name AS BookName,
                v.chapter AS ChapterNumber,
                v.verse AS VerseNumber,
                v.text AS VerseText 
                FROM KJV_verses v 
                JOIN KJV_books b 
                ON v.book_id = b.id 
                WHERE v.book_id = @bookId AND v.chapter = @chapterNumber";

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@chapterNumber", chapterNumber);
                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        verses.Add(new BibleVerseModel
                        {
                            BookId = reader.GetInt32("BookId"),
                            VerseId = reader.GetInt32("VerseId"),
                            BookName = reader.GetString("BookName"),
                            ChapterNumber = reader.GetInt32("ChapterNumber"),
                            VerseNumber = reader.GetInt32("VerseNumber"),
                            VerseText = reader.GetString("VerseText")
                        });
                    }
                    return verses;
                }
            }
        }

        public async Task<BibleVerseModel> GetVerseByIdAsync(int verseId)
        {
            string query = @"SELECT
                v.book_id AS BookId,
                v.id AS VerseId,
                b.name AS BookName,
                v.chapter AS ChapterNumber,
                v.verse AS VerseNumber,
                v.text AS VerseText 
                FROM KJV_verses v 
                JOIN KJV_books b 
                ON v.book_id = b.id 
                WHERE v.id = @verseId";

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@verseId", verseId);
                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new BibleVerseModel
                        {
                            BookId = reader.GetInt32("BookId"),
                            VerseId = reader.GetInt32("VerseId"),
                            BookName = reader.GetString("BookName"),
                            ChapterNumber = reader.GetInt32("ChapterNumber"),
                            VerseNumber = reader.GetInt32("VerseNumber"),
                            VerseText = reader.GetString("VerseText")
                        };
                    }
                    return null;
                }
            }
        }

        public async Task<Dictionary<int, string>> GetAllBooksAsync()
        {
            Dictionary<int, string> books = new Dictionary<int, string>();
            string sql = @"SELECT * FROM `kjv_books` WHERE 1;";
            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        books.Add(reader.GetInt32("id"), reader.GetString("name"));
                    }
                    return books;
                }
            }
        }

        public async Task<int> GetNumberOfChaptersPerBookAsync(int bookId)
        {
            string sql = @"SELECT COUNT(DISTINCT `chapter`) FROM `kjv_verses` WHERE `book_id` = @bookId;";
            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }

        public async Task<int> GetNumberOfVersesAsync(int bookId, int chapterNumber)
        {
            string sql = @"SELECT COUNT(DISTINCT `verse`) FROM `kjv_verses` WHERE `book_id` = @bookId AND `chapter` = @chapterNumber; ";
            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@chapterNumber", chapterNumber);
                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }

		public async Task<IEnumerable<BibleVerseModel>> SearchForVersesAsync(SearchFor searchFor, int page, int pageSize)
		{
            List<BibleVerseModel> results = new List<BibleVerseModel>();
            string sql = @"
                SELECT 
                    v.book_id AS BookId,
                    v.id AS VerseId,
                    b.name AS BookName,
                    v.chapter AS ChapterNumber,
                    v.verse AS VerseNumber,
                    v.text AS VerseText
                FROM KJV_verses v
                JOIN KJV_books b ON v.book_id = b.id
                WHERE LOWER(v.text) LIKE LOWER(@searchTerm)";

            // appliesd filters if only one is checked
            if (searchFor.InOldTestament && !searchFor.InNewTestament)
                sql += " AND v.book_id <= 39 ";
            else if (searchFor.InNewTestament && !searchFor.InOldTestament)
                sql += " AND v.book_id >= 40 ";
            // if none/both are checked, no filter applied (searched both)
            sql += " LIMIT @pageSize OFFSET @offset";

            int offset = (page - 1) * pageSize;

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@searchTerm", $"%{searchFor.SearchTerm}%");
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@offset", offset);

                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        results.Add(new BibleVerseModel
                        {
							BookId = reader.GetInt32("BookId"),
							VerseId = reader.GetInt32("VerseId"),
							BookName = reader.GetString("BookName"),
							ChapterNumber = reader.GetInt32("ChapterNumber"),
							VerseNumber = reader.GetInt32("VerseNumber"),
							VerseText = reader.GetString("VerseText")
						});
                    }
                    return results;
                }
            }
		}

		public async Task<int> CountSearchResults(SearchFor searchFor)
		{
            string sql = @"SELECT COUNT(*) FROM `kjv_verses` v WHERE LOWER(v.text) LIKE LOWER(@searchTerm)";
			// appliesd filters if only one is checked
			if (searchFor.InOldTestament && !searchFor.InNewTestament)
				sql += " AND v.book_id <= 39 ";
			else if (searchFor.InNewTestament && !searchFor.InOldTestament)
				sql += " AND v.book_id >= 40 ";
			// if none/both are checked, no filter applied (searched both)

            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@searchTerm", $"%{searchFor.SearchTerm}%");
                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
		}

        public async Task<IEnumerable<BibleVerseModel>> GetVersesByBookAndChapterAsync(int bookId, int chapterNumber, int page, int pageSize)
        {
            List<BibleVerseModel> results = new List<BibleVerseModel>();
            string sql = @"
                SELECT 
                    v.book_id AS BookId,
                    v.id AS VerseId,
                    b.name AS BookName,
                    v.chapter AS ChapterNumber,
                    v.verse AS VerseNumber,
                    v.text AS VerseText
                FROM KJV_verses v
                JOIN KJV_books b ON v.book_id = b.id 
                WHERE v.book_id = @bookId AND v.chapter = @chapterNumber 
                LIMIT @pageSize OFFSET @offset";
            int offset = (page - 1) * pageSize;
            using (MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                await conn.OpenAsync();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.Parameters.AddWithValue("@chapterNumber", chapterNumber);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@offset", offset);
                using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        results.Add(new BibleVerseModel
                        {
                            BookId = reader.GetInt32("BookId"),
                            VerseId = reader.GetInt32("VerseId"),
                            BookName = reader.GetString("BookName"),
                            ChapterNumber = reader.GetInt32("ChapterNumber"),
                            VerseNumber = reader.GetInt32("VerseNumber"),
                            VerseText = reader.GetString("VerseText")
                        });
                    }
                    return results;
                }
            }
        }
    }
}
