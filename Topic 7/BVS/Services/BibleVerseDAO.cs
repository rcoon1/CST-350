using Bible_Search_Application.Models;
using BibleVerseSearch.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BibleVerseSearch.Services
{
    //Controls calls to Bible Database, implements BibleVerseDataService Interface
    public class BibleVerseDAO : IBibleVerseDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = Bible; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //Returns all verses in the database
        public List<BibleVerse> AllVerses()
        {
            List<BibleVerse> verses = new List<BibleVerse>();
            //Using temp string to limit search to first 4 books of bible
            //string sqlStatement = "SELECT * FROM dbo.t_asv";
            string sqlStatement = "SELECT * FROM dbo.t_asv where id Between 01001001 and 05000000";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        verses.Add(
                            new BibleVerse(
                                (int)reader[0],
                                ((BookName)(int)reader[1]).ToString(),
                                (int)reader[2],
                                (int)reader[3],
                                (string)reader[4]));
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return verses;
        }
        //Enum of Bible book names to allow quick access and reference to all Bible books
        public enum BookName
        {
            Any = 0,
            Genesis = 1,
            Exodus,
            Leviticus,
            Numbers,
            Deuteronomy,
            Joshua,
            Judges,
            Ruth,
            First_Samuel,
            Second_Samuel,
            First_Kings,
            Second_Kings,
            First_Chronicles,
            Second_Chronicles,
            Ezra,
            Nehemiah,
            Esther,
            Job,
            Psalms,
            Proverbs,
            Ecclesiastes,
            Song_Of_Solomon,
            Isaiah,
            Jeremiah,
            Lamentations,
            Ezekiel,
            Daniel,
            Hosea,
            Joel,
            Amos,
            Obadiah,
            Jonah,
            Micah,
            Nahum,
            Habakkuk,
            Zephaniah,
            Haggai,
            Zechariah,
            Malachi,
            Matthew,
            Mark,
            Luke,
            John,
            Acts,
            Romans,
            First_Corinthians,
            Second_Corinthians,
            Galatians,
            Ephesians,
            Philippians,
            Colossians,
            First_Thessalonians,
            Second_Thessalonians,
            First_Timothy,
            Second_Timothy,
            Titus,
            Philemon,
            Hebrews,
            James,
            First_Peter,
            Second_Peter,
            First_John,
            Second_John,
            Third_John,
            Jude,
            Revelation
        }
        //Unused (For future use if wanted to limit scope of user choice of chapter based on number of chapters in each book
        public enum BookChapters
        {
            Genesis = 50,
            Exodus = 40,
            Leviticus = 27,
            Numbers = 36,
            Deuteronomy = 34,
            Joshua = 24,
            Judges = 21,
            Ruth = 4,
            FirstSamuel = 31,
            SecondSamuel = 24,
            FirstKings = 22,
            SecondKings = 25,
            FirstChronicles = 29,
            SecondChronicles = 36,
            Ezra = 10,
            Nehemiah = 13,
            Esther = 10,
            Job = 42,
            Psalms = 150,
            Proverbs = 31,
            Ecclesiastes = 12,
            SongOfSolomon = 8,
            Isaiah = 66,
            Jeremiah = 52,
            Lamentations = 5,
            Ezekiel = 48,
            Daniel = 12,
            Hosea = 14,
            Joel = 3,
            Amos = 9,
            Obadiah = 1,
            Jonah = 4,
            Micah = 7,
            Nahum = 3,
            Habakkuk = 3,
            Zephaniah = 3,
            Haggai = 2,
            Zechariah = 14,
            Malachi = 4,
            Matthew = 28,
            Mark = 16,
            Luke = 24,
            John = 21,
            Acts = 28,
            Romans = 16,
            FirstCorinthians = 16,
            SecondCorinthians = 13,
            Galatians = 6,
            Ephesians = 6,
            Philippians = 4,
            Colossians = 4,
            FirstThessalonians = 5,
            SecondThessalonians = 3,
            FirstTimothy = 6,
            SecondTimothy = 4,
            Titus = 3,
            Philemon = 1,
            Hebrews = 13,
            James = 5,
            FirstPeter = 5,
            SecondPeter = 3,
            FirstJohn = 5,
            SecondJohn = 1,
            ThirdJohn = 1,
            Jude = 1,
            Revelation = 22
        }

        //Searches Database based off the given BibleVerse object
        public List<BibleVerse> SearchVerses(BibleVerse search)
        {
            string sqlStatement;
            BookName names;
            //Check if searching in book
            if (search.Book != "Any")
            {
                int bookNum = 0;
                //Check if searching within chapter
                if (Enum.TryParse(search.Book, out names))
                {
                    bookNum = (int)names;
                }

                //Book number was corectly found in Enum
                if (bookNum > 0)
                {
                    //Check if they want from a specific chapter
                    if (search.Chapter > 0)
                    {
                        //If yes, search by chapter, book, and text
                        sqlStatement = "SELECT * FROM dbo.t_asv where b = " + bookNum + " and c = " + search.Chapter + " and t LIKE '%" + search.Text + "%'";
                    }
                    else
                    {
                        //Otherwise search by Book and text
                        sqlStatement = "SELECT * FROM dbo.t_asv where b = " + bookNum + " and t LIKE '%" + search.Text + "%'";
                    }

                }
                //Search only by text
                else
                {
                    sqlStatement = "SELECT * FROM dbo.t_asv WHERE t LIKE '%" + search.Text + "%'";
                }
            }
            else
            {
                sqlStatement = "SELECT * FROM dbo.t_asv WHERE t LIKE '%" + search.Text + "%'";
            }
            List<BibleVerse> verses = new List<BibleVerse>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        verses.Add(
                            new BibleVerse(
                                (int)reader[0],
                                ((BookName)(int)reader[1]).ToString(),
                                (int)reader[2],
                                (int)reader[3],
                                (string)reader[4]));
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return verses;
        }

        List<BibleVerse> IBibleVerseDataService.AllVerses()
        {
            throw new NotImplementedException();
        }
    }
}
