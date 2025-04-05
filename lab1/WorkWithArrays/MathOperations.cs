using System;

namespace ArrayApp
{
    public partial class ArrayActions
    {
        public double DoMath(double x, double y, double z)
        {
            const double Epsilon = 1e-7;
            double denominator = x + y;

            if (Math.Abs(denominator) < Epsilon)
            {
                return 0;
            }

            return (2 * Math.Sin(x) + 3 * y * Math.Pow(Math.Cos(z), 3)) / denominator;
        }

        public double AddBetweenPositives(double[] array)
        {
            int first = -1, last = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > 0)
                {
                    first = first == -1 ? i : first;
                    last = i;
                }
            }

            if (first == -1 || first == last) return 0;
            
            double sum = 0;
            for (int i = first; i <= last; i++)
            {
                sum += array[i];
            }
            return sum;
        }
    }
}