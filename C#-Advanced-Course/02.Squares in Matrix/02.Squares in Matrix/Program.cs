int[] input = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

int rows= input[0];
int cols= input[1];

string[,] matrix = new string[rows, cols];

for (int row = 0; row < rows; row++)
{
    string[] array = Console.ReadLine().Split(" ");

    for(int col = 0; col < cols; col++)
    {
        matrix[row,col]= array[col];
    }
}
int count = 0;
for (int row = 0; row < rows-1;row++)
{


    for (int col = 0; col < cols - 1; col++)
    {
        if (matrix[row, col] == matrix[row, col + 1]
            && matrix[row, col] == matrix[row + 1, col + 1]
            && matrix[row, col] == matrix[row + 1, col]
            )
        {
            count++;
        }

    }
}

Console.WriteLine(count);