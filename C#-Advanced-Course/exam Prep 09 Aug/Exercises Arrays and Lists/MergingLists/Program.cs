List <int> firstRow = Console.ReadLine().Split(" ").Select(int.Parse).ToList();
List <int> secondRow = Console.ReadLine().Split(" ").Select(int.Parse).ToList();

List <int> output = new List<int>();

int longerList = Math.Max(firstRow.Count, secondRow.Count);

for (int i = 0; i < longerList; i++)
{
    int currentNumber = firstRow[i];
    output.Add(currentNumber);
    for (int j = 0; j < secondRow.Count - 1; j++)
    {
        int currNumber = secondRow[j];
        output.Add(currNumber);
        
    }
}