
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024.Day3
{
    internal class Mull_It_Over : EventTask
    {
        private string data;
        public override string GetAnswer1()
        {
            ReadInput();
            Regex regex = new Regex(@"mul\(([0-9]+,[0-9]+)\)");

            MatchCollection matches = regex.Matches(data);

            int res = 0;
            foreach (Match match in matches)
            {
                var nums = match.Value.Substring(4, match.Value.IndexOf(")") - 4).Split(',');

                res += int.Parse(nums[0]) * int.Parse(nums[1]);
            }

            return res.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();
            Regex regex = new Regex(@"(mul\(([0-9]+,[0-9]+)\)|don't\(\)|do\(\))");
            
            MatchCollection matches = regex.Matches(data);

            int res = 0;
            bool isDo = true;
            string startIgnore = "don't()";
            string endIgnore = "do()";

            foreach (Match match in matches)
            {
                if (match.Value.Equals(startIgnore))
                {
                    isDo = false;
                    continue;
                }

                if (match.Value.Equals(endIgnore))
                {
                    isDo = true;
                    continue;
                }

                if (isDo)
                {
                    var nums = match.Value.Substring(4, match.Value.IndexOf(")") - 4).Split(',');

                    res += int.Parse(nums[0]) * int.Parse(nums[1]);
                }
            }

            return res.ToString();
        }


        protected override void ReadInput()
        {
            string path = GetPathToInput();
            data = File.ReadAllText(path);
        }
    }
}
