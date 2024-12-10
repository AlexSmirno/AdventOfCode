

using System.Drawing;

namespace AdventOfCode.Year2024.Day8
{
    internal class Resonant_Collinearity : EventTask
    {
        private List<List<char>> map;

        private Dictionary<char, List<(int, int)>> antinodes;
        public override string GetAnswer1()
        {
            ReadInput();
            antinodes = CollectAntennas(map);

            int count = 0;

            foreach (var antinode in antinodes)
            {
                var coords = antinode.Value;

                for (int i = 0; i < coords.Count; i++)
                {
                    int x1, y1;
                    (y1, x1) = coords[i];
                    for (int j = 0; j < coords.Count && i != j; j++)
                    {
                        int x2, y2;
                        (y2, x2) = coords[j];

                        int x_c = x2 - x1;
                        int y_c = y2 - y1;

                        if (x2 + x_c >= 0 && x2 + x_c < map.Count &&
                            y2 + y_c >= 0 && y2 + y_c < map.Count &&
                            map[y2 + y_c][x2 + x_c] != '#')
                        {
                            map[y2 + y_c][x2 + x_c] = '#';
                            count++;
                        }

                        if (x1 - x_c >= 0 && x1 - x_c < map.Count &&
                            y1 - y_c >= 0 && y1 - y_c < map.Count &&
                            map[y1 - y_c][x1 - x_c] != '#')
                        {
                            map[y1 - y_c][x1 - x_c] = '#';
                            count++;
                        }
                    }
                }
            }

            DrawMap();

            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();

            antinodes = CollectAntennas(map);

            int count = 0;

            foreach (var antinode in antinodes)
            {
                var coords = antinode.Value;

                if (coords.Count > 2)
                {
                    count += coords.Count;
                }

                for (int i = 0; i < coords.Count; i++)
                {
                    int x1, y1;
                    (y1, x1) = coords[i];
                    for (int j = 0; j < coords.Count && i != j; j++)
                    {
                        int x2, y2;
                        (y2, x2) = coords[j];

                        int x_l = x2 - x1;
                        int y_l = y2 - y1;

                        int k = 1;
                        while (x2 + k*x_l >= 0 && x2 + k*x_l < map.Count &&
                               y2 + k*y_l >= 0 && y2 + k*y_l < map.Count)
                        {
                            if (map[y2 + k*y_l][x2 + k*x_l] == '.')
                            {
                                map[y2 + k*y_l][x2 + k*x_l] = '#';
                                count++;
                            }
                            k++;
                        }

                        k = 1;

                        while (x1 - k*x_l >= 0 && x1 - k*x_l < map.Count &&
                            y1 - k*y_l >= 0 && y1 - k*y_l < map.Count)
                        {
                            if (map[y1 - k*y_l][x1 - k*x_l] == '.')
                            {
                                map[y1 - k*y_l][x1 - k*x_l] = '#';
                                count++;
                            }
                            k++;
                        }
                    }
                }
            }

            DrawMap();

            return count.ToString();
        }


        private Dictionary<char, List<(int, int)>> CollectAntennas(List<List<char>> map)
        {
            Dictionary<char, List<(int, int)>> antennas = new Dictionary<char, List<(int, int)>>();

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] != '.')
                    {
                        if (antennas.ContainsKey(map[i][j]) == false)
                        {
                            antennas.Add(map[i][j], new List<(int, int)>());
                        }

                        antennas[map[i][j]].Add((i, j));
                    }
                }
            }

            return antennas;
        }

        private void DrawMap()
        {
            Console.Clear();
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

            using (StreamReader sr = new StreamReader(path)) 
            {
                while (sr.EndOfStream == false)
                {
                    string line = sr.ReadLine();
                    
                    var list = new List<char>();

                    for (int i = 0; i < line.Length; i++)
                    {
                        list.Add(line[i]);
                    }

                    map.Add(list);
                }
            }
        }
    }
}
