using Lzw;

Console.WriteLine("Insert \"-c file path\" to compress\n\"-d file path\" to decompress");
Console.WriteLine($"File path can be global or leads in: {AppDomain.CurrentDomain.BaseDirectory}");
var input = Console.ReadLine()?.Split(" ");
try
{
    if (input?.Length == 2)
    {
        switch (input[0])
        {
            case "-c":
            {
                if (File.Exists(input[1] + ".zipped"))
                {
                    Console.WriteLine(input[1] + ".zipped file already exists. Would you like to delete existing file?\ny/n");
                    if (Console.ReadLine() == "y")
                    {
                        File.Delete(input[1] + ".zipped");
                    }
                    else
                    {
                        break;
                    }                    
                }
                LzwMethods.Compress(input[1]);
                var coefficient = (double)new FileInfo(input[1]).Length / new FileInfo(input[1] + ".zipped").Length;
                Console.WriteLine($"Compression coefficient: {Math.Round(coefficient, 5)}");
                break;
            }
            case "-d":
            {
                if (input[1].Length > 7 && input[1][(input[1].Length - 7)..] == ".zipped")
                {
                    if (File.Exists(input[1][..(input[1].Length - 7)]))
                    {
                        Console.WriteLine(input[1] + ".zipped file already exists. Would you like to delete existing file?\ny/n");
                        if (Console.ReadLine() == "y")
                        {
                            File.Delete(input[1][..(input[1].Length - 7)]);
                        }
                        else
                        {
                            break;
                        }
                    }
                    LzwMethods.Decompress(input[1]);
                    Console.WriteLine("File successfully decompressed");
                    return;
                }

                Console.WriteLine("Expected name of file ready for compression");
                break;
            }
            default:
            {
                Console.WriteLine("Incorrect transformation argument");
                break;
            }
        }
    }
    else
    {
        Console.WriteLine("Incorrect amount of arguments");
    }
}
catch (FileNotFoundException)
{
    Console.WriteLine($"There is no such file path as: \"{input?[1]}\"");
}
