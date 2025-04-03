using System;

namespace ArrayApp
{
   
    public class ArrayReader
    {
        
        public double[] ReadInputArray()
        {
            string inputLine = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputLine))
            {
                return new double[0];
            }

            string[] parts = inputLine.Split();
            double[] nums = new double[parts.Length];

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