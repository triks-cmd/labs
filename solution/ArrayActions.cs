using System;

namespace ArrayApp
{
    public static class ArrayActions
    {
        public static double[] MixArrays(double[] primaryArray, double[] secondaryArray, int index)
        {
            int minIndexSecond = FindMin(secondaryArray, true);
            int minIndexFirst = FindMin(primaryArray, false);

            if (index <= minIndexFirst)
            {
                Console.WriteLine("Index too small!!!!!!");
                return new double[0];
            }

            var piece1 = GrabPiece(secondaryArray, minIndexSecond + 1);
            var piece2 = GrabPiece(primaryArray, minIndexFirst + 1, index - minIndexFirst);

            return StickTogether(piece1, piece2);
        }

        public static int FindMin(double[] data, bool findFirstOccurrence)
        {
            int minIndex = 0;
            double minValue = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] < minValue || (!findFirstOccurrence && data[i] == minValue))
                {
                    minValue = data[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }

        public static double[] GrabPiece(double[] source, int startIndex, int length = -1)
        {
            if (startIndex >= source.Length)
            {
                return new double[0];
            }

            int size = (length == -1) ? source.Length - startIndex : Math.Min(length, source.Length - startIndex);
            var result = new double[size];

            for (int i = 0; i < size; i++)
            {
                result[i] = source[startIndex + i];
            }

            return result;
        }

        public static double[] StickTogether(double[] first, double[] second)
        {
            var combinedArray = new double[first.Length + second.Length];

            for (int i = 0; i < first.Length; i++)
            {
                combinedArray[i] = first[i];
            }

            for (int i = 0; i < second.Length; i++)
            {
                combinedArray[first.Length + i] = second[i];
            }

            return combinedArray;
        }

        public static double GetNum(double[] array, int index)
        {
            return (index < array.Length) ? array[index] : 0;
        }

        public static double DoMath(double a, double b, double c)
        {
            var numerator = 2 * Math.Sin(a) + 3 * b * Math.Pow(Math.Cos(c), 3);
            var denominator = a + b;

            if (Math.Abs(denominator) < 0.0000001)
                return 0;

            return numerator / denominator;
        }

        public static double AddBetweenPositives(double[] data)
        {
            int firstPositiveIndex = -1, lastPositiveIndex = -1;
            double sum = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > 0)
                {
                    if (firstPositiveIndex == -1)
                    {
                        firstPositiveIndex = i;
                    }
                    lastPositiveIndex = i;
                }
            }

            if (firstPositiveIndex == -1 || firstPositiveIndex == lastPositiveIndex)
            {
                return 0;
            }

            for (int i = firstPositiveIndex; i <= lastPositiveIndex; i++)
            {
                sum += data[i];
            }

            return sum;
        }
    }
}
