


using Collection;


List<string> items = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Skip(1)
    .ToList();

ListyIterator<string> listyIterator = new(items);

string command;

while ((command = Console.ReadLine()) != "END")
{
    if (command == "Move")
    {
        Console.WriteLine(listyIterator.Move());
    }
    else if (command == "HasNext")
    {
        Console.WriteLine(listyIterator.HasNext());
    }
    else if (command == "Print")
    {
        try
        {
            listyIterator.Print();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
    }else if(command == "PrintAll")
    {
        foreach (var item in items)
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
    }
}