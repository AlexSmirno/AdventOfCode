

using System.Collections.Generic;

namespace AdventOfCode.Year2024.Day5
{
    internal class Print_Queue : EventTask
    {
        private List<Tuple<int, int>> pairs;
        private List<List<int>> sequences;

        public override string GetAnswer1()
        {
            ReadInput();

            int sum = 0;

            for (int i = 0; i < sequences.Count; i++)
            {
                if (CheckSequence(sequences[i]))
                {
                    sum += sequences[i][sequences[i].Count / 2];
                }
            }

            return sum.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();

            int sum = 0;

            for (int i = 0; i < sequences.Count; i++)
            {
                if (CheckSequence(sequences[i]) == false)
                {
                    FixSequence(sequences[i], pairs);
                    sum += sequences[i][sequences[i].Count / 2];
                }
            }

            return sum.ToString();
        }

        private void FixSequence(List<int> nums, List<Tuple<int, int>> rules)
        {
            while (CheckSequence(nums) == false)
            {
                for (int i = 0; i < rules.Count; i++)
                {
                    int a = pairs[i].Item1;
                    int b = pairs[i].Item2;

                    int a_i = nums.IndexOf(a);
                    int b_i = nums.IndexOf(b);

                    if (a_i == -1 || b_i == -1) continue;

                    if (a_i > b_i)
                    {
                        (nums[a_i], nums[b_i]) = (nums[b_i], nums[a_i]);
                        break;
                    }
                }
            }
        }


        private bool CheckSequence(List<int> nums)
        {
            bool isRight = true;

            for (int j = 0; j < pairs.Count - 1; j++)
            {
                int a = pairs[j].Item1;
                int b = pairs[j].Item2;

                int a_i = nums.IndexOf(a);
                int b_i = nums.IndexOf(b);

                if (a_i == -1 || b_i == -1) continue;

                if (a_i > b_i)
                {
                    isRight = false;
                    break;
                }
            }


            return isRight;
        }

        protected override void ReadInput()
        {
            string path = GetInputPath();
            pairs = new List<Tuple<int, int>>();
            sequences = new List<List<int>>();

            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();
                while (reader.EndOfStream == false && line != "")
                {
                    string[] val = line.Split("|");

                    pairs.Add(
                        new Tuple<int, int>(
                            int.Parse(val[0]),
                            int.Parse(val[1]))
                    );

                    line = reader.ReadLine();
                }

                while (reader.EndOfStream == false)
                {
                    line = reader.ReadLine();
                    string[] val = line.Split(",");

                    var list = new List<int>();

                    for (int i = 0; i < val.Length; i++)
                    {
                        list.Add(int.Parse(val[i]));
                    }

                    sequences.Add(list);
                }
            }

        }
    }
}
