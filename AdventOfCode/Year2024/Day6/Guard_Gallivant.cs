using System.Reflection;

namespace AdventOfCode.Year2024.Day6
{
    internal class Guard_Gallivant : EventTask
    {
        private char[][] field;
        private Guardian guardian;

        private Tuple<int, int>[] directions = new Tuple<int, int>[4]
            {
                new Tuple<int, int> (0, -1),
                new Tuple<int, int> (1, 0),
                new Tuple<int, int> (0, 1),
                new Tuple<int, int> (-1,0),
            };

        public override string GetAnswer1()
        {
            ReadInput();

            int steps = 0;

            int stepVal = Step();
            while (stepVal >= 0)
            {
                steps += stepVal;

                stepVal = Step();
            }
            DrawField();
            steps += 1;
            return steps.ToString();
        }

        private int Step()
        {
            if (guardian.x + directions[guardian.direction].Item1 < 0 ||
                guardian.y + directions[guardian.direction].Item2 < 0 ||
                guardian.x + directions[guardian.direction].Item1 >= field[0].Length ||
                guardian.y + directions[guardian.direction].Item2 >= field.Length)
            {
                field[guardian.y][guardian.x] = 'X';
                return -1;
            }

            if (field[guardian.y + directions[guardian.direction].Item2]
                     [guardian.x + directions[guardian.direction].Item1]
                      == '#')
            {
                guardian.direction++;
                if (guardian.direction >= 4)
                {
                    guardian.direction = 0;
                }
            }

            int isUniqPlace = 1;

            if (field[guardian.y + directions[guardian.direction].Item2]
                     [guardian.x + directions[guardian.direction].Item1]
                      == 'X')
            {
                isUniqPlace = 0;
            }

            field[guardian.y][guardian.x] = 'X';

            guardian.y += directions[guardian.direction].Item2;
            guardian.x += directions[guardian.direction].Item1;

            field[guardian.y][guardian.x] = GetGuardChar(guardian.direction);

            return isUniqPlace;
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

        public override string GetAnswer2()
        {

            return "";
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
            public int direction;
            public int x;
            public int y;
        }
    }

    
}
