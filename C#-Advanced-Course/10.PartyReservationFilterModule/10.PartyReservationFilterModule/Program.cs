

Dictionary<string, Predicate<string>> filters = new();


List<string> people = Console.ReadLine()
   .Split(" ", StringSplitOptions.RemoveEmptyEntries)
   .ToList();

string commnad;

while ((commnad = Console.ReadLine()) != "Print")
{

    string[] tokens = commnad
        .Split(";", StringSplitOptions.RemoveEmptyEntries);

    string actions = tokens[0];
    string filter = tokens[1];
    string value = tokens[2];

    if (actions == "Add filter")
    {
        if (!filters.ContainsKey(filter + value))
        {
            filters.Add(filter + value, getPredicate(filter, value));
        }
    }
    else
    {
        filters.Remove(filter + value);
    }



};

foreach (var filt in filters)
{
    people.RemoveAll(filt.Value);
}

Console.WriteLine(string.Join(" ", people));

static Predicate<string> getPredicate(string filter, string value)
{
    switch (filter)
    {
        case "Starts with":
            return p => p.StartsWith(value);
        case "Ends with":
            return p => p.EndsWith(value);
        case "Length":
            return p => p.Length == int.Parse(value);
        case "Contains":
            return p => p.Contains(value);
        default:
            return default;
    }
}