using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2024.Day9
{
    internal class Disk_Fragmenter : EventTask
    {
        private List<char> data;
        public override string GetAnswer1()
        {
            ReadInput();

            List<int> disk = PrepareDisk(data);

            for (int i = disk.Count - 1; i >= 0; i--)
            {
                if (disk[i] == -1) continue;

                for (int j = 0; j < disk.Count; j++)
                {
                    if (disk[j] != -1 || i <= j) continue;

                    disk[j] = disk[i];
                    disk[i] = -1;
                }
            }

            long count = CalcSum(disk);

            return count.ToString();
        }

        public override string GetAnswer2()
        {
            ReadInput();

            List<int> disk = PrepareDisk(data);


            for (int i = disk.Count - 1; i >= 0; i--)
            {
                if (disk[i] == - 1) continue;

                int data_l = 0;
                while (i - data_l >= 0 && disk[i - data_l] == disk[i]) data_l++;

                for (int j = 0; j < disk.Count && j < i; j++)
                {
                    if (disk[j] != -1) continue;

                    int l = 0;

                    while (j + l < disk.Count && disk[j + l] == -1) l++;

                    if (l >= data_l)
                    {
                        for (int k = 0; k < data_l; k++)
                        {
                            disk[j + k] = disk[i - k];
                            disk[i - k] = -1;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    j += l;


                    break;
                }

                i -= data_l - 1; // -1 от длины так как на след. итерации будет еще раз -1
            }

            long count = CalcSum(disk);

            return count.ToString();
        }


        private long CalcSum(List<int> disk)
        {
            long sum = 0;
            for (int i = 0; i < disk.Count; i++)
            {
                if (disk[i] == -1) continue;

                sum += disk[i] * i;
            }
            return sum;
        }
        private List<int> PrepareDisk(List<char> data)
        {
            List<int> disk = new List<int>();
            int id = 0;
            for (int i = 0; i < data.Count; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < int.Parse(data[i].ToString() ); j++)
                    {
                        disk.Add(id);
                    }

                    continue;
                }

                for (int j = 0; j < int.Parse(data[i].ToString()); j++)
                {
                    disk.Add(-1);
                }
                id++;
            }

            return disk;
        }

        protected override void ReadInput()
        {
            string path = GetInputPath();

            using (StreamReader reader = new StreamReader(path))
            {
                data = reader.ReadLine().ToList();
            }
        }
    }
}
