using AdventOfCode.Year2024.Day1;
using AdventOfCode.Year2024.Day2;
using AdventOfCode.Year2024.Day3;
using AdventOfCode.Year2024.Day4;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EventTask task = new Ceres_Search();

            Console.WriteLine(task.GetAnswer1());
            Console.WriteLine(task.GetAnswer2());



        }
    }
}
