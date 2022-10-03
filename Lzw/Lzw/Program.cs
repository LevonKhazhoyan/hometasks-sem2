using Lzw;

try
{
    if (args?.Length == 2)
    {
        switch (args[0])
        {
            case "-c":
            {
                if (File.Exists(args[1] + ".zipped"))
                {
                    Console.WriteLine(args[1] + ".zipped file already exists. Would you like to delete existing file?\ny/n");
                    if (Console.ReadLine() == "y")
                    {
                        File.Delete(args[1] + ".zipped");
                    }
                    else
                    {
                        break;
                    }                    
                }
                else
                {
                    Console.WriteLine($"File path can be global or leads in: {AppDomain.CurrentDomain.BaseDirectory}");
                }
                LzwMethods.Compress(args[1]);
                var coefficient = (double)new FileInfo(args[1]).Length / new FileInfo(args[1] + ".zipped").Length;
                Console.WriteLine($"Compression coefficient: {Math.Round(coefficient, 5)}");
                break;
            }
            case "-d":
            {
                if (args[1].Length > 7 && args[1][(args[1].Length - 7)..] == ".zipped")
                {
                    if (File.Exists(args[1][..(args[1].Length - 7)]))
                    {
                        Console.WriteLine(args[1] + ".unzipped file already exists. Would you like to delete existing file?\ny/n");
                        if (Console.ReadLine() == "y")
                        {
                            File.Delete(args[1][..(args[1].Length - 7)]);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"File path can be global or leads in: {AppDomain.CurrentDomain.BaseDirectory}");
                    }
                    LzwMethods.Decompress(args[1]);
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
    Console.WriteLine($"There is no such file path as: \"{args?[1]}\"");
}
