using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2024.Day18
{
    internal class RAM_Run : EventTask
    {
        private int size = 71;
        private int takeOnly = 1024;
        private (int x, int y) person;

        private List<(int x, int y)> brokens;

        private List<List<char>> map;
        private List<List<int>> used;

        private bool isEnd = false;

        private List<(int x, int y)> d = new List<(int x, int y)>()
        {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1),
        };

        public override string GetAnswer1()
        {
            ReadInput();
            DrawMap();

            Step(person.x, person.y, 0);
            StepBackward(size - 1, size -1 );

            DrawMap();

            return used[size-1][size-1].ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();
            DrawMap();

            isEnd = false;

            while (StepToPath(person.x, person.y, 0))
            {
                isEnd = false;

                StepBackward(size - 1, size - 1);

                isEnd = false;
                ClearMap();

                takeOnly++;
                map[brokens[takeOnly].y][brokens[takeOnly].x] = '#';
            }

            DrawMap();

            return brokens[takeOnly].x + "," + brokens[takeOnly].y;
        }

        // Find smallest way
        private void Step(int x, int y, int length)
        {
            if (x < 0 || x >= size || y < 0 || y >= size || map[y][x] == '#') return;


            if (used[y][x] > length) used[y][x] = length;
            else return;


            for (int i = 0; i < d.Count; i++)
            {
                Step(x + d[i].x, y + d[i].y, length + 1);
            }

            return;
        }

        private void StepBackward(int x, int y)
        {
            if (x < 0 || x >= size || y < 0 || y >= size || map[y][x] == '#') return;

            if (x == 0 && y == 0) isEnd = true;

            if (isEnd) return;

            for (int i = 0; i < d.Count; i++)
            {
                if (!(x + d[i].x < 0 || x + d[i].x >= size 
                    || y + d[i].y < 0 || y + d[i].y >= size) 
                    && used[y][x] > used[y + d[i].y][x + d[i].x]
                    && map[y + d[i].y][x + d[i].x] != 'O')
                {
                    map[y + d[i].y][x + d[i].x] = 'O';
                    StepBackward(x + d[i].x, y + d[i].y);
                }
            }

            return;
        }


        // Find just one way
        private bool StepToPath(int x, int y, int length)
        {
            if (x < 0 || x >= size || y < 0 || y >= size || map[y][x] == '#') return false;

            if (used[y][x] > length) 
                used[y][x] = length;
            else return false;

            if (x == size - 1 && y == size - 1) isEnd = true;
            if (isEnd) return true;

            bool res = false;
            for (int i = 0; i < d.Count; i++)
            {
                res = res | StepToPath(x + d[i].x, y + d[i].y, length + 1);
            }

            return res;
        }

        private void DrawMap()
        {
            Console.Clear();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (map[i][j] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(map[i][j]);
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    Console.Write(map[i][j]);
                }

                Console.WriteLine();
            }
        }

        private void ClearMap()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (map[i][j] == 'O')
                    {
                        map[i][j] = '.';
                    }

                    used[i][j] = int.MaxValue;
                }
            }
        }

        protected override void ReadInput()
        {
            string path = GetInputPath();

            brokens = new List<(int x, int y)>();
            map = new List<List<char>>();
            used = new List<List<int>>();

            using (StreamReader reader = new StreamReader(path))
            {
                while (reader.EndOfStream == false)
                {
                    string line = reader.ReadLine();

                    var nums = line.Split(",");

                    brokens.Add((int.Parse(nums[0]), int.Parse(nums[1])));
                }
            }

            for (int i = 0; i < size; i++)
            {
                map.Add(new List<char>());
                used.Add(new List<int>());
                for (int j = 0; j < size; j++)
                {
                    bool isBroken = false;
                    for (int k = 0; k < takeOnly; k++)
                    {
                        if (brokens[k].x == j && brokens[k].y == i)
                        {
                            isBroken = true;
                        }
                    }
                    if (isBroken)
                        map[i].Add('#');
                    else
                        map[i].Add('.');

                    used[i].Add(int.MaxValue);
                }
            }
        }

    }
}
