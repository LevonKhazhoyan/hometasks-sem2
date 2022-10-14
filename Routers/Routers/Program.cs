using Routers;

if (args.Length != 2)
{
    Console.WriteLine("Incorrect input");
    Console.WriteLine("Input file with network graph of routers connections");
    Console.WriteLine("Input file to insert result");
}
else
{
    try
    {
        RoutersNetwork.MakeOptimalNetwork(args[0], args[1]);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("Network is not connected");
    }
}
