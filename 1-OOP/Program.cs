using System;

namespace ArrayApp
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Type first array (spaces between nums please):");
            var firstArray = ArrayReader.ReadInputArray();

            Console.WriteLine("Ok now second array:");
            var secondArray = ArrayReader.ReadInputArray();

            Console.Write("Give me index: ");
            if (!int.TryParse(Console.ReadLine(), out int cutIndex))
            {
                Console.WriteLine("Incorrect index!");
                return;
            }

            try
            {
                var result = ArrayActions.MixArrays(firstArray, secondArray, cutIndex);
                if (result.Length == 0)
                {
                    Console.WriteLine("ERROR!");
                    return;
                }

                double x = ArrayActions.GetNum(firstArray, 7);
                double y = ArrayActions.GetNum(secondArray, 14);
                double z = ArrayActions.GetNum(result, 3);

                if (x == y || x == z) x = ArrayActions.GetNum(firstArray, 8);
                if (y == x || y == z) y = ArrayActions.GetNum(secondArray, 15);
                if (z == x || z == y) z = ArrayActions.GetNum(result, 4);

                var ans = ArrayActions.DoMath(x, y, z);
                Console.WriteLine($"Got this result: {ans}");

                var sumBetweenPositives = ArrayActions.AddBetweenPositives(result);
                Console.WriteLine($"Sum between positives: {sumBetweenPositives}");
            }
            catch
            {
                Console.WriteLine("Something error!");
            }
        }
    }
}
