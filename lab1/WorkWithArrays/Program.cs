using System;

namespace ArrayApp
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Type first array (spaces between nums please):");
            var reader = new ArrayReader();
            double[] firstArray = reader.ReadInputArray();

            Console.WriteLine("Ok now second array:");
            double[] secondArray = reader.ReadInputArray();

            Console.Write("Give me index: ");
            if (!int.TryParse(Console.ReadLine(), out int cutIndex))
            {
                Console.WriteLine("Incorrect index!");
                return;
            }

            try
            {
                var actions = new ArrayActions();
                double[] result = actions.MixArrays(firstArray, secondArray, cutIndex);
                
                if (result.Length == 0)
                {
                    Console.WriteLine("ERROR!");
                    return;
                }

                const double epsilon = 1e-9;
                double x = actions.GetNum(firstArray, 7);
                double y = actions.GetNum(secondArray, 14);
                double z = actions.GetNum(result, 3);

                if (Math.Abs(x - y) < epsilon || Math.Abs(x - z) < epsilon)
                {
                    x = actions.GetNum(firstArray, 8);
                }

                if (Math.Abs(y - x) < epsilon || Math.Abs(y - z) < epsilon)
                {
                    y = actions.GetNum(secondArray, 15);
                }

                if (Math.Abs(z - x) < epsilon || Math.Abs(z - y) < epsilon)
                {
                    z = actions.GetNum(result, 4);
                }

                double ans = actions.DoMath(x, y, z);
                Console.WriteLine($"Got this result: {ans}");

                double sumBetweenPositives = actions.AddBetweenPositives(result);
                Console.WriteLine($"Sum between positives: {sumBetweenPositives}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong! Error: {ex.Message}");
            }
        }
    }
}