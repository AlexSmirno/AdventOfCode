using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024.Day4
{
    internal class Ceres_Search : EventTask
    {
        private List<string> lines;
        //https://www.geeksforgeeks.org/rotate-matrix-by-45-degrees/

        private const string word = "XMAS";
        private const int len = 4;

        private Tuple<int, int>[] directions = new Tuple<int, int>[8]
            {
                new Tuple<int, int> (-1, -1),
                new Tuple<int, int> (0, -1),
                new Tuple<int, int> (1, -1),

                new Tuple<int, int> (-1, 0),
                new Tuple<int, int> (1, 0),

                new Tuple<int, int> (-1, 1),
                new Tuple<int, int> (0, 1),
                new Tuple<int, int> (1, 1)
            };

        public override string GetAnswer1()
        {
            ReadInput();

            int count = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    for (int k = 0; k < directions.Length; k++)
                    {
                        if (CheckDirection(j, directions[k].Item1, i, directions[k].Item2))
                        {
                            count++;
                        }
                    }
                }
            }


            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();

            int count = 0;

            for (int i = 1; i < lines.Count - 1; i++)
            {
                for (int j = 1; j < lines[0].Length - 1; j++)
                {
                    if (CheckMAS(j, i))
                    {
                        count++;
                    }
                }
            }

            return count.ToString();
        }



        private bool CheckMAS(int x, int y)
        {
            if (x < 1 || y < 1 || x > lines[0].Length - 2 || y > lines.Count - 2)
            {
                return false;
            }

            string str1 = new string(new char[] { lines[y - 1][x - 1], lines[y][x], lines[y + 1][x + 1] });
            string str2 = new string(new char[] { lines[y - 1][x + 1], lines[y][x], lines[y + 1][x - 1] });


            return (str1 == "MAS" || str1 == "SAM") &&
                (str2 == "MAS" || str2 == "SAM");
        }


        private bool CheckDirection(int x, int dx, int y, int dy)
        {
            //bounds for word
            int bx = x + (len - 1)*dx;
            int by = y + (len - 1)*dy;

            if (bx < 0 || by < 0 || bx >= lines[0].Length || by >= lines[0].Length)
            {
                return false;
            }


            for (int i = 0; i < len; i++)
            {
                int nx = x + i*dx;
                int ny = y + i*dy;

                if (lines[ny][nx] != word[i])
                {
                    return false;
                }
            }

            return true;
        }








        private List<string> RotateMatrix45(List<string> matrix, int n, int m)
        {
            int ctr = 0;
            List<string> newM = new List<string>();

            while (ctr < 2*n-1)
            {
                List<char> lst = new List<char>();

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if(i + j == ctr)
                        {
                            lst.Add(matrix[i][j]);
                        }
                    }
                }

                newM.Add(new string(lst.ToArray()));
                ctr++;
            }

            return newM;
        }

        private List<string> RotateMatrix90(List<string> matrix, int n, int m)
        {
            List<string> newM = new List<string>();

            for (int i = 0; i < m; i++)
            {
                StringBuilder str = new StringBuilder();
                for (int j = 0; j < n; j++)
                {
                    str.Append(matrix[j][i]);
                }
                newM.Add(str.ToString());
            }

            return newM;
        }


        private int CheckHorisontal(List<string> lines)
        {
            int count = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                count += CheckLine(lines[i]);
            }
            return count;

        }


        private int CheckVertical(List<string> lines)
        {
            int count = 0;
            List<List<char>> verts_chars = new List<List<char>>(lines[0].Length);

            for (int i = 0; i < lines[0].Length; i++)
            {
                verts_chars.Add(new List<char>(lines.Count));
            }

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    verts_chars[i].Add(lines[j][i]);
                }
            }

            for (int i = 0; i < lines[0].Length; i++)
            {
                string line = new string(verts_chars[i].ToArray());

                count += CheckLine(line);
            }

            return count;
        }


        private int CheckDiagonal1(List<string> lines)
        {
            List<List<char>> diag_chars = new List<List<char>>(lines[0].Length);

            for (int i = 0; i < lines[0].Length + lines.Count; i++)
            {
                diag_chars.Add(new List<char>(lines.Count));
            }

            int col = lines[0].Length;
            int row = lines.Count;
            int count = 0;
            for (int i = 0; i < row; i++)
            {
                diag_chars[i].Add(lines[i][0]);
                int r = i - 1;
                int c = 1;
                while (isValid(r, c))
                {
                    diag_chars[i].Add(lines[r][c]);

                    r--;
                    c++;
                }
            }

            for (int i = 0; i < col; i++)
            {
                diag_chars[row + i].Add(lines[row-1][i]);
                int r = row - 2;
                int c = i + 1;
                while (isValid(r, c))
                {
                    diag_chars[row + i].Add(lines[r][c]);

                    r--;
                    c++;
                }
            }

            bool isValid(int i, int j)
            {
                if (i < 0 || i >= row || j >= col || j < 0)
                    return false;
                return true;
            }

            for (int i = 0; i < diag_chars.Count; i++)
            {
                string line = new string(diag_chars[i].ToArray());
                Console.WriteLine(line);
                count += CheckLine(line);
            }


            return count;
        }


        private int CheckDiagonal2(List<string> lines)
        {
            List<List<char>> diag_chars = new List<List<char>>(lines[0].Length);

            for (int i = 0; i < lines[0].Length + lines.Count - 1; i++)
            {
                diag_chars.Add(new List<char>(lines.Count));
            }

            int col = lines[0].Length;
            int row = lines.Count;
            int count = 0;
            for (int i = row - 1; i >= 0; i--)
            {
                diag_chars[row - 1 - i].Add(lines[i][0]);
                int r = i + 1;
                int c = 1;
                while (isValid(r, c))
                {
                    diag_chars[row - 1 - i].Add(lines[r][c]);

                    r++;
                    c++;
                }
            }

            for (int i = 1; i < col; i++)
            {
                diag_chars[row - 1 + i].Add(lines[0][i]);
                int r = 1;
                int c = i + 1;

                while (isValid(r, c))
                {
                    diag_chars[row - 1 + i].Add(lines[r][c]);


                    r++;
                    c++;
                }
            }

            bool isValid(int i, int j)
            {
                if (i < 0 || i >= row || j >= col || j < 0)
                    return false;
                return true;
            }

            for (int i = 0; i < diag_chars.Count; i++)
            {
                string line = new string(diag_chars[i].ToArray());

                Console.WriteLine(line);
                count += CheckLine(line);
            }


            return count;
        }

        private int CheckLine(string line)
        {
            Regex regex = new Regex(@"(?=(XMAS|SAMX))");

            MatchCollection matches = regex.Matches(line);

            return matches.Count;
        }

        protected override void ReadInput()
        {
            lines = new List<string>();
            string path = GetInputPath();
            lines = File.ReadAllText(path).Split("\r\n").ToList();
        }
    }
}
