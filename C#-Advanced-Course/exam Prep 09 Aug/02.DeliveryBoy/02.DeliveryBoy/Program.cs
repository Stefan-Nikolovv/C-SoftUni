int[] matrixSize = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int rows = matrixSize[0];
int cols = matrixSize[1];
string[,] matrix = new string[rows, cols];

int rowBoy = 0;
int colBoy = 0;
int rowStart = 0;
int colStart = 0;

for (int row = 0; row < rows; row++)
{
    string values = Console.ReadLine();



    for (int col = 0; col < cols; col++)
    {
        matrix[row, col] = values[col].ToString();

        if (values[col] == 'B')
        {
            rowBoy = row;
            colBoy = col;
            rowStart = row;
            colStart = col;
        }
    }
}

string command;

while ((command = Console.ReadLine()) != null)
{


    if (command == "down")
    {
        if (ValidateMove(rowBoy + 1, colBoy, matrix))
        {
           
            if (matrix[rowBoy + 1, colBoy] == "*")
            {
                continue;
            }
            if (matrix[rowBoy, colBoy] == "R")
            {
                rowBoy++;
            }
            else
            {
                matrix[rowBoy, colBoy] = ".";
                rowBoy++;
            }

            

            if (matrix[rowBoy, colBoy] == "-")
            {
                matrix[rowBoy, colBoy] = ".";
            }
            else if (matrix[rowBoy, colBoy] == "P")
            {
                matrix[rowBoy, colBoy] = "R";
                Console.WriteLine("Pizza is collected. 10 minutes for delivery.");
                continue;
            }
            else if (matrix[rowBoy, colBoy] == "A")
            {
                matrix[rowBoy, colBoy] = "P";
                Console.WriteLine("Pizza is delivered on time! Next order...");
                matrix[rowStart, colStart] = "B";
                break;
            }
        }
        else
        {
            Console.WriteLine("The delivery is late. Order is canceled.");
            matrix[rowStart, colStart] = " ";
            break;
        }
    }
    if (command == "left")
    {

        if (ValidateMove(rowBoy, colBoy - 1, matrix))
        {
            

            if (matrix[rowBoy, colBoy - 1] == "*")
            {

                continue;
            }
            if (matrix[rowBoy, colBoy] == "R")
            {
                colBoy--;
            }
            else
            {
                matrix[rowBoy, colBoy] = ".";
                colBoy--;
            }

            if (matrix[rowBoy, colBoy] == "-")
            {
                matrix[rowBoy, colBoy] = ".";
            }
            else if (matrix[rowBoy, colBoy] == "P")
            {
                matrix[rowBoy, colBoy] = "R";
                Console.WriteLine("Pizza is collected. 10 minutes for delivery.");
                continue;
            }
            else if (matrix[rowBoy, colBoy] == "A")
            {
                matrix[rowBoy, colBoy] = "P";
                Console.WriteLine("Pizza is delivered on time! Next order...");
                matrix[rowStart, colStart] = "B";
                break;
            }
        }
        else
        {
            Console.WriteLine("The delivery is late. Order is canceled.");
            matrix[rowStart, colStart] = " ";
            break;
        }

    }
    if (command == "right")
    {
        if (ValidateMove(rowBoy, colBoy + 1, matrix))
        {
           
            if (matrix[rowBoy, colBoy + 1] == "*")
            {

                continue;
            }
            if (matrix[rowBoy, colBoy] == "R")
            {
                colBoy++;
            }
            else
            {
                matrix[rowBoy, colBoy] = ".";
                colBoy++;
            }

            if (matrix[rowBoy, colBoy] == "-")
            {
                matrix[rowBoy, colBoy] = ".";
            }
            else if (matrix[rowBoy, colBoy] == "P")
            {
                matrix[rowBoy, colBoy] = "R";
                Console.WriteLine("Pizza is collected. 10 minutes for delivery.");
                continue;
            }
            else if (matrix[rowBoy, colBoy] == "A")
            {
                matrix[rowBoy, colBoy] = "P";
                Console.WriteLine("Pizza is delivered on time! Next order...");
                matrix[rowStart, colStart] = "B";
                break;
            }

        }
        else
        {
            Console.WriteLine("The delivery is late. Order is canceled.");
            matrix[rowStart, colStart] = " ";
            break;
        }
    }
    if (command == "up")
    {
        if (ValidateMove(rowBoy - 1, colBoy, matrix))
        {
           
            if (matrix[rowBoy - 1, colBoy] == "*")
            {
                continue;
            }
            if (matrix[rowBoy, colBoy] == "R")
            {
                rowBoy--;
            }
            else
            {
                matrix[rowBoy, colBoy] = ".";
                rowBoy--;
            }


            if (matrix[rowBoy, colBoy] == "-")
            {
                matrix[rowBoy, colBoy] = ".";
            }
            else if (matrix[rowBoy, colBoy] == "P")
            {
                matrix[rowBoy, colBoy] = "R";
                Console.WriteLine("Pizza is collected. 10 minutes for delivery.");
                continue;
            }
            else if (matrix[rowBoy, colBoy] == "A")
            {
                matrix[rowBoy, colBoy] = "P";
                Console.WriteLine("Pizza is delivered on time! Next order...");
                matrix[rowStart, colStart] = "B";
                break;
            }

        }
        else
        {
            Console.WriteLine("The delivery is late. Order is canceled.");
            matrix[rowStart, colStart] = " ";
            break;
        }
    }

}

Print2DArray(matrix);
static bool ValidateMove(int row, int col, string[,] matrix)
{
    bool result = row >= 0
        && row < matrix.GetLength(0)
        && col >= 0
        && col < matrix.GetLength(1);

    return result;
}
static void Print2DArray<T>(T[,] matrix)
{
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            Console.Write(matrix[i, j]);
        }
        Console.WriteLine();
    }
}
