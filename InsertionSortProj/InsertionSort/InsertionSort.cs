namespace InsertionSortProj
{
    public class InsertionSort
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Insert array to sort");
            try
            {
                var res = ConsoleArrayRead();
                Sort(res);
                Console.WriteLine("There is your sorted array");
                Console.WriteLine("[{0}]", string.Join(", ", res));
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case FormatException e:
                        Console.WriteLine("Failed! Example of input: -1 0 1 2");
                        break;
                    case ArgumentException e:
                        Console.WriteLine("Expected array of integers");
                        break;
                    default:
                        Console.WriteLine("Unknown exception");
                        break;
                }
            }
        }

        public static void Sort(int[] arr)
        {
            for (int sortedLen = 0; sortedLen < arr.Length; sortedLen++)
            {
                for (int k = 0; k < sortedLen; k++)
                {
                    int i = sortedLen - k;
                    if (arr[i] < arr[i - 1])
                    {
                        (arr[i], arr[i-1]) = (arr[i-1], arr[i]);
                    } else
                    {
                        break;
                    }
                }
            }
        }
      
        private static int[] ConsoleArrayRead()
        {
            var numbers = Console.ReadLine().Split(' ');
            var arr = new int[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                arr[i] = int.Parse(numbers[i]);
            }
            return arr;
        }
    }
}
