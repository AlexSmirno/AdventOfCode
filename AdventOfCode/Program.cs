using AdventOfCode.Year2024.Day1;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EventTask task = new Historian_Hysteria();

            Console.WriteLine(task.GetAnswer1());
            Console.WriteLine(task.GetAnswer2());
        }
    }
}
