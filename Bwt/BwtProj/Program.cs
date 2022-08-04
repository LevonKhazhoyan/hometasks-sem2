using BWT;

Console.WriteLine("Input \"BWT\" to transform string or \"Inverse\" ");
switch (Console.ReadLine())
{
    case "BWT":
    {
        Console.WriteLine("Input string with '$' symbol at the end");
        try
        {
            var bwt = Console.ReadLine();
            if (bwt != null)
            {
                var result = BwtProj.BwTransformation(bwt);
                Console.WriteLine(new string(result.Item1));
                Console.WriteLine(result.Item2);
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Failed! Example of input: banana$");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Expected String");
        }
        break;
    }
    case "Inverse":
    {
        Console.WriteLine("Input BWT string");
        var bwt = Console.ReadLine()?.ToCharArray();
        Console.WriteLine("Input index in sorted shift array of BWT string");
        var num = Console.ReadLine();
        if (bwt != null && num != null)
        {
            Console.WriteLine(new string(BwtProj.InverseTransform(new Tuple<char[], int>(bwt, Int32.Parse(num)))));
        }
        break;
    }
    default:
    {
        Console.WriteLine("Incorrect input");
        break;
    }
}