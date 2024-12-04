using AdventOfCode.Year2024.Day1;
using AdventOfCode.Year2024.Day2;
using System.Linq;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Red_Nosed_Reports task = new Red_Nosed_Reports();

            //Console.WriteLine(task.GetAnswer1());
            Console.WriteLine(task.GetAnswer2());

            Console.WriteLine();


            Test t = new Test();

            Console.WriteLine(t.Part1());

            var res = t.Part2().Item1;
            var list = t.Part2().Item2;
            Console.WriteLine(res);

            var list3 = list.Except(task.rights).ToList();

            for (int i = 0; i < list3.Count; i++)
            {
                Console.WriteLine(list3[i]);
            }
        }
    }
}
