Queue<int> monsterArmor = new Queue<int>(Console.ReadLine()
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

Stack<int> soldierPower = new Stack<int>(Console.ReadLine()
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

int difference = 0;
int killedMonsters = 0;
int leftMonster = 0;
while (monsterArmor.Count > 0 && soldierPower.Count > 0)
{
    int power = soldierPower.Pop();
    int armor = monsterArmor.Dequeue();

    if (power >= armor)
    {
        difference = power - armor;
        killedMonsters++;
        int nextValue = soldierPower.Pop();
        soldierPower.Push(nextValue + difference);
        if (leftMonster > 0)
        {
            leftMonster--;
        }
    }
    else
    {
        leftMonster++;
        monsterArmor.Enqueue(armor);
    }
}

if (soldierPower.Count() >= 1 && monsterArmor.Count() == 0)
{
    Console.WriteLine("All monsters have been killed!");
    Console.WriteLine($"Total monsters killed: {killedMonsters}");
}

if (soldierPower.Count() == 0 && monsterArmor.Count() >= 1)
{
    Console.WriteLine("The soldier has been defeated.");
    Console.WriteLine($"Total monsters killed: {killedMonsters}");
}

if (soldierPower.Count() == 0 && monsterArmor.Count() == 0)
{
    if (leftMonster > killedMonsters)
    {
        Console.WriteLine("The soldier has been defeated.");
        Console.WriteLine($"Total monsters killed: {killedMonsters}");
    }
}

