using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2024.Day15
{
    internal class Warehouse_Woes : EventTask
    {
        private List<(int x, int y)> directions = new List<(int x, int y)>()
        {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1),
        };

        private List<List<char>> map;
        private Queue<int> robotSteps;

        private (int x, int y) robot;

        public override string GetAnswer1()
        {
            ReadInput();

            int count = 0;

            DrawMap();

            while (robotSteps.Any())
            {
                var d = directions[robotSteps.Dequeue()];
                if (MakeStep(d, robot))
                {
                    robot.x += d.x;
                    robot.y += d.y;
                }
            }

            DrawMap();

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == 'O')
                    {
                        count += i*100 + j;
                    }
                }
            }

            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();

            int count = 0;

            ExpandMap();

            DrawMap();

            while (robotSteps.Any())
            {
                var d = directions[robotSteps.Dequeue()];
                if (MakeStep(d, robot))
                {
                    robot.x += d.x;
                    robot.y += d.y;
                }
                DrawMap();
            }

            DrawMap();

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == '[')
                    {
                        count += i * 100 + j;
                    }
                }
            }

            return count.ToString();
        }

        
        private bool MakeStep((int x, int y) d, (int x, int y) loc)
        {
            if (map[loc.y + d.y][loc.x + d.x] == '#')
            {
                return false;
            }

            bool isPossible = true;
            if (map[loc.y + d.y][loc.x + d.x] == 'O')
            {
                isPossible = MakeStep(d, (loc.x + d.x, loc.y + d.y));
            }

            if (map[loc.y + d.y][loc.x + d.x] == '[')
            {
                isPossible = MakeStep2(d, (loc.x + d.x, loc.y + d.y));
            }

            if (map[loc.y + d.y][loc.x + d.x] == ']')
            {
                isPossible = MakeStep2(d, (loc.x + d.x - 1, loc.y + d.y));
            }

            if (isPossible)
            {
                map[loc.y + d.y][loc.x + d.x] = map[loc.y][loc.x];
                map[loc.y][loc.x] = '.';
            }

            DrawMap();
            return isPossible;
        }

        private bool MakeStep2((int x, int y) d, (int x, int y) loc)
        {
            if (map[loc.y + d.y][loc.x + d.x] == '#' || map[loc.y + d.y][loc.x + d.x + 1] == '#')
            {
                return false;
            }

            bool isPossible = true;

            if (d.x == 1 && map[loc.y + d.y][loc.x + d.x] == '[')
            {
                isPossible = MakeStep2(d, (loc.x + d.x, loc.y));
            }

            if (d.x == -1 && map[loc.y + d.y][loc.x + d.x] == ']')
            {
                isPossible = MakeStep2(d, (loc.x + d.x - 1, loc.y));
            }

            if (d.y != 0)
            {
                if (map[loc.y + d.y][loc.x + d.x] == '[')
                {
                    isPossible = MakeStep2(d, (loc.x, loc.y + d.y));
                }
                else if (map[loc.y + d.y][loc.x + d.x] == ']')
                {
                    isPossible = MakeStep2(d, (loc.x - 1, loc.y + d.y));
                }
                else if (map[loc.y + d.y][loc.x + d.x + 1] == '[')
                {
                    isPossible = MakeStep2(d, (loc.x, loc.y + d.y));
                }
                else if (map[loc.y + d.y][loc.x + d.x + 1] == ']')
                {
                    isPossible = MakeStep2(d, (loc.x - 1, loc.y + d.y));
                }
            }


            if (isPossible)
            {
                map[loc.y + d.y][loc.x + d.x] = map[loc.y][loc.x];
                map[loc.y][loc.x] = '.';
                DrawMap();

                map[loc.y + d.y][loc.x + d.x + 1] = map[loc.y][loc.x + 1];
                map[loc.y][loc.x + 1] = '.';

                DrawMap();
            }

            return isPossible;
        }

        private void ExpandMap()
        {
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == '#')
                    {
                        map[i].Insert(j, '#');
                        j++;
                    }

                    if (map[i][j] == '.')
                    {
                        map[i].Insert(j, '.');
                        j++;
                    }

                    if (map[i][j] == 'O')
                    {
                        map[i].Insert(j, '[');
                        j++;
                        map[i][j] = ']';
                    }

                    if (map[i][j] == '@')
                    {
                        j++;
                        map[i].Insert(j, '.');

                        robot.x = map[i].IndexOf('@');
                        robot.y = i;
                    }
                }
            }

        }

        private void DrawMap()
        {
            Console.Clear();
            for (int i = 0; i < map.Count*2; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();

            for (int i = 0; i < map.Count; i++)
            {
                map[i].ForEach(j => Console.Write(j));
                Console.WriteLine();
            }
        }

        protected override void ReadInput()
        {
            string path = GetInputPath();

            map = new List<List<char>>();
            robotSteps = new Queue<int>();

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();

                while (line != "")
                {
                    var arr = line.ToCharArray().ToList();

                    map.Add(arr);

                    if (line.Contains('@'))
                    {
                        robot.x = line.IndexOf("@");
                        robot.y = map.Count - 1;
                    }

                    line = reader.ReadLine();
                }

                while (reader.EndOfStream == false)
                {
                    line = reader.ReadLine();

                    foreach (var c in line)
                    {
                        robotSteps.Enqueue(GetDirectionFromChar(c));
                    }

                }
            }
        }

        private int GetDirectionFromChar(char c)
        {
            switch (c)
            {
                case '>':
                    return 0;
                case 'v':
                    return 1;
                case '<':
                    return 2;
                case '^':
                    return 3;
                default:
                    return 0;
            }
        }


    }
}
