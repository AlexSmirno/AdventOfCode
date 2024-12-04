
using System.Text.RegularExpressions;
using static System.Math;

namespace AdventOfCode.Year2024.Day4
{
    internal class Ceres_Search : EventTask
    {
        private List<string> lines;
        //https://www.geeksforgeeks.org/rotate-matrix-by-45-degrees/
        public override string GetAnswer1()
        {
            ReadInput();


            int count = 0;

            count += CheckHorisontal(lines);
            count += CheckVertical(lines);
            count += CheckDiagonal1(lines);
            count += CheckDiagonal2(lines);

            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();

            return "";
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
            string path = GetPathToInput();
            lines = File.ReadAllText(path).Split("\r\n").ToList();
        }
    }
}
