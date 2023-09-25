Queue<int> monstersArmor = new(Console.ReadLine()
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

Stack<int> soldierPower = new(Console.ReadLine()
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

int killedMonsters = 0;
<<<<<<< HEAD
int currentValue = 0;


while (monstersArmor.Count > 0 && soldierPower.Count > 0)
{
    int soldier = soldierPower.Peek();
    
    int armor = monstersArmor.Peek();
    if (soldier >= armor)
    {
        killedMonsters++;
        int currentSoldier = soldierPower.Pop();
        int currentArmor = monstersArmor.Dequeue();
        currentValue = currentSoldier - currentArmor;
        if(!monstersArmor.Any())
        {
            break;
        } 
        if(currentValue == 0)
        {
            continue;
        }
        int nextValue = soldierPower.Pop();
        soldierPower.Push(nextValue + currentValue);
       
        
    }
    else
    {
        int currentSoldier = soldierPower.Pop();
        int currentArmor = monstersArmor.Dequeue();
        
        currentValue = currentArmor - currentSoldier;
        monstersArmor.Enqueue(currentValue);
    }
    

}

if (soldierPower.Any())
{
    
    Console.WriteLine("All monsters have been killed!");
   
}
if(monstersArmor.Any())
{
    Console.WriteLine("The soldier has been defeated.");
   
=======



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

>>>>>>> 3171d7f035fd03c2a063b99358ee5b22fb937138
}

Console.WriteLine($"Total monsters killed: {killedMonsters}");