
using Stack;

CustomStack<int> items = new CustomStack<int>();

string command = string.Empty;

while((command = Console.ReadLine()) != "END")
{
    string[] tokens = command
       .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
    string action = tokens[0];

    if(action == "Push")
    {
        int[] itemsToPush = tokens
            .Skip(1)
            .Select(int.Parse).ToArray();

        foreach (var item in itemsToPush)
        {
            items.Push(item);
        }
    }
    else
    {
        try
        {
            items.Pop();
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}

foreach (var item in items)
{
    Console.WriteLine(item);
}

foreach (var item in items)
{
    Console.WriteLine(item);
}