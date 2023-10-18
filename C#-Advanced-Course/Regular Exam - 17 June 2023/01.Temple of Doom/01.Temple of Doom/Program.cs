Queue<int> tools = new(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
Stack<int> substances = new(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
List<int> challenges = new List<int>(Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());



while (tools.Any() && substances.Any() && challenges.Any())
{
    int currentTool = tools.Dequeue();
    int currentSub = substances.Pop();
    int result = currentTool * currentSub;

    if (challenges.Contains(result))
    {
     
        challenges.Remove(result);

    }
    else 
    {
        if(currentSub - 1 > 0)
        {
            substances.Push(currentSub - 1);
        }
        tools.Enqueue(currentTool + 1);
    }
   
}

if(substances.Count == 0 && challenges.Count > 0)
{
    Console.WriteLine("Harry is lost in the temple. Oblivion awaits him.");
    Console.WriteLine($"Tools: {string.Join(", ", tools)}");
    Console.WriteLine($"Challenges: {string.Join(", ", challenges)}");

}




if (challenges.Count == 0)
{
    Console.WriteLine("Harry found an ostracon, which is dated to the 6th century BCE.");
    if(substances.Count == 0)
    {
        Console.WriteLine($"Tools: {string.Join(", ", tools)}");
    }
    if(tools.Count == 0)
    {
        Console.WriteLine($"Substances: {string.Join(", ", substances)}");
    }
    if(tools.Count > 0 && substances.Count > 0)
    {
        Console.WriteLine($"Tools: {string.Join(", ", tools)}");
        Console.WriteLine($"Substances: {string.Join(", ", substances)}");
    }
}