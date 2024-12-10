using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2024.Day7
{
    internal class Bridge_Repair : EventTask
    {
        private List<(long, string)> values;
        public override string GetAnswer1()
        {
            ReadInput();

            long count = 0;

            foreach (var (num, line) in values)
            {
                if (CheckEquations(num, line))
                {
                    count += num;
                }
            }

            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();
            return "";
        }

        private bool CheckEquations(long val, string eq)
        {
            int count = eq.AsSpan().Count(" ");

            if (count > 0)
            {
                string newEq = Calc(eq, '+');
                if (CheckEquations(val, newEq))
                {
                    return true;
                }

                newEq = Calc(eq, '*');
                if (CheckEquations(val, newEq))
                {
                    return true;
                }

                newEq = Calc(eq, '|');
                if (CheckEquations(val, newEq))
                {
                    return true;
                }

                return false;
            }

            if (long.Parse(eq) == val)
            {
                return true;
            }
            return false;
        }

        private string Calc(string text, char sigh)
        {
            int i = text.IndexOf(' ');
            int j = text.IndexOf(' ', text.IndexOf(' ') + 1);

            if (j == -1) j = text.Length;

            long a = long.Parse(text.Substring(0, i));
            long b = long.Parse(text.Substring(i, j - i));

            if (sigh == '+')
            {
                return (a + b) + text.Substring(j);
            }

            if (sigh == '*')
            {
                return (a * b) + text.Substring(j);
            }

            return long.Parse((a.ToString() + b)) + text.Substring(j);

        }


        protected override void ReadInput()
        {
            string path = GetInputPath();
            values = new List<(long, string)>();

            using (var reader = new StreamReader(path))
            {
                while (reader.EndOfStream == false)
                {
                    string line = reader.ReadLine();

                    long val = long.Parse(line.Substring(0, line.IndexOf(':')));

                    string equation = line.Substring(line.IndexOf(':') + 2);

                    values.Add((val, equation));
                }
            }
        }
    }
}
