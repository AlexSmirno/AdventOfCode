using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2024.Day13
{
    internal class Claw_Contraption : EventTask
    {
        private List<Machine> machines;
        public override string GetAnswer1()
        {
            ReadInput();
            long count = 0;

            foreach (Machine machine in machines)
            {
                long a, b;
                (a,b) = machine.SolveMachine();

                long sum = a*3 + b*1;
                count += sum;
            }


            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();
            long count = 0;



            return count.ToString();
        }

        protected override void ReadInput()
        {
            string path = GetInputPath();

            machines = new List<Machine>();

            using (StreamReader reader = new StreamReader(path))
            {
                while (reader.EndOfStream == false)
                {
                    string a = reader.ReadLine();
                    if (a == "")
                    {
                        a = reader.ReadLine();
                    }
                    string b = reader.ReadLine();
                    string p = reader.ReadLine();

                    var m = new Machine(a, b, p);

                    machines.Add(m);
                }
            }
        }


        private class Machine
        {
            public (long, long) ButtonA;
            public (long, long) ButtonB;
            public (long, long) Prize;

            public Machine() { }

            public Machine(string a, string b, string prize)
            {
                ButtonA = ParseString(a, '+');
                ButtonB = ParseString(b,'+');
                Prize = ParseString(prize, '=');
                Prize.Item1 += 10000000000000;
                Prize.Item2 += 10000000000000;
            }

            
            private (long, long) ParseString(string str, char sigh)
            {
                int a = str.IndexOf("X"+sigh) + 2;
                string sx = str.Substring(a, str.IndexOf(",") - a);

                int b = str.IndexOf("Y"+sigh) + 2;
                string sy = str.Substring(b, str.Length - b);

                long x = long.Parse(sx);
                long y = long.Parse(sy);

                return (x, y);
            }

            //Система из двух линейных уравнений p = a*x+b*y - всегда будет иметь 1 решение (или бесконечно много решений)
            public (long, long) SolveMachine()
            {
                long a, b;

                a = (ButtonB.Item1*Prize.Item2 - ButtonB.Item2*Prize.Item1) / (ButtonA.Item2*ButtonB.Item1 - ButtonB.Item2*ButtonA.Item1);

                b = (Prize.Item1 - a*ButtonA.Item1)/ButtonB.Item1;

                long p1 = ButtonA.Item1*a + ButtonB.Item1*b;
                long p2 = ButtonA.Item2*a + ButtonB.Item2*b;

                if (p1 != Prize.Item1 || p2 != Prize.Item2)
                {
                    return (0, 0);
                }
                return (a, b);
            }

            private bool Check(long a, long b)
            {
                return (ButtonA.Item1*a + ButtonB.Item1*b == Prize.Item1) && 
                       (ButtonA.Item2*a + ButtonB.Item2*b == Prize.Item2);
            }
        }
    }

}
