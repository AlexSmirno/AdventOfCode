namespace AdventOfCode.Year2024.Day16
{
    internal class Reindeer_Maze : EventTask
    {
        private List<List<char>> map;
        private List<List<int>> used;
        private Deer deer;
        private (int x, int y) finish;

        public override string GetAnswer1()
        {
            ReadInput();


            Step(deer, 0);

            DrawMap(deer);
            return used[finish.y][finish.x].ToString();
        }

        public override string GetAnswer2()
        {
            deer.x = finish.x;
            deer.y = finish.y;


            int count = 0;
            count = StepBackward(deer);
            DrawMap(deer);

            return count.ToString();
        }

        private void Step(Deer deer, int points)
        {
            if (map[deer.y][deer.x] == '#') return;

            if (used[deer.y][deer.x] <= points)
            {
                return;
            }

            used[deer.y][deer.x] = points;

            for (int i = 0; i < 4; i++)
            {
                if (deer.d == i)
                    Step(deer.MakeStep(i), points + 1);
                else
                    Step(deer.MakeStep(i), points + 1001);
            }
        }

        private int StepBackward(Deer deer)
        {
            if (map[deer.y][deer.x] == '#' || map[deer.y][deer.x] == 'O') return 0;

            map[deer.y][deer.x] = 'O';

            int count = 1;


            for (int i = 0; i < deer.directions.Count; i++)
            {
                if (used[deer.y + deer.directions[i].y][deer.x + deer.directions[i].x] < used[deer.y][deer.x])
                {
                    count += StepBackward(deer.MakeStep(i));

                    if (deer.y + deer.directions[i].y * 2 >= 0 &&
                        deer.y + deer.directions[i].y * 2 < map.Count &&
                        deer.x + deer.directions[i].x * 2 >= 0 &&
                        deer.x + deer.directions[i].x * 2 < map.Count &&
                        used[deer.y + deer.directions[i].y * 2][deer.x + deer.directions[i].x * 2] < used[deer.y][deer.x])
                    {
                        count += StepBackward(deer.MakeStep(i).MakeStep(i));
                    }
                }
            }

            return count;
        }

        private void DrawMap(Deer deer)
        {
            Console.Clear();

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (deer.x == j && deer.y == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('D');
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }

                    if (map[i][j] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('O');
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    Console.Write(map[i][j]);
                }

                Console.WriteLine();
            }
        }

        protected override void ReadInput()
        {
            string path = GetInputPath();

            map = new List<List<char>>();
            used = new List<List<int>>();
            deer = new Deer();

            using (StreamReader reader = new StreamReader(path))
            {
                int y = 0;
                while (reader.EndOfStream == false)
                {
                    string line = reader.ReadLine();

                    map.Add(line.ToCharArray().ToList());
                    var list = new List<int>();

                    for (int i = 0; i < line.Length; i++)
                    {
                        list.Add(int.MaxValue);
                    }
                    used.Add(list);

                    if (line.Contains('S'))
                    {
                        deer.x = line.IndexOf('S');
                        deer.y = y;
                        deer.d = 0; // Восток (x + 1)
                    }

                    if (line.Contains('E'))
                    {
                        finish.x = line.IndexOf('E');
                        finish.y = y;
                    }
                    y++;
                }
            }

        }

        private class Deer
        {
            public int x;
            public int y;
            public int d;

            public List<(int x, int y)> directions = new List<(int x, int y)>()
            {
                (1, 0),
                (0, 1),
                (-1, 0),
                (0, -1),
            };


            public Deer() { }
            public Deer(int x, int y, int d)
            {
                this.x = x;
                this.y = y;
                this.d = d;
            }

            public Deer MakeStep(int direction)
            {
                return new Deer(
                    x + directions[direction].x,
                    y + directions[direction].y,
                    direction);
            }


        }
    }
}
