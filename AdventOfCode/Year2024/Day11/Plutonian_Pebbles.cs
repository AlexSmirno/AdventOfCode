
using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2024.Day11
{
    internal class Plutonian_Pebbles : EventTask
    {
        private List<long> stones;

        private long c = 0;
        public override string GetAnswer1()
        {
            ReadInput();

            long count = 0;

            for (int i = 0; i < 25; i++)
            {
                Blink(stones);
            }

            count = stones.Count;

            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();

            int winks = 50;

            var timer2 = System.Diagnostics.Stopwatch.StartNew();
            long count2 = 0;
            for (int i = 0; i < stones.Count; i++)
            {
                count2 += Blink(stones[i], 0, winks);
            }
            timer2.Stop();
            Console.WriteLine(timer2.ElapsedTicks / 10000f);


            /*
            var timer = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < winks; i++)
            {
                Blink(stones);
            }
            long count1 = stones.Count;
            timer.Stop();
            Console.WriteLine(timer.ElapsedTicks / 10000f);
            */

            return count2.ToString();
        }


        private void Blink(List<long> list)
        {
            int list_begin_length = list.Count;
            for (int i = 0; i < list_begin_length; i++)
            {
                if (list[i] == 0)
                {
                    list[i] = 1;
                    continue;
                }

                long length = (long)Math.Floor(Math.Log10(list[i])) + 1;
                if ((length & 1) == 0)
                {
                    long num = list[i];
                    length = (long)(Math.Pow(10, length / 2));

                    list[i] = num / length;
                    list.Add(num % length);

                    continue;
                }


                list[i] = list[i] * 2024;
            }
        }


        private long Blink(long x, int i, int max)
        {
            if (i == max) return 1;

            if (x== 0) return Blink(1, i + 1, max);

            long length = (long)Math.Floor(Math.Log10(x)) + 1;
            if ((length & 1) == 0)
            {
                length = (long)(Math.Pow(10, length / 2));

                long count = 0;
                count += Blink(x/length, i+1, max);
                count += Blink(x%length, i + 1, max);

                return count;
            }

            return Blink(x * 2024, i + 1, max);
        }

        private void DrawStones()
        {
            stones.ForEach(s => Console.Write(s + " "));
            Console.WriteLine();
        }


        protected override void ReadInput()
        {
            string path = GetInputPath();

            stones = new List<long>(int.MaxValue-56);

            using (StreamReader sr = new StreamReader(path))
            {
                var list = sr.ReadLine().Split(' ').ToList();

                foreach (var item in list)
                {
                    stones.Add(long.Parse(item)); 
                }
            }

        }
    }
}
