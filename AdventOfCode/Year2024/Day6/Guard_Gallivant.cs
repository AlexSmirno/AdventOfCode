using System.Linq;
using System.Reflection;

namespace AdventOfCode.Year2024.Day6
{
    internal class Guard_Gallivant : EventTask
    {
        private char[][] field;
        private Guardian guardian;

        // (x,y,dx,dy)
        private HashSet<(int, int, int, int)> path;


        public override string GetAnswer1()
        {
            ReadInput();
            path = new HashSet<(int, int, int, int)>();

            int steps = 0;

            int stepVal = Step();
            while (stepVal >= 0)
            {
                steps += stepVal;

                stepVal = Step();
            }

            steps += 1;
            return steps.ToString();
        }

        // Too slow...
        public override string GetAnswer2()
        {

            int count = 0;

            for (int i = 0; i < field.Length; i++)
            {
                for (int j = 0; j < field[i].Length; j++)
                {
                    ReadInput();
                    path = new HashSet<(int, int, int, int)>();

                    field[i][j] = '-';

                    int stepVal = Step();
                    while (stepVal >= 0)
                    {
                        stepVal = Step();
                    }

                    if (stepVal == -2)
                    {
                        count++;
                        //Console.ForegroundColor = ConsoleColor.Green;
                    }

                    //DrawField();
                    //Console.ForegroundColor = ConsoleColor.White;
                }
            }

            //DrawField();
            return count.ToString();
        }


        //  1 - все норм
        //  0 - здесь были
        // -1 - ушел за пределы видимости
        // -2 - цикл
        private int Step()
        {
            int state = CheckCell(
                guardian.x,
                guardian.y,
                guardian.dx,
                guardian.dy
                );

            if (state == -1)
            {
                field[guardian.y][guardian.x] = 'X';
                return -1;
            }

            if (state == 1)
            {
                guardian.RotateRight();
                return Step();
                
            }

            if (state == -2)
            {
                return -2;
            }

            field[guardian.y][guardian.x] = 'X';

            guardian.y += guardian.dy;
            guardian.x += guardian.dx;

            field[guardian.y][guardian.x] = GetGuardChar(guardian.direction);


            if (state == 2)
            {
                return 0;
            }

            path.Add((guardian.x - guardian.dx,
                guardian.y - guardian.dy,
                guardian.dx,
                guardian.dy));

            return 1;
        }


        //  2 - препятствие
        //  1 - все норм
        //  0 - здесь были
        // -1 - ушел за пределы видимости
        // -2 - цикл
        private int CheckCell(int x, int y, int dx, int dy)
        {
            if (x + dx < 0 ||
                y + dy < 0 ||
                x + dx >= field[0].Length ||
                y + dy >= field.Length)
            {
                return -1;
            }

            if (field[y + dy][x + dx] == '#' || field[y + dy][x + dx] == '-')
            {
                return 1;
            }

            if (path.Contains((x + dx, y + dy, dx, dy)))
            {
                return -2;
            }

            if (field[y + dy][x + dx] == 'X')
            {
                return 2;
            }

            return 0;
        }

        private void DrawField()
        {
            Console.Clear();
            for (int i = 0; i < field.Length; i++)
            {
                for (int j = 0; j < field[0].Length; j++)
                {
                    Console.Write(field[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
        protected override void ReadInput()
        {
            string path = GetInputPath();

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();
                field = new char[line.Length][];
                int i = 0;
                field[i] = FillLine(line, i);
                while (reader.EndOfStream == false)
                {
                    i++;
                    line = reader.ReadLine();
                    field[i] = FillLine(line, i);
                }
            }
        }


        private char[] FillLine(string line, int y)
        {
            var fieldLine = new char[line.Length];

            for (int i = 0; i < line.Length; i++)
            {
                fieldLine[i] = line[i];
                if (fieldLine[i] == '>' ||
                    fieldLine[i] == '<' ||
                    fieldLine[i] == 'v' ||
                    fieldLine[i] == '^')
                {
                    guardian = new Guardian();
                    guardian.direction = GetDirection(fieldLine[i]);
                    guardian.x = i;
                    guardian.y = y;
                }
            }

            return fieldLine;
        }
        private int GetDirection(char c)
        {
            switch (c)
            {
                case '^':
                    return 0;
                case '>':
                    return 1;
                case 'v':
                    return 2;
                case '<':
                    return 3;
                default:
                    return 0;
            }
        }

        private char GetGuardChar(int direction)
        {
            switch (direction)
            {
                case 0:
                    return '^';
                case 1:
                    return '>';
                case 2:
                    return 'v';
                case 3:
                    return '<';
                default:
                    return '^';
            }
        }


        private class Guardian
        {
            private int d;
            public int direction
            {
                get { return d; }
                set 
                {
                    d = value;

                    if (direction >= 4) direction = 0;

                    dx = directions[d].Item1;
                    dy = directions[d].Item2;
                }
            }
            public int x;
            public int y;

            public int dx { get; private set; }
            public int dy { get; private set; }

            public void RotateRight()
            {
                direction++;
            }
            public int GetDirectionX()
            {
                return directions[direction].Item1;
            }

            public int GetDirectionY()
            {
                return directions[direction].Item2;
            }

            private Tuple<int, int>[] directions = new Tuple<int, int>[4]
                {
                new Tuple<int, int> (0, -1),
                new Tuple<int, int> (1, 0),
                new Tuple<int, int> (0, 1),
                new Tuple<int, int> (-1,0),
                };
        }
    }

    
}
