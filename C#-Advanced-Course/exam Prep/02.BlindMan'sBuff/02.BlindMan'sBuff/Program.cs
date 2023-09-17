int[] matrixSize = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int rows = matrixSize[0];
int cols = matrixSize[1];

char[,] matrix = new char[rows, cols];
int currentPositionRow = 0;
int currentPositionColumn = 0;

for (int row = 0; row < rows; row++)
{
    char[] values = Console.ReadLine()
           .Split(" ", StringSplitOptions.RemoveEmptyEntries)
           .Select(char.Parse)
           .ToArray();
    for (
        int col = 0; col < cols; col++)
    {


        matrix[row, col] = values[col];

        if(matrix[row, col] == 'B')
        {
            currentPositionRow = row;
            currentPositionColumn = col;
        }
    }
}

int moveMades = 0;
int opponentsHitted = 0;

string command = string.Empty;

while ((command = Console.ReadLine()) != "Finish")
{
    if (opponentsHitted == 3)
    {
        break;
    };


    if(command == "up")
    {
        if(ValidateMove(currentPositionRow - 1, currentPositionColumn, matrix))
        {
            moveMades++;
            matrix[currentPositionRow, currentPositionColumn] = '-';
            if (matrix[currentPositionRow - 1, currentPositionColumn] == 'P')
            {
                opponentsHitted++;
            }
            currentPositionRow--;
            matrix[currentPositionRow, currentPositionColumn] = 'B';


        }
    } 
    else if (command == "down")
    {

        if (ValidateMove(currentPositionRow + 1, currentPositionColumn, matrix))
        {
            moveMades++;
            matrix[currentPositionRow, currentPositionColumn] = '-';
            if (matrix[currentPositionRow + 1, currentPositionColumn] == 'P')
            {
                opponentsHitted++;
            }
            currentPositionRow++;
            matrix[currentPositionRow, currentPositionColumn] = 'B';

        }
    }
    else if(command == "left")
    {
        if(ValidateMove(currentPositionRow, currentPositionColumn - 1, matrix))
        {
            moveMades++;
            matrix[currentPositionRow, currentPositionColumn] = '-';
            if (matrix[currentPositionRow, currentPositionColumn - 1] == 'P')
            {
                opponentsHitted++;
            }
            currentPositionColumn--;
            matrix[currentPositionRow, currentPositionColumn] = 'B';
            
        }
    }
    else if(command == "right")
    {
        if (ValidateMove(currentPositionRow, currentPositionColumn + 1, matrix))
        {
            moveMades++;
            matrix[currentPositionRow, currentPositionColumn] = '-';
            if (matrix[currentPositionRow, currentPositionColumn + 1] == 'P')
            {
                opponentsHitted++;
            }
            currentPositionColumn++;
            matrix[currentPositionRow, currentPositionColumn] = 'B';

        }

    }
}
Console.WriteLine("Game over!");
Console.WriteLine($"Touched opponents: {opponentsHitted} Moves made: {moveMades}");


static bool ValidateMove(int row, int col, char[,] matrix)
{
    bool result = row >= 0
        && row < matrix.GetLength(0)
        && col >= 0
        && col < matrix.GetLength(1)
        && matrix[row, col] != 'O';

    return result;
}

