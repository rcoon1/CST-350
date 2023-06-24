using System;
using System.Linq;

namespace LINQ_Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scores = new[] { 50, 66, 90, 81, 77, 45, 0, 100, 99, 72, 87, 85, 81, 80, 77, 74, 95, 97 };

            foreach (var individualScore in scores)
            {
                Console.WriteLine("One of the scores was {0}", individualScore);
            }
            Console.ReadLine();

            var theBestStudents =
            from individualScore in scores
            where individualScore > 90
            select individualScore;

            foreach (var individualScore in theBestStudents)
            {
                Console.WriteLine("One of the BEST scores was {0}", individualScore);
            }
            Console.ReadLine();

            var sortedScores =
            from individualScores in scores
            orderby individualScores
            select individualScores;

            foreach (var individualScores in sortedScores)
            {
                Console.WriteLine("One of the scores was {0}", individualScores);
            }
            Console.ReadLine();

            var bStudents =
            from individualScores in scores
            orderby individualScores ascending
            where individualScores > 80 && individualScores < 90
            select individualScores;
            Console.WriteLine("#### Challenge #####");
            foreach (var individualScores in bStudents)
            {
                Console.WriteLine("One of the B scores was {0}", individualScores);
            }
            Console.ReadLine();
        }
        
    }
}
