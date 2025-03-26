using System;

namespace ArrayApp
{
    public static class ArrayReader
    {
        public static double[] ReadInputArray()
        {
            string inputLine = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputLine))
            {
                return new double[0];
            }

            var parts = inputLine.Split();
            var nums = new double[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                if (!double.TryParse(parts[i], out double value))
                {
                     Console.WriteLine($"Warning: '{parts[i]}' is not a valid number.");
                     value = 0;
                }

                nums[i] = value;
            }

            return nums;
        }
    }
}
