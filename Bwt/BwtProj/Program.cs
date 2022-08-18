using BWT;

Console.WriteLine("Input \"BWT\" to transform string or \"Inverse\" ");
switch (Console.ReadLine())
{
    case "BWT":
    {
        Console.WriteLine("Input string");
        try
        {
            var input = Console.ReadLine();
            if (input != null)
            {
                var result = Bwt.BwTransformation(input);
                Console.WriteLine(new string(result.Item1));
                Console.WriteLine(result.Item2);
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Failed! Example of input: banana");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Expected String");
        }
        break;
    }
    case "Inverse":
    {
        Console.WriteLine("Input BWT string");
        var input = Console.ReadLine()?.ToCharArray();
        Console.WriteLine("Input index in sorted shift array of BWT string");
        var num = Console.ReadLine();
        if (input != null && num != null)
        {
            Console.WriteLine(new string(Bwt.InverseTransform(new Tuple<char[], int>(input, Int32.Parse(num)))));
        }
        break;
    }
    default:
    {
        Console.WriteLine("Incorrect input");
        break;
    }
}