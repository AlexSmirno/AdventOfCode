using AdventOfCode.Year2024.Day1;
using AdventOfCode.Year2024.Day2;
using AdventOfCode.Year2024.Day3;
using AdventOfCode.Year2024.Day4;
using AdventOfCode.Year2024.Day5;
using AdventOfCode.Year2024.Day6;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EventTask task = new Guard_Gallivant();

            Console.WriteLine(task.GetAnswer1());
            Console.WriteLine(task.GetAnswer2());



        }
    }
}
