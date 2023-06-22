using Bible_Search_Application.Models;
using BibleVerseSearch.Models;

namespace BibleVerseSearch.Services
{
    public interface IBibleVerseDataService
    {
        List<BibleVerse> AllVerses();
        List<BibleVerse> SearchVerses(BibleVerse verse);
    }
}
