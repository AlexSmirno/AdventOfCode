
namespace AdventOfCode.Year2024.Day1
{
    internal class Historian_Hysteria : EventTask
    {
        private List<int> arr1 = [3, 4, 2, 1, 3, 3];
        private List<int> arr2 = [4, 3, 5, 3, 9, 3];

        public override string GetAnswer1()
        {
            ReadInput();
            arr1.Sort();
            arr2.Sort();
            int sum = 0;

            for (int i = 0; i < arr1.Count; i++)
            {
                sum += Math.Abs(arr1[i] - arr2[i]);
            }


            return sum.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();
            arr1.Sort();
            arr2.Sort();

            var arr2_groups = arr2.GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.ToList());
            int sum = 0;

            for (int i = 0; i < arr1.Count; i++)
            {
                List<int> list = new List<int>();

                arr2_groups.TryGetValue(arr1[i], out list);

                if (list == null) continue;

                foreach (int item in list)
                {
                    sum += item;
                }

            }


            return sum.ToString();
        }

        protected override void ReadInput()
        {
            string path = GetInputPath();
            arr1 = new List<int>();
            arr2 = new List<int>();
            using (StreamReader reader = new StreamReader(path))
            {
                while (reader.EndOfStream == false)
                {
                    string line = reader.ReadLine();
                    string[] nums = line.Split("   ");

                    arr1.Add(int.Parse(nums[0]));
                    arr2.Add(int.Parse(nums[1]));
                    
                }
            }
        }

    }
}
