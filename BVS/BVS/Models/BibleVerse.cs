using System.ComponentModel.DataAnnotations;

namespace BibleVerseSearch.Models
{
    //Represents a Bible Verse. 
    public class BibleVerse
    {
        //ID is in relation to the database system of organizing verses
        public int Id { get; set; }
        public string Book { get; set; }
        public int Chapter { get; set; }
        public int Verse { get; set; }
        [Required][StringLength(40, MinimumLength = 1)] 
        public string Text { get; set; }

        public BibleVerse(int id, string book, int chapter, int verse, string text)
        {
            Id = id;
            Book = book;
            Text = text;
            Verse = verse;
            Chapter = chapter;
        }
        public BibleVerse() {
        //empty constructor
        }

        public override string ToString()
        {
            return $"Book: {Book}, Chapter: {Chapter}, Text: {Text}";
        }
    }
}
