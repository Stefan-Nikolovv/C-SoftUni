int rows = int.Parse(Console.ReadLine());

int[][] jaggedArray = new int[rows][];

for (int row = 0; row < rows; row++)
{
    jaggedArray[row] = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
}

string command = Console.ReadLine().ToLower();

while (command != "end")
{
    string[] toknes = command.Split(' ');
    int row = int.Parse(toknes[1]);
    int col = int.Parse(toknes[2]);
    int value = int.Parse(toknes[3]);

    bool isValid = true;

    if(row < 0 || jaggedArray.Length <= 0)
    {
       isValid = false;
    }
    else
    {
        if (jaggedArray[row].Length <= col || col < 0)
        { 
            isValid = false;
        } 
    }
    if(isValid) {
        if (toknes[0] == "add")
        {
            jaggedArray[row][col] += value;
        }
        else
        {
            jaggedArray[row][col] -= value;
        }
    }
    else
    {
        Console.WriteLine("Invalid coordinates");
    }
    command = Console.ReadLine().ToLower();
} 

for (int row = 0; row < jaggedArray.Length; row++)
{
    for (int col = 0; col < jaggedArray[row].Length; col++)
    {
        Console.Write($"{jaggedArray[row][col]} ");
    }
    Console.WriteLine();    
}