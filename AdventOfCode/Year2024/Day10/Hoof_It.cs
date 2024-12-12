using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2024.Day10
{
    internal class Hoof_It : EventTask
    {
        List<List<char>> map;

        List<List<int>> used;

        public override string GetAnswer1()
        {
            ReadInput();

            used = new List<List<int>>();

            List<int> res = new List<int>();

            for (int i = 0; i < map.Count; i++)
            {
                used.Add(new List<int>());
                for (int j = 0; j < map[i].Count; j++)
                {
                    used[i].Add(0);
                }
            }

            int count = 0;

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == '0')
                    {
                        res.Add(FindPath(i, j));
                        ClearUsedMap();
                    }
                }
            }

            count = res.Sum();
            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();

            used = new List<List<int>>();

            List<int> res = new List<int>();

            for (int i = 0; i < map.Count; i++)
            {
                used.Add(new List<int>());
                for (int j = 0; j < map[i].Count; j++)
                {
                    used[i].Add(0);
                }
            }

            int count = 0;

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == '0')
                    {
                        res.Add(FindPath2(i, j));
                        ClearUsedMap();
                    }
                }
            }


            count = res.Sum();
            return count.ToString();
        }


        private int FindPath(int y, int x)
        {
            int count = 0;

            used[y][x] = 1;

            if (map[y][x] == '9') return 1;
            

            if (y + 1 < map.Count && used[y + 1][x] > 0 && map[y + 1][x] - map[y][x] == 1)
            {
                count += FindPath(y + 1, x);
            }

            if (x + 1 < map.Count && used[y][x + 1]>0 && map[y][x + 1] - map[y][x] == 1)
            {
                count += FindPath(y, x + 1);
            }

            if (y - 1 >= 0 && used[y - 1][x] > 0 && map[y - 1][x] - map[y][x] == 1)
            {
                count += FindPath(y - 1, x);
            }

            if (x - 1 >= 0 && used[y][x - 1] > 0 && map[y][x - 1] - map[y][x] == 1)
            {
                count += FindPath(y, x - 1);
            }

            return count;
        }


        private int FindPath2(int y, int x)
        {
            int count = 0;

            //DrawMap(y, x);

            if (used[y][x] > 0) return used[y][x];

            count += used[y][x];

            if (map[y][x] == '9') return 1;


            if (y + 1 < map.Count && map[y + 1][x] - map[y][x] == 1)
            {
                count += FindPath2(y + 1, x);
                //DrawMap(y, x);
            }

            if (x + 1 < map.Count && map[y][x + 1] - map[y][x] == 1)
            {
                count += FindPath2(y, x + 1);
                //DrawMap(y, x);
            }

            if (y - 1 >= 0 && map[y - 1][x] - map[y][x] == 1)
            {
                count += FindPath2(y - 1, x);
                //DrawMap(y, x);
            }

            if (x - 1 >= 0 && map[y][x - 1] - map[y][x] == 1)
            {
                count += FindPath2(y, x - 1);
                //DrawMap(y, x);
            }

            return count;
        }

        private void DrawMap(int y, int x)
        {
            Thread.Sleep(200);
            Console.Clear();
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (i == y && j == x)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.Write(map[i][j] + " ");

                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("       ");

                for (int j = 0; j < map[i].Count; j++)
                {
                    if (used[i][j] > 0)
                    {
                        if (map[i][j] == '9')
                        {
                            Console.Write("9 ");
                        }
                        else
                        {
                            Console.Write(used[i][j] + " ");
                        }
                    }
                    else
                    {
                        Console.Write(". ");
                    }

                }
                Console.WriteLine();
            }
        }

        private void ClearUsedMap()
        {
            for(int i = 0;i < used.Count;i++)
            {
                for (int j = 0; j < used.Count; j++)
                {
                    used[i][j] = 0;
                }
            }
        }

        protected override void ReadInput()
        {
            string path = GetInputPath();

            map = new List<List<char>>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream == false)
                {
                    string line = sr.ReadLine();

                    map.Add(line.ToCharArray().ToList());
                }
            }
        }
    }
}
