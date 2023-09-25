Queue<int> monstersArmor = new(Console.ReadLine()
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

Stack<int> soldierPower = new(Console.ReadLine()
    .Split(",", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse));

int killedMonsters = 0;
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
   
}

Console.WriteLine($"Total monsters killed: {killedMonsters}");