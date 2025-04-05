using System;

namespace ArrayApp
{
    public partial class ArrayActions
    {
        public double[] MixArrays(double[] primaryArray, double[] secondaryArray, int cutIndex)
        {
            int minSecondaryIndex = FindMinIndex(secondaryArray, true);
            int minPrimaryIndex = FindMinIndex(primaryArray, false);

            if (cutIndex <= minPrimaryIndex)
            {
                Console.WriteLine("Index too small!");
                return Array.Empty<double>();
            }

            return Concatenate(
                GetSubArray(secondaryArray, minSecondaryIndex + 1),
                GetSubArray(primaryArray, minPrimaryIndex + 1, cutIndex - minPrimaryIndex)
            );
        }

        private int FindMinIndex(double[] array, bool findFirstOccurrence)
        {
            int minIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < array[minIndex] || (!findFirstOccurrence && array[i] == array[minIndex]))
                {
                    minIndex = i;
                }
            }
            return minIndex;
        }

        private double[] GetSubArray(double[] source, int startIndex, int length = -1)
        {
            if (startIndex >= source.Length) return Array.Empty<double>();
            
            int actualLength = length == -1 
                ? source.Length - startIndex 
                : Math.Min(length, source.Length - startIndex);
            
            double[] result = new double[actualLength];
            Array.Copy(source, startIndex, result, 0, actualLength);
            return result;
        }

        private double[] Concatenate(double[] firstArray, double[] secondArray)
        {
            double[] combined = new double[firstArray.Length + secondArray.Length];
            Array.Copy(firstArray, 0, combined, 0, firstArray.Length);
            Array.Copy(secondArray, 0, combined, firstArray.Length, secondArray.Length);
            return combined;
        }

        public double GetNum(double[] array, int index)
        {
            return index < array.Length ? array[index] : 0;
        }
    }
}