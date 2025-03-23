class ArrayStuff  
{
    static void Main()
    {
        Console.WriteLine("Type first array (spaces between nums please):");
        var nums1 = ReadStuff();

        Console.WriteLine("Ok now second array:");
        var nums2 = ReadStuff();

        Console.Write("Give me index ");
        int cut = int.Parse(Console.ReadLine());

        try 
        {
            var result = MixArrays(nums1, nums2, cut);
            if(result.Length == 0) 
            {
                Console.WriteLine("ERROR!");
                return;
            }

            double x = GetNum(nums1, 7);
            double y = GetNum(nums2, 14);
            double z = GetNum(result, 3);

            if(x == y || x == z) x = GetNum(nums1, 8);
            if(y == x || y == z) y = GetNum(nums2, 15);
            if(z == x || z == y) z = GetNum(result, 4);

            var ans = DoMath(x, y, z);
            Console.WriteLine($"Got this result: {ans}");

            var sum = AddBetweenPositives(result);
            Console.WriteLine($"Sum between positives: {sum}");
        }
        catch 
        {
            Console.WriteLine("Something broke!");
        }
    }

    static double[] ReadStuff()
    {
        var input = Console.ReadLine().Split();
        var nums = new double[input.Length];

        for(int i = 0; i < input.Length; i++)
        {
             nums[i] = double.Parse(input[i]);
        }

        return nums;
    }

    static double[] MixArrays(double[] arr1, double[] arr2, int idx)
    {
        int p1 = FindMin(arr2, true);
        int p2 = FindMin(arr1, false);

        if(idx <= p2)
        {
            Console.WriteLine("Index too small!!!!!!");
            return new double[0];
        }

        var piece1 = GrabPiece(arr2, p1 + 1);
        var piece2 = GrabPiece(arr1, p2 + 1, idx - p2);

        return StickTogether(piece1, piece2);
    }

    static int FindMin(double[] data, bool firstOne)
    {
        int spot = 0;
        double min = data[0];

        for(int i = 1; i < data.Length; i++)
        {
            if(data[i] < min || (!firstOne && data[i] == min))
            {
                min = data[i];
                spot = i;
            }
        }
        return spot;
    }

    static double[] GrabPiece(double[] src, int start, int len = -1)
    {
        if(start >= src.Length)
        {
              return new double[0];
        }

        int size = (len == -1) ? src.Length - start : Math.Min(len, src.Length - start);
        var temp = new double[size];

        for(int i = 0; i < size; i++)
        {
             temp[i] = src[start + i];
        }
           

        return temp;
    }

    static double[] StickTogether(double[] first, double[] second)
    {
        var temp = new double[first.Length + second.Length];
        
        for(int i = 0; i < first.Length; i++)
        {
            temp[i] = first[i];
        }
            

        for(int i = 0; i < second.Length; i++)
        {
            temp[first.Length + i] = second[i];
        }

        return temp;
    }

    static double GetNum(double[] arr, int idx)
    {
        return (idx < arr.Length) ? arr[idx] : 0;
    }

    static double DoMath(double a, double b, double c)
    {
        var top = 2 * Math.Sin(a) + 3 * b * Math.Pow(Math.Cos(c), 3);
        var bottom = a + b;
        
        if(Math.Abs(bottom) < 0.0000001) 
            return 0;
            
        return top / bottom;
    }

    static double AddBetweenPositives(double[] data)
    {
        int start = -1, end = -1;
        double total = 0;

        for(int i = 0; i < data.Length; i++)
        {
            if(data[i] > 0)
            {
                if(start == -1)
                {
                    start = i;
                    end = i;
                } 
            }
        }

        if(start == -1 || start == end)
        {
              return 0;
        }

        for(int i = start; i <= end; i++)
        {
             total += data[i];
        }

        return total;
    }
}