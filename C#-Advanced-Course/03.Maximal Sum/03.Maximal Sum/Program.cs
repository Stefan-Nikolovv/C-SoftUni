int[] input = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

int rows = input[0];
int cols = input[1];

int[,] matrix = new int[rows, cols];

for (int row=0; row<rows; row++ )
{
    int[] array = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

    for (int col=0; col<cols; col++)
    {
        matrix[row, col] = array[col];
    }
}
int maxSum = int.MinValue;
int maxSumRow = 0;
int maxSumCol = 0;


for (int row = 0; row < rows -2; row++)
{
    
    for (int col = 0; col < cols -2;cols++)
    {
        int currentSum =
            matrix[row, col] + matrix[row, col + 1] + matrix[row, col + 2] +
            matrix[row + 1, col] + matrix[row + 1, col + 1] + matrix[row + 2, col + 2] +
            matrix[row + 2, col] + matrix[row + 2, col + 1] + matrix[row + 2, col + 2];

        if(currentSum > maxSum)
        {
            maxSum = currentSum;
            maxSumRow = row;
            maxSumCol = col;
        }
    }
}
Console.WriteLine($"Sum = {maxSumCol}");
for (int row = maxSumRow; row < maxSumRow+3;row++)
{
    
    for (int col = maxSumCol; col < maxSumCol+3;col++)
    {
        Console.Write($"{matrix[row, col]} ");
    }

    Console.WriteLine();
}