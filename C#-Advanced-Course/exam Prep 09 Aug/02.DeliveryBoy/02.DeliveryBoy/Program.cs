int[] matrixSize = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int rows = matrixSize[0];
int cols = matrixSize[1];
string[,] matrix = new string[rows, cols];

<<<<<<< HEAD
int rowBoy = -1;
int colBoy = -1;
int rowStart = -1;
int colStart = -1;
=======
int rowBoy = 0;
int colBoy = 0;
int rowStart = 0;
int colStart = 0;
>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5

for (int row = 0; row < rows; row++)
{
    string values = Console.ReadLine();
<<<<<<< HEAD
        
           
           
=======



>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
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

<<<<<<< HEAD
   if(command.ToLower() == "down")
    {
      if(ValidateMove(rowBoy + 1, colBoy, matrix))
        {
=======

    if (command == "down")
    {
        if (ValidateMove(rowBoy + 1, colBoy, matrix))
        {
           
>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
            if (matrix[rowBoy, colBoy] == "*")
            {
                continue;
            }
<<<<<<< HEAD
           
=======
>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
            if (matrix[rowBoy, colBoy] == "R")
            {
                rowBoy++;
            }
            else
            {
                matrix[rowBoy, colBoy] = ".";
                rowBoy++;
            }

<<<<<<< HEAD
           
            if (matrix[rowBoy, colBoy] == "-")
            {
                matrix[rowBoy, colBoy] = ".";
            }else if(matrix[rowBoy, colBoy] == "P")
=======
            

            if (matrix[rowBoy, colBoy] == "-")
            {
                matrix[rowBoy, colBoy] = ".";
            }
            else if (matrix[rowBoy, colBoy] == "P")
>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
            {
                matrix[rowBoy, colBoy] = "R";
                Console.WriteLine("Pizza is collected. 10 minutes for delivery.");
                continue;
<<<<<<< HEAD
            }else if(matrix[rowBoy, colBoy] == "A")
=======
            }
            else if (matrix[rowBoy, colBoy] == "A")
>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
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
<<<<<<< HEAD
   if(command.ToLower() == "left")
    {
    
          if(ValidateMove(rowBoy, colBoy - 1, matrix))
        {
=======
    if (command == "left")
    {

        if (ValidateMove(rowBoy, colBoy - 1, matrix))
        {
            

>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
            if (matrix[rowBoy, colBoy - 1] == "*")
            {

                continue;
            }
<<<<<<< HEAD
           
=======
>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
            if (matrix[rowBoy, colBoy] == "R")
            {
                colBoy--;
            }
            else
            {
                matrix[rowBoy, colBoy] = ".";
                colBoy--;
            }
<<<<<<< HEAD
            
=======

>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
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
<<<<<<< HEAD
    if(command.ToLower() == "right")
    {
        if(ValidateMove(rowBoy, colBoy + 1, matrix))
        {
=======
    if (command == "right")
    {
        if (ValidateMove(rowBoy, colBoy + 1, matrix))
        {
           
>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
            if (matrix[rowBoy, colBoy + 1] == "*")
            {

                continue;
            }
<<<<<<< HEAD
         
=======
>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
            if (matrix[rowBoy, colBoy] == "R")
            {
                colBoy++;
            }
            else
            {
                matrix[rowBoy, colBoy] = ".";
<<<<<<< HEAD
                colBoy++; 
            }
            
=======
                colBoy++;
            }

>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
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
<<<<<<< HEAD
            
=======

>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
        }
        else
        {
            Console.WriteLine("The delivery is late. Order is canceled.");
            matrix[rowStart, colStart] = " ";
            break;
        }
    }
<<<<<<< HEAD
    if (command.ToLower() == "up")
    {
        if (ValidateMove(rowBoy - 1, colBoy , matrix))
        {
            if (matrix[rowBoy - 1, colBoy] == "*")
            {

                continue;
            }
            if(matrix[rowBoy, colBoy] == "R")
=======
    if (command == "up")
    {
        if (ValidateMove(rowBoy - 1, colBoy, matrix))
        {
           
            if (matrix[rowBoy - 1, colBoy] == "*")
            {
                continue;
            }
            if (matrix[rowBoy, colBoy] == "R")
>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
            {
                rowBoy--;
            }
            else
            {
                matrix[rowBoy, colBoy] = ".";
                rowBoy--;
            }
<<<<<<< HEAD
            
            
=======


>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
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
<<<<<<< HEAD
Print2DArray(matrix);

=======

Print2DArray(matrix);
>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
static bool ValidateMove(int row, int col, string[,] matrix)
{
    bool result = row >= 0
        && row < matrix.GetLength(0)
        && col >= 0
        && col < matrix.GetLength(1);
<<<<<<< HEAD
   
=======

>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
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
<<<<<<< HEAD



///


namespace DeliveryBoy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[] dimentions = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            string[,] field = new string[dimentions[0], dimentions[1]];

            int boyRow = -1;
            int boyCol = -1;

            int startRow = -1;
            int startCol = -1;

            bool hasLeft = false;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                string newRow = Console.ReadLine();
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = newRow[j].ToString();

                    if (field[i, j] == "B")
                    {
                        boyRow = i;
                        boyCol = j;
                        startRow = i;
                        startCol = j;
                    }
                }
            }


            while (true)
            {
                string command = Console.ReadLine();

                if (command == "left")
                {
                    if (boyCol > 0)
                    {
                        if (field[boyRow, boyCol - 1] == "*")
                        {
                            continue;
                        }

                        if (field[boyRow, boyCol] != "R")
                        {
                            field[boyRow, boyCol] = ".";
                        }
                        boyCol--;
                    }
                    else
                    {
                        if (field[boyRow, boyCol] == "-")
                        {
                            field[boyRow, boyCol] = ".";
                        }
                        hasLeft = true;
                        Console.WriteLine("The delivery is late. Order is canceled.");
                        break;
                    }
                }

                if (command == "right")
                {
                    if (boyCol < field.GetLength(1) - 1)
                    {
                        if (field[boyRow, boyCol + 1] == "*")
                        {
                            continue;
                        }
                        if (field[boyRow, boyCol] != "R")
                        {
                            field[boyRow, boyCol] = ".";
                        }
                        boyCol++;
                    }
                    else
                    {
                        if (field[boyRow, boyCol] == "-")
                        {
                            field[boyRow, boyCol] = ".";
                        }
                        hasLeft = true;
                        Console.WriteLine("The delivery is late. Order is canceled.");
                        break;
                    }
                }

                if (command == "up")
                {
                    if (boyRow > 0)
                    {
                        if (field[boyRow - 1, boyCol] == "*")
                        {
                            continue;
                        }
                        if (field[boyRow, boyCol] != "R")
                        {
                            field[boyRow, boyCol] = ".";
                        }
                        boyRow--;
                    }
                    else
                    {
                        if (field[boyRow, boyCol] == "-")
                        {
                            field[boyRow, boyCol] = ".";
                        }
                        hasLeft = true;
                        Console.WriteLine("The delivery is late. Order is canceled.");
                        break;
                    }
                }

                if (command == "down")
                {
                    if (boyRow < field.GetLength(0) - 1)
                    {
                        if (field[boyRow + 1, boyCol] == "*")
                        {
                            continue;
                        }
                        if (field[boyRow, boyCol] != "R")
                        {
                            field[boyRow, boyCol] = ".";
                        }
                        boyRow++;
                    }
                    else
                    {
                        if (field[boyRow, boyCol] == "-")
                        {
                            field[boyRow, boyCol] = ".";
                        }
                        hasLeft = true;
                        Console.WriteLine("The delivery is late. Order is canceled.");
                        break;
                    }
                }

                if (field[boyRow, boyCol] == "P")
                {
                    Console.WriteLine("Pizza is collected. 10 minutes for delivery.");
                    field[boyRow, boyCol] = "R";
                    continue;
                }

                if (field[boyRow, boyCol] == "A")
                {
                    field[boyRow, boyCol] = "P";
                    Console.WriteLine("Pizza is delivered on time! Next order...");
                    break;
                }
            }

            if (hasLeft)
            {
                field[startRow, startCol] = " ";
            }
            else
            {
                field[startRow, startCol] = "B";
            }

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write(field[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
=======
>>>>>>> 6e4310d14efcc0605bd503c57887997fd952c3d5
