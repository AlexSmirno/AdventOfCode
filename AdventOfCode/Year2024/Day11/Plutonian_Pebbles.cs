
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

            int blinks = 25;
            long count = 0;
            
            for (int i = 0; i < stones.Count; i++)
            {
                count += Blink(stones[i], 0, blinks);
            }

            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();

            var stones_dic = new Dictionary<long, long>();

            for (int i = 0; i < stones.Count; i++)
            {
                stones_dic.Add(stones[i], 1);
            }

            int blinks = 75;


            for (int i = 0; i < blinks; i++)
            {
                var new_dic = new Dictionary<long, long>();

                foreach (var stone in stones_dic)
                {
                    var list = Blink(stone.Key);

                    for (int j = 0; j < list.Count; j++)
                    {
                        if (new_dic.ContainsKey(list[j]) == false)
                        {
                            new_dic.Add(list[j], 0);
                        }

                        new_dic[list[j]] += stone.Value;
                    }
                }

                stones_dic = new_dic;
            }

            return stones_dic.Sum(s => s.Value).ToString();
        }


        private List<long> Blink(long x)
        {
            if (x == 0)
            {
                x = 1;
                return new List<long>() { x };
            }

            long length = (long)Math.Floor(Math.Log10(x)) + 1;
            if ((length & 1) == 0)
            {
                long num = x;
                length = (long)(Math.Pow(10, length / 2));

                x = num / length;
                long x2 = num % length;
                return new List<long>() { x, x2 };
            }

            x = x * 2024;
            return new List<long>() { x };
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

            stones = new List<long>();

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
