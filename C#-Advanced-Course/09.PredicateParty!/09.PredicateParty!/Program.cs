


List<string> people = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .ToList();

string commnad;
while((commnad = Console.ReadLine()) != "Party!")
{
    string[] tokens = commnad.Split(" ", StringSplitOptions.RemoveEmptyEntries);

    string actions = tokens[0];
    string filter = tokens[1];
    string value = tokens[2];

    if(actions == "Remove")
    {
        people.RemoveAll(getPredicate(filter, value));
    }
    else
    {
        List<string> peoples = people.FindAll(getPredicate(filter, value));

        foreach (var person in peoples) 
        
        {
            int index = peoples.FindIndex(p => p == person);
            people.Insert(index, person);

        }
    }
};

if (people.Any())
{
    Console.WriteLine($"{string.Join(", ", people)} are going to the party!");
}
else
{
    Console.WriteLine("Nobody is going to the party!");
}

static Predicate<string> getPredicate(string filter, string value)
{
    if(filter == "StartsWith")
    {
        return p => p.StartsWith(value);
    }else if(filter == "EndsWith")
    {
        return p => p.EndsWith(value);
    }
    else if (filter == "Length")
    {
        return p => p.Length == int.Parse(value);
    }
    return default(Predicate<string>);
};