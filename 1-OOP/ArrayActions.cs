using System;

namespace ArrayApp
{
    public static class ArrayActions
    {
        public static double[] MixArrays(double[] firstArr, double[] secondArr, int idx)
        {
            int minIndexSecond = FindMin(secondArr, true);
            int minIndexFirst = FindMin(firstArr, false);

            if (idx <= minIndexFirst)
            {
                Console.WriteLine("Index too small!!!!!!");
                return new double[0];
            }

            var piece1 = GrabPiece(secondArr, minIndexSecond + 1);
            var piece2 = GrabPiece(firstArr, minIndexFirst + 1, idx - minIndexFirst);

            return StickTogether(piece1, piece2);
        }

        public static int FindMin(double[] data, bool firstOne)
        {
            int spot = 0;
            double min = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] < min || (!firstOne && data[i] == min))
                {
                    min = data[i];
                    spot = i;
                }
            }
            return spot;
        }

        public static double[] GrabPiece(double[] src, int start, int len = -1)
        {
            if (start >= src.Length)
            {
                return new double[0];
            }

            int size = (len == -1) ? src.Length - start : Math.Min(len, src.Length - start);
            var temp = new double[size];

            for (int i = 0; i < size; i++)
            {
                temp[i] = src[start + i];
            }

            return temp;
        }

        public static double[] StickTogether(double[] first, double[] second)
        {
            var temp = new double[first.Length + second.Length];

            for (int i = 0; i < first.Length; i++)
            {
                temp[i] = first[i];
            }

            for (int i = 0; i < second.Length; i++)
            {
                temp[first.Length + i] = second[i];
            }

            return temp;
        }

        public static double GetNum(double[] arr, int idx)
        {
            return (idx < arr.Length) ? arr[idx] : 0;
        }

        public static double DoMath(double a, double b, double c)
        {
            var top = 2 * Math.Sin(a) + 3 * b * Math.Pow(Math.Cos(c), 3);
            var bottom = a + b;

            if (Math.Abs(bottom) < 0.0000001)
                return 0;

            return top / bottom;
        }

        public static double AddBetweenPositives(double[] data)
        {
            int start = -1, end = -1;
            double total = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > 0)
                {
                    if (start == -1)
                    {
                        start = i;
                    }
                    end = i;
                }
            }

            if (start == -1 || start == end)
            {
                return 0;
            }

            for (int i = start; i <= end; i++)
            {
                total += data[i];
            }

            return total;
        }
    }
}
