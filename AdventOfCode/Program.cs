using AdventOfCode.Year2024.Day1;
using AdventOfCode.Year2024.Day2;
using AdventOfCode.Year2024.Day3;
using AdventOfCode.Year2024.Day4;
using AdventOfCode.Year2024.Day5;
using AdventOfCode.Year2024.Day6;
using AdventOfCode.Year2024.Day7;
using AdventOfCode.Year2024.Day8;
using AdventOfCode.Year2024.Day9;
using AdventOfCode.Year2024.Day10;
using AdventOfCode.Year2024.Day11;
using AdventOfCode.Year2024.Day12;
using AdventOfCode.Year2024.Day13;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EventTask task = new Claw_Contraption();

            Console.WriteLine(task.GetAnswer1());
            Console.WriteLine(task.GetAnswer2());

        }
    }
}
