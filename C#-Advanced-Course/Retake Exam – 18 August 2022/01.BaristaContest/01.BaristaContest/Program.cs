Queue<int> coffee = new(Console.ReadLine()
    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse).ToArray());

Stack<int> milk = new(Console.ReadLine()
    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse).ToArray());

Dictionary<string, int> kafePairs = new Dictionary<string, int>();


while (coffee.Any() && milk.Any()) 
{
    int currentCof = coffee.Dequeue();
    int currentMilk = milk.Pop();
    int result = currentCof + currentMilk;
    
    if (result == 50)
    {
        if (!kafePairs.ContainsKey("Cortado"))
        {
            kafePairs.Add("Cortado", 1);
        }
        else
        {
            kafePairs["Cortado"] += 1;
        }
    }
    else if (result == 75) 
    {
        if (!kafePairs.ContainsKey("Espresso"))
        {
            kafePairs.Add("Espresso", 1);
        }
        else
        {
            kafePairs["Espresso"] += 1;
        }
    }
    else if (result == 100) 
    {
        if (!kafePairs.ContainsKey("Capuccino"))
        {
            kafePairs.Add("Capuccino", 1);
        }
        else
        {
            kafePairs["Capuccino"] += 1;
        }
    }
    else if (result == 150)
    {
        if (!kafePairs.ContainsKey("Americano"))
        {
            kafePairs.Add("Americano", 1);
        }
        else
        {
            kafePairs["Americano"] += 1;
        }
    }
    else if (result == 200)
    {
        if (!kafePairs.ContainsKey("Latte"))
        {
            kafePairs.Add("Latte", 1);
        }
        else
        {
            kafePairs["Latte"] += 1;
        }
    }else
    {
        milk.Push(currentMilk - 5);
    }

}

if(coffee.Count == 0 && milk.Count == 0)
{
    Console.WriteLine("Nina is going to win! She used all the coffee and milk!");
    Console.WriteLine("Coffee left: none");
    Console.WriteLine("Milk left: none");
    foreach (var kafePair in kafePairs.OrderBy(x => x.Value).ThenByDescending(x => x.Key))
    {
        Console.WriteLine($"{kafePair.Key}: {kafePair.Value}");

    };
}

if(coffee.Count > 0 && milk.Count == 0)
{
    Console.WriteLine("Nina needs to exercise more! She didn't use all the coffee and milk!");
    Console.WriteLine($"Coffee left: {string.Join("", coffee)}");
    Console.WriteLine("Milk left: none");
    foreach (var kafePair in kafePairs.OrderBy(x => x.Value).ThenByDescending(x => x.Key))
    {
        Console.WriteLine($"{kafePair.Key}: {kafePair.Value}");

    };
}

if (coffee.Count == 0 && milk.Count > 0)
{
    Console.WriteLine("Nina needs to exercise more! She didn't use all the coffee and milk!");
    Console.WriteLine($"Coffee left: none");
    Console.WriteLine($"Milk left: {string.Join("", milk)}");
    foreach (var kafePair in kafePairs.OrderBy(x => x.Value).ThenByDescending(x => x.Key))
    {
        Console.WriteLine($"{kafePair.Key}: {kafePair.Value}");
        
    };
}