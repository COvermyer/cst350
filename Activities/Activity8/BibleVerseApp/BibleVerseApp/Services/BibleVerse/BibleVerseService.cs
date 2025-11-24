using BibleVerseApp.Models;
using BibleVerseApp.Models.BibleVerse;
using System.Net;

namespace BibleVerseApp.Services.BibleVerse
{
    public class BibleVerseService : IBibleVerseService
    {
        private readonly IBibleVerseDAO _bibleVerseDAO;
        private readonly IBibleVerseMapper _bibleVerseMapper;

        public BibleVerseService(IBibleVerseDAO bibleVerseDAO, IBibleVerseMapper bibleVerseMapper)
        {
            _bibleVerseDAO = bibleVerseDAO;
            _bibleVerseMapper = bibleVerseMapper;
        }

        public async Task<IEnumerable<BibleVerseViewModel>> GetAllVersesAsync()
        {
            IEnumerable<BibleVerseModel> verseModels = await _bibleVerseDAO.GetAllVersesAsync();
            List<BibleVerseViewModel> verseViewModels = new List<BibleVerseViewModel>();
            foreach (var model in verseModels)
            {
                BibleVerseDTO dto = _bibleVerseMapper.ToDTO(model);
                verseViewModels.Add(_bibleVerseMapper.ToViewModel(dto));
            }
            return verseViewModels;
        }

        public async Task<BibleVerseViewModel> GetVerseAsync(int bookId, int chapterNumber, int verseNumber)
        {
            BibleVerseModel model = await _bibleVerseDAO.GetVerseAsync(bookId, chapterNumber, verseNumber);
            BibleVerseDTO dto = _bibleVerseMapper.ToDTO(model);
            return _bibleVerseMapper.ToViewModel(dto);
        }

        public async Task<BibleVerseViewModel> GetVerseByIdAsync(int verseId)
        {
            BibleVerseModel model = await _bibleVerseDAO.GetVerseByIdAsync(verseId);
            BibleVerseDTO dto = _bibleVerseMapper.ToDTO(model);
            return _bibleVerseMapper.ToViewModel(dto);
        }

        public async Task<IEnumerable<BibleVerseViewModel>> GetVersesByBookAndChapterAsync(int bookId, int chapterNumber)
        {
            IEnumerable<BibleVerseModel> verseModels = await _bibleVerseDAO.GetVersesByBookAndChapterAsync(bookId, chapterNumber);
            List<BibleVerseViewModel> versesViewModels = new List<BibleVerseViewModel>();
            foreach (var model in verseModels)
            {
                BibleVerseDTO dto = _bibleVerseMapper.ToDTO(model);
                versesViewModels.Add(_bibleVerseMapper.ToViewModel(dto));
            }
            return versesViewModels;
        }

        public async Task<IEnumerable<BibleVerseViewModel>> GetVersesByBookAndChapterAsync(int bookId, int chapterNumber, int page, int pageSize)
        {
            IEnumerable<BibleVerseModel> models = await _bibleVerseDAO.GetVersesByBookAndChapterAsync(bookId, chapterNumber, page, pageSize);
            List<BibleVerseViewModel> versesViewModels = new List<BibleVerseViewModel>();
            foreach(var model in models)
            {
                BibleVerseDTO dto = _bibleVerseMapper.ToDTO(model);
                versesViewModels.Add(_bibleVerseMapper.ToViewModel(dto));
            }
            return versesViewModels;
        }

        public async Task<IEnumerable<BibleVerseViewModel>> SearchForVersesAsync(SearchFor searchFor, int page, int pageSize)
        {
            List<BibleVerseViewModel> results = new List<BibleVerseViewModel>();
            IEnumerable<BibleVerseModel> models = await _bibleVerseDAO.SearchForVersesAsync(searchFor, page, pageSize);
            foreach (var model in models)
            {
                BibleVerseDTO dto = _bibleVerseMapper.ToDTO(model);
                results.Add(_bibleVerseMapper.ToViewModel(dto));
            }
            return results;
        }

        public async Task<Dictionary<int, string>> GetAllBooksAsync()
        {
            return await _bibleVerseDAO.GetAllBooksAsync();
        }

        public async Task<int> GetNumberOfChaptersPerBookAsync(int bookId)
        {
            return await _bibleVerseDAO.GetNumberOfChaptersPerBookAsync(bookId);
        }

        public async Task<int> GetNumberOfVersesAsync(int bookId, int chapterNumber)
        {
            return await _bibleVerseDAO.GetNumberOfVersesAsync(bookId, chapterNumber);
        }

		public async Task<int> CountSearchResults(SearchFor searchFor)
		{
			return await _bibleVerseDAO.CountSearchResults(searchFor);
		}
	}
}
