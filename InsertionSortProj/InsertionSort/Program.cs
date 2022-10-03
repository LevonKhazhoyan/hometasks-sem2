using InsertionSort;

Console.WriteLine("Insert array to sort");
try
{
    var input = Console.ReadLine();
    if (input != null)
    {
        var result = InsertionSortProj.InsertionSort(
            input.Split(' ').Select(n => Convert.ToInt32(n)).ToArray());
        Console.WriteLine("There is your sorted array");
        Console.WriteLine($"[{string.Join(", ", result)}]");
    }
}
catch (FormatException)
{
    Console.WriteLine("Failed! Example of input: -1 0 1 2");
}
catch (ArgumentException)
{
    Console.WriteLine("Expected array of integers");
}