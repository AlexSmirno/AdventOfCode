

using System.ComponentModel;

namespace AdventOfCode.Year2024.Day14
{
    internal class Restroom_Redoubt : EventTask
    {
        private List<Robot> _robots;
        public override string GetAnswer1()
        {
            ReadInput();

            int width = 101;
            int height = 103;

            int count = 0;


            foreach (Robot r in _robots)
            {
                r.Step(100, width, height);
            }

            int one = CountRobots((0, height / 2), (0, width / 2));
            int two = CountRobots((0, height / 2), (width / 2 + 1, width));
            int three = CountRobots((height / 2 + 1, height), (0, width / 2));
            int four = CountRobots((height / 2 + 1, height), (width / 2 + 1, width));

            count = one * two * three * four;

            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();

            int width = 101;
            int height = 103;

            int count = 0;


            while (IsEasterEgg(width, height) == false)
            {
                foreach (Robot r in _robots)
                {
                    r.Step(1, width, height);
                }
                count++;
            }
            DrawMap(width, height);



            return count.ToString();
        }

        private bool IsEasterEgg(int width, int height)
        {
            int segment_count = 10;
            (int, int) segment = (width / segment_count, height / segment_count);

            int tree_density = 50;

            for (int i = 0; i < segment_count; i++)
            {
                for (int j = 0; j < segment_count; j++)
                {
                    int density = CountRobots((i*segment.Item1, (i + 1)*segment.Item1), (j*segment.Item2, (j + 1)*segment.Item2));

                    if (density > tree_density)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected override void ReadInput()
        {
            string path = GetInputPath();
            _robots = new List<Robot>();

            using (StreamReader reader = new StreamReader(path))
            {
                while (reader.EndOfStream == false)
                {
                    string line = reader.ReadLine();

                    Robot r = new Robot(line);

                    _robots.Add(r);
                }
            }
        }

        private int CountRobots((int,int) height, (int, int) width)
        {
            int count = 0;

            for (int k = 0; k < _robots.Count; k++)
            {
                if (_robots[k].Position.Item1 >= width.Item1 &&
                    _robots[k].Position.Item1 < width.Item2 &&
                    _robots[k].Position.Item2 >= height.Item1 &&
                    _robots[k].Position.Item2 < height.Item2)
                {
                    count++;
                }
            }

            return count;
        }

        private void DrawMap(int width, int height)
        {
            Console.Clear();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int count = 0;
                    for(int k = 0; k < _robots.Count; k++)
                    {
                        if (_robots[k].Position.Item2 == i && _robots[k].Position.Item1 == j)
                        {
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        Console.Write(count + " ");
                        continue;
                    }

                    Console.Write(". ");

                }
                Console.WriteLine();
            }
        }


        private class Robot
        {
            public (int, int) Position;
            public (int, int) Speed;

            public Robot(string line)
            {
                var arr = line.Split(' ');

                string p = arr[0];
                string v = arr[1];

                var nums = p.Substring(p.IndexOf("p=") + 2).Split(',');

                Position = (int.Parse(nums[0]), int.Parse(nums[1]));

                nums = v.Substring(v.IndexOf("v=") + 2).Split(',');

                Speed = (int.Parse(nums[0]), int.Parse(nums[1]));
            }

            public void Step(int count, int width, int height)
            {
                Position.Item1 += Speed.Item1 * count;
                Position.Item2 += Speed.Item2 * count;


                Position.Item1 = Position.Item1 % width;
                Position.Item2 = Position.Item2 % height;

                if (Position.Item1 < 0)
                {
                    Position.Item1 = width + Position.Item1;
                }

                if (Position.Item2 < 0)
                {
                    Position.Item2 = height + Position.Item2;
                }
            }
        }
    }
}
