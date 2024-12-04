
namespace AdventOfCode.Year2024.Day2
{
    internal class Red_Nosed_Reports : EventTask
    {
        private List<List<int>> reports;
        public List<int> rights = new List<int>();
        public override string GetAnswer1()
        {
            ReadInput();
            int count = 0;

            for (int i = 0; i < reports.Count; i++)
            {
                var report = reports[i];

                if (IsDecent(report) == 0 || IsChangeSlow(report) == false)
                {
                    continue;
                }

                count++;
            }

            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();
            int count = 0;

            for (int i = 0; i < reports.Count; i++)
            {
                var report = new List<int>(reports[i]);
                int index = 0;
                bool isRight = true;
                while (IsDecent(report) == 0 || IsChangeSlow(report) == false)
                {
                    if (index == reports[i].Count)
                    {
                        isRight = false;
                        break;
                    }

                    report = new List<int>(reports[i]);
                    report.RemoveAt(index);
                    index++;
                }

                if (isRight)
                {
                    rights.Add(i);
                    count++;
                }
            }

            return count.ToString();
        }


        //  0 - нет закономерности
        //  1 - возрастание
        // -1 - убывание
        private int IsDecent(List<int> nums)
        {
            bool isDesent = nums[0] - nums[1] > 0;

            for (int i = 1; i < nums.Count - 1; i++)
            {
                if (nums[i] - nums[i + 1] > 0 != isDesent)
                {
                    return 0;
                }
            }

            if (isDesent) return -1;

            return 1;
        }

        private bool IsChangeSlow(List<int> report)
        {
            for (int i = 0; i < report.Count - 1; i++)
            {
                int diff = Math.Abs(report[i] - report[i + 1]);
                if (diff < 1 || 3 < diff)
                {
                    return false;
                }
            }

            return true;
        }


        protected override void ReadInput()
        {
            reports = new List<List<int>>();
            string path = GetPathToInput();

            using (StreamReader reader = new StreamReader(path))
            {
                while (reader.EndOfStream == false)
                {
                    string str = reader.ReadLine();
                    var list = str.Split().ToList();

                    var list_int = new List<int>();

                    foreach (var item in list)
                    {
                        list_int.Add(int.Parse(item));
                    }

                    reports.Add(list_int);
                }
            }
        }
    }
}
