Queue<int> monstersArmor = new(Console.ReadLine()
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

Stack<int> soldierPower = new(Console.ReadLine()
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

int killedMonsters = 0;



while (monstersArmor.Any() && soldierPower.Any())
{
    int currentSoldier = soldierPower.Pop();
    int currentArmor = monstersArmor.Dequeue();
    if (currentSoldier >= currentArmor)
    {
        killedMonsters++;
        var currentValue = currentSoldier - currentArmor;
        if (currentValue == 0)
        {
            continue;
        }

        if (soldierPower.Any())
        {
            int nextValue = soldierPower.Pop();
            soldierPower.Push(nextValue + currentValue);
        }
        else
        {
            soldierPower.Push(currentValue);
        }
    }
    else
    {
        var currentValue = currentArmor - currentSoldier;
        monstersArmor.Enqueue(currentValue);
    }


}

if (monstersArmor.Count == 0)
{

    Console.WriteLine("All monsters have been killed!");

}
if (soldierPower.Count == 0)
{
    Console.WriteLine("The soldier has been defeated.");

}

Console.WriteLine($"Total monsters killed: {killedMonsters}");