using System.Reflection.Metadata;

int dimention = int.Parse(Console.ReadLine());

string[,] matrix = new string[dimention, dimention];

int rowS1Start = 0;
int rowS2Start = 0;
int col1Start = 0;
int colS2Start = 0;
int moleRow = 0;
int moleCol = 0;
int molePoints = 0;
int countS = 0;
for (int row = 0; row < dimention; row++)
{
    string values = Console.ReadLine();
    for (int col = 0; col < values.Length; col++)
    {
    matrix[row, col] = values[col].ToString();
        if (values[col] == 'S')
        {
            
            if(countS > 0)
            {
                rowS2Start = row;
                colS2Start = col;
            }
            else
            {
                rowS1Start = row;
                col1Start = col;
                countS++;
            }
           

        }
        if (values[col] == 'M')
        {
            moleRow = row;
            moleCol = col;
        }

    }

}

string command;

while((command = Console.ReadLine()) != "End")
{
    if(molePoints >= 25)
    {
        break;
    }
    if(command == "left")
    {
        if(ValidateMove(moleRow, moleCol - 1, matrix))
        {
            if (matrix[moleRow, moleCol - 1] == "-")
            {
                matrix[moleRow, moleCol] = "-";
                matrix[moleRow, moleCol - 1] = "M";
                moleCol--;
                continue;
            }
            if (matrix[moleRow, moleCol - 1] == "S")
            {
                matrix[moleRow, moleCol] = "-";
                matrix[moleRow, moleCol - 1] = "-";
                if (molePoints > 0)
                {
                    molePoints -= 3;
                }
                if (moleRow == rowS1Start && (moleCol - 1) == col1Start)
                {
                    matrix[rowS2Start, colS2Start] = "M";
                    moleRow = rowS2Start;
                    moleCol = colS2Start;
                    continue;

                }
                else
                {

                    matrix[rowS1Start, col1Start] = "M";
                    moleRow = rowS1Start;
                    moleCol = col1Start;
                    continue;
                }
            }
           
            molePoints += int.Parse(matrix[moleRow, moleCol - 1]);
            matrix[moleRow, moleCol] = "-";
            matrix[moleRow, moleCol - 1] = "M";
            moleCol--;
        } 
        else
        {
            Console.WriteLine("Don't try to escape the playing field!");
            continue;
        }
    }
    if (command == "right")
    {
        if (ValidateMove(moleRow, moleCol + 1, matrix))
        {
            if (ValidateMove(moleRow, moleCol + 1, matrix))
            {
                if (matrix[moleRow, moleCol + 1] == "-")
                {
                    matrix[moleRow, moleCol] = "-";
                    matrix[moleRow, moleCol + 1] = "M";
                    moleCol++;
                    continue;
                }
                if (matrix[moleRow, moleCol + 1] == "S")
                {
                    matrix[moleRow, moleCol] = "-";
                    matrix[moleRow, moleCol + 1] = "-";
                    if (molePoints > 0)
                    {
                        molePoints -= 3;
                    }
                    if (moleRow == rowS1Start && (moleCol + 1) ==  col1Start)
                    {
                        matrix[rowS2Start, colS2Start] = "M";
                        moleRow = rowS2Start;
                        moleCol = colS2Start;
                        continue;
                    }
                    else
                    {
                        matrix[rowS1Start, col1Start] = "M";
                        moleRow = rowS1Start;
                        moleCol = col1Start;
                        continue;
                    }
                }
                
                molePoints += int.Parse(matrix[moleRow, moleCol + 1]);
                matrix[moleRow, moleCol] = "-";
                matrix[moleRow, moleCol + 1] = "M";
                moleCol++;
            }
        }
        else
        {
            Console.WriteLine("Don't try to escape the playing field!");
            continue;
        }
    }
    if (command == "up")
    {
        if (ValidateMove(moleRow - 1, moleCol , matrix))
        {
            if (matrix[moleRow - 1, moleCol] == "-")
            {
                matrix[moleRow, moleCol] = "-";
                matrix[moleRow - 1, moleCol] = "M";
                moleRow--;
                continue;
            }
            if (matrix[moleRow - 1, moleCol] == "S")
            {
                matrix[moleRow , moleCol] = "-";
                matrix[moleRow - 1, moleCol] = "-";
                if (molePoints > 0)
                {
                    molePoints -= 3;
                }
                if ((moleRow - 1) == rowS1Start && moleCol == col1Start)
                {
                    matrix[rowS2Start, colS2Start] = "M";
                    moleRow = rowS2Start;
                    moleCol = colS2Start;
                    continue;
                }
                else
                {
                    matrix[rowS1Start, col1Start] = "M";
                    moleRow = rowS1Start;
                    moleCol = col1Start;
                    continue;
                }
            }

            molePoints += int.Parse(matrix[moleRow - 1, moleCol]);
            matrix[moleRow, moleCol] = "-";
            matrix[moleRow - 1, moleCol] = "M";
            moleRow--;
        }
        else
        {
            Console.WriteLine("Don't try to escape the playing field!");
            continue;
        }
    }
    if (command == "down")
    {
        if (ValidateMove(moleRow + 1, moleCol, matrix))
        {
            if (matrix[moleRow + 1, moleCol] == "-")
            {
                matrix[moleRow, moleCol] = "-";
                matrix[moleRow + 1, moleCol] = "M";
                moleRow++;
                continue;
            }
            if (matrix[moleRow + 1, moleCol] == "S")
            {
                matrix[moleRow , moleCol] = "-";
                matrix[moleRow + 1, moleCol ] = "-";
                if (molePoints > 0)
                {
                    molePoints -= 3;
                }
                if ((moleRow + 1) == rowS1Start && moleCol == col1Start)
                {
                    matrix[rowS2Start, colS2Start] = "M";
                    moleRow = rowS2Start;
                    moleCol = colS2Start;
                    continue;
                }
                else
                {
                    matrix[rowS1Start, col1Start] = "M";
                    moleRow = rowS1Start;
                    moleCol = col1Start;
                    continue;
                }
            }

            molePoints += int.Parse(matrix[moleRow + 1, moleCol]);
            matrix[moleRow, moleCol] = "-";
            matrix[moleRow + 1, moleCol] = "M";
            moleRow++;
        }
        else
        {
            Console.WriteLine("Don't try to escape the playing field!");
            continue;
        }
    }
}

if(molePoints < 25)
{
    Console.WriteLine("Too bad! The Mole lost this battle!");
    Console.WriteLine($"The Mole managed to survive with a total of {molePoints} points.");
    Print2DArray(matrix);
}
else
{
    Console.WriteLine("Yay! The Mole survived another game!");
    Console.WriteLine($"The Mole managed to survive with a total of {molePoints} points.");
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