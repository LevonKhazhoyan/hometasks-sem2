using InsertionSort;

Console.WriteLine("Insert array to sort");
try
{
    var input = Console.ReadLine();
    if (input != null)
    {
        var result = InsertionSortProj.InsertionSort(ConsoleArrayRead(input.Split(' ')));
        Console.WriteLine("There is your sorted array");
        Console.WriteLine($"[{string.Join(", ", result)}]");
    }
}
catch (FormatException e)
{
    Console.WriteLine("Failed! Example of input: -1 0 1 2");
}
catch (ArgumentException e)
{
    Console.WriteLine("Expected array of integers");
}

static int[] ConsoleArrayRead(string[] numbers)
{
    var arr = new int[numbers.Length];
    for (var i = 0; i < numbers.Length; i++)
    {
        arr[i] = int.Parse(numbers[i]);
    }
    return arr;
}
