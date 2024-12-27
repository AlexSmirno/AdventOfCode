using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2024.Day17
{
    internal class Chronospatial_Computer : EventTask
    {
        private long A, B, C;
        private List<long> program;
        private List<long> output;

        private int pointer = 0;

        public override string GetAnswer1()
        {
            ReadInput();

            Compute();

            return string.Join(',', output);
        }

        public override string GetAnswer2()
        {
            ReadInput();
            long a = 0, b = B, c = C;

            pointer = 0;

            int outputPointer = program.Count;
            var neededOutput = new List<long>();
            neededOutput.Insert(0, program[--outputPointer]);
            neededOutput.Insert(0, program[--outputPointer]);

            while (CompareLists(output, program) == false)
            {
                while (CompareLists(neededOutput, output) == false)
                {
                    output = new List<long>();
                    Compute();

                    a++;
                    A = a;
                    B = b;
                    C = c;
                    pointer = 0;
                }
                
                output.ForEach(x => Console.Write(x + " "));
                Console.Write("    " + (A - 1) + "    ");
                Console.Write("    " + Convert.ToString(A - 1, 2));
                Console.WriteLine();

                if (outputPointer <= 0 && CompareLists(output, program))
                {
                    break;
                }
                

                neededOutput.Insert(0, program[--outputPointer]);
                neededOutput.Insert(0, program[--outputPointer]);

                a = (A-1)*64;
                A = a;
                B = b;
                C = c;
            }


            return (A - 1).ToString();
        }


        private bool CompareLists(List<long> a, List<long> b)
        {
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }


        private void Compute()
        {
            while (pointer < program.Count)
            {
                var o = program[pointer];
                var n = program[pointer + 1];
                Exec(o, n);
                pointer += 2;
            }
        }

        private void Exec(long o, long n)
        {

            switch (o)
            {
                case 0:
                    adv(n);
                    break;
                case 1:
                    bxl(n);
                    break;
                case 2:
                    bst(n);
                    break;
                case 3:
                    jnz(n);
                    break;
                case 4:
                    bxc(n);
                    break;
                case 5:
                    _out(n);
                    break;
                case 6:
                    bdv(n);
                    break;
                case 7:
                    cdv(n);
                    break;
                default:
                    break;
            }
        }

        private void adv(long n)
        {
            n = GetCombined(n);
            A = A >> (int)n;
        }
        private void bxl(long n)
        {
            B = B ^ n;
        }
        private void bst(long n)
        {
            n = GetCombined(n);
            B = n % 8;
        }
        private void jnz(long n)
        {
            if (A == 0) return;

            pointer = (int)n;
            pointer -= 2; // Для анулирования += 2 в конце операции
        }
        private void bxc(long n)
        {
            B = B ^ C;
        }
        private void _out(long n)
        {
            n = GetCombined(n);
            long o = n % 8;
            output.Add(o);
        }
        private void bdv(long n)
        {
            n = GetCombined(n);
            B = A >> (int)n;
        }
        private void cdv(long n)
        {
            n = GetCombined(n);
            C = A >> (int)n;
        }

        private long GetCombined(long n)
        {
            if (n == 4) return A;

            if (n == 5) return B;

            if (n == 6) return C;

            return n;
        }

        protected override void ReadInput()
        {
            string path = GetInputPath();

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();
                A = long.Parse(line.Substring(line.IndexOf("A: ") + 3).Trim());

                line = reader.ReadLine();
                B = long.Parse(line.Substring(line.IndexOf("B: ") + 3).Trim());

                line = reader.ReadLine();
                C = long.Parse(line.Substring(line.IndexOf("C: ") + 3).Trim());

                line = reader.ReadLine();

                line = reader.ReadLine();
                var list = line.Substring(line.IndexOf(": ") + 2).Trim().Split(',');

                program = new List<long>();
                output = new List<long>();
                for (int i = 0; i < list.Length; i++)
                {
                    program.Add(long.Parse(list[i]));
                }

            }

        }
    }
}
