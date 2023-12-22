int[] dimentions =  Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

int rows = dimentions[0];
int cols = dimentions[1];
string[,] matrix = new string[rows, cols];
int rowStart = 0;
int colStart = 0;
int mouseRow = 0;
int mouseCol = 0;
int count = 0;
for (int row = 0; row < rows; row++)
{
    string values = Console.ReadLine();
   
    for (int col = 0; col < cols; col++)
    {
        matrix[row, col] = values[col].ToString();

        if (values[col] == 'M')
        {
            rowStart = row;
            mouseRow= row;
            colStart = col;
            mouseCol = col;
        }
        if(values[col] == 'C')
        {
            count++;
        }
    }

}
string command;
while ((command = Console.ReadLine()) != "danger") 
{
    if(count == 0)
    {
        Console.WriteLine("Happy mouse! All the cheese is eaten, good night!");
        break;
    }
    if (command == "left")
    {
        if(ValidateMove(mouseRow, mouseCol - 1, matrix))
        {
            mouseCol--;
            if (matrix[mouseRow, mouseCol] == "*")
            {
                matrix[mouseRow, mouseCol + 1] = "*";
              
                matrix[mouseRow, mouseCol] = "M";
            }
            if (matrix[mouseRow, mouseCol] == "T")
            {
                
               
                matrix[mouseRow, mouseCol] = "M";
                Console.WriteLine("Mouse is trapped!");
                break;
            }
            if (matrix[mouseRow, mouseCol ] == "C")
            {
                count--;
                matrix[mouseRow, mouseCol + 1] = "*";
                matrix[mouseRow, mouseCol] = "M";
            }
            if(matrix[mouseRow, mouseCol] == "@")
            {
                matrix[mouseRow, mouseCol + 1] = "*";
                mouseCol++;
                continue;
            }

        }
        else
        {
            Console.WriteLine("No more cheese for tonight!");
            break;
        }
    }
    
    if(command == "right") 
    {
        if(ValidateMove(mouseRow, mouseCol + 1, matrix))
        {
            mouseCol++;
            if (matrix[mouseRow, mouseCol] == "*")
            {
                matrix[mouseRow, mouseCol] = "M";
                matrix[mouseRow, mouseCol - 1] = "*";
            }
            if(matrix[mouseRow, mouseCol] == "T")
            {
                matrix[mouseRow, mouseCol] = "M";
                
                Console.WriteLine("Mouse is trapped!");
                break;
            }
            if(matrix[mouseRow, mouseCol] == "C")
            {
                matrix[mouseRow, mouseCol] = "M";
                matrix[mouseRow, mouseCol - 1] = "*";
                count--;
            }
            if(matrix[mouseRow, mouseCol] == "@")
            {
                matrix[mouseRow, mouseCol - 1] = "*";
                mouseCol--;
                continue;
            }

        }
        else
        {
            Console.WriteLine("No more cheese for tonight!");
            break;
        }
    }
    

    if (command == "up")
    {
        if(ValidateMove(mouseRow - 1, mouseCol, matrix))
        {
            mouseRow--;
            if (matrix[mouseRow, mouseCol] == "*")
            {
                matrix[mouseRow, mouseCol] = "M";
                matrix[mouseRow + 1, mouseCol] = "*";
            }
            if (matrix[mouseRow, mouseCol] == "T")
            {
                matrix[mouseRow, mouseCol] = "M";
                
                Console.WriteLine("Mouse is trapped!");
                break;
            }
            if (matrix[mouseRow, mouseCol] == "C")
            {
                count--;
                matrix[mouseRow, mouseCol] = "M";
                matrix[mouseRow + 1, mouseCol] = "*";
            }
            if (matrix[mouseRow, mouseCol] == "@")
            {
                matrix[mouseRow + 1, mouseCol] = "*";
                mouseRow++;
                continue;
            }

        }
        else
        {
            Console.WriteLine("No more cheese for tonight!");
            break;
        }

    }
    
    if (command == "down")
    {
        if(ValidateMove(mouseRow + 1, mouseCol, matrix))
        {
            mouseRow++;
            if (matrix[mouseRow, mouseCol] == "*")
            {
                matrix[mouseRow, mouseCol] = "M";
                matrix[mouseRow - 1, mouseCol] = "*";
            }
            if (matrix[mouseRow, mouseCol] == "T")
            {
                matrix[mouseRow - 1, mouseCol] = "*";
                matrix[mouseRow, mouseCol] = "M";
                
                Console.WriteLine("Mouse is trapped!");
                break;
            }
            if (matrix[mouseRow, mouseCol] == "C")
            {
                count--;
                matrix[mouseRow, mouseCol] = "M";
                matrix[mouseRow - 1, mouseCol] = "*";
            }
            if (matrix[mouseRow, mouseCol] == "@")
            {
                matrix[mouseRow - 1, mouseCol] = "*";
                mouseRow--;
                continue;
            }

        }
        else
        {
            Console.WriteLine("No more cheese for tonight!");
            break;
        }
    }
    
}
if (command == "danger")
{
    Console.WriteLine("Mouse will come back later!");
    Print2DArray(matrix);
}
else
{
    Print2DArray(matrix);
}


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