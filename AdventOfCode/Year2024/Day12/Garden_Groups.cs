

using System.Numerics;

namespace AdventOfCode.Year2024.Day12
{
    internal class Garden_Groups : EventTask
    {
        private List<List<char>> map;
        private List<List<bool>> visited;

        private List<(int, int)> directions = new List<(int, int)>()
        {
            (0, 1),
            (1, 0),
            (0, -1),
            (-1, 0),

        };

        private List<(int, int)> directions2 = new List<(int, int)>()
        {
            (0, 1),
            (1, 0),
            (0, -1),
            (-1, 0),

            (1, 1),
            (1, -1),
            (-1, -1),
            (-1, 1),
        };


        public override string GetAnswer1()
        {
            ReadInput();

            List<(char, int, int)> volumes = new List<(char, int, int)>();

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    int np, ns;
                    (np, ns) = Step(i, j, map[i][j]);

                    if (np == 0 || ns == 0) continue;
                    volumes.Add((map[i][j], np, ns));

                }
            }

            return volumes.Sum(item => item.Item2 * item.Item3).ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();

            int count = 0;

            List<(char, int, int)> volumes = new List<(char, int, int)>();

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    int np, ns;
                    (np, ns) = Step2(i, j, map[i][j]);

                    if (np == 0 || ns == 0) continue;
                    volumes.Add((map[i][j], np, ns));

                }
            }

            return volumes.Sum(item => item.Item2 * item.Item3).ToString();
        }


        private (int, int) Step(int y, int x, char field)
        {
            if (visited[y][x]) return (0, 0);

            visited[y][x] = true;

            int p = 0, s = 1;

            foreach (var d in directions)
            {
                if (IsOnField(y + d.Item2, x + d.Item1) &&
                    map[y + d.Item2][x + d.Item1] == field)
                {
                    int np, ns;
                    (np, ns) = Step(y + d.Item2, x + d.Item1, field);
                    p += np;
                    s += ns;
                }
                else p++;
            }

            return (p, s);
        }


        private (int, int) Step2(int y, int x, char field)
        {
            if (visited[y][x]) return (0, 0);

            visited[y][x] = true;

            int angels = 0, s = 1;

            int[] edges = new int[8];
            int e_i = 0;
            foreach (var d in directions2)
            {
                if (IsOnField(y + d.Item2, x + d.Item1) &&
                    map[y + d.Item2][x + d.Item1] == field)
                {
                    if (d.Item1 == 0 || d.Item2 == 0)
                    {
                        int na, ns;
                        (na, ns) = Step2(y + d.Item2, x + d.Item1, field);
                        angels += na;
                        s += ns;
                    }
                }
                else
                {
                    edges[e_i] = 1;
                }
                e_i++;
            }

            if (edges[0] + edges[1] == 2 || edges[0] + edges[1] == 0 && edges[4] == 1) angels++;
            if (edges[1] + edges[2] == 2 || edges[1] + edges[2] == 0 && edges[5] == 1) angels++;
            if (edges[2] + edges[3] == 2 || edges[2] + edges[3] == 0 && edges[6] == 1) angels++;
            if (edges[3] + edges[0] == 2 || edges[3] + edges[0] == 0 && edges[7] == 1) angels++;

            return (angels, s);
        }

        private bool IsOnField(int y, int x)
        {
            return x < map[0].Count &&
                    x >= 0 &&
                    y < map[0].Count &&
                    y >= 0;
        }

        private int CountAngels(int y, int x)
        {
            int count = 0;
            for (int i = 0; i < directions.Count - 1; i++)
            {
                var d1 = directions[i];
                var d2 = directions[i + 1];

                if (!IsOnField(y + d1.Item2, x + d1.Item1) &&
                    !IsOnField(y + d2.Item2, x + d2.Item1))
                {
                    count++;
                }
            }
            return count;
        }

        protected override void ReadInput()
        {
            string path = GetInputPath();

            map = new List<List<char>>();
            visited = new List<List<bool>>();


            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.EndOfStream == false)
                {
                    string line = sr.ReadLine();

                    map.Add(line.ToCharArray().ToList());

                    var list = new List<bool>();
                    for (int i = 0; i < line.Length; i++)
                    {
                        list.Add(false);
                    }
                    visited.Add(list);
                }
            }

        }
    }
}