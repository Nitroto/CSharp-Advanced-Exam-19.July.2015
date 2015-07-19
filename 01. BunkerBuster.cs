using System;
using System.Linq;
using System.Text;

class BunkerBuster
{
    static void Main()
    {
        int[] fieldDimensions = Console.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToArray();
        int[][] field = new int[fieldDimensions[0]][];
        FillField(field);
        string command = Console.ReadLine();
        while (command != "cease fire!")
        {
            DropBomb(command, field);
            command = Console.ReadLine();
        }
        int destroyedCells = CountDestroyedCells(field);
        int fieldCount = field.Length*field[0].Length;
        StringBuilder output = new StringBuilder();
        output.AppendLine(string.Format("Destroyed bunkers: {0}", destroyedCells));
        output.Append(string.Format("Damage done: {0:P1}",(destroyedCells/(double)fieldCount)));
        Console.WriteLine(output.ToString());
    }

    private static int CountDestroyedCells(int[][] field)
    {
        int counter = 0;
        for (int i = 0; i < field.Length; i++)
        {
            for (int j = 0; j < field[i].Length; j++)
            {
                if (field[i][j] <= 0)
                {
                    counter++;
                }
            }
        }
        return counter;
    }

private static void FillField(int[][] field)
{
    for (int i = 0; i < field.GetLength(0); i++)
    {
        field[i] = Console.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToArray();
    }
}

private static void DropBomb(string command, int[][] field)
{
    string[] bombParameters = command
        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
        .ToArray();
    int row = int.Parse(bombParameters[0]);
    int col = int.Parse(bombParameters[1]);
    int bombPower = char.Parse(bombParameters[2]);
    int bombHalfPower = (int)Math.Ceiling(bombPower / 2.0);
    field[row][col] -= bombPower;
    for (int i = row - 1; i <= row + 1; i++)
    {
        for (int j = col - 1; j <= col + 1; j++)
        {
            bool isValidCoordinate =
                (i >= 0 && i < field.Length)
                && (j >= 0 && j < field[row].Length)
                && (i != row || j != col);
            if (isValidCoordinate)
            {
                field[i][j] -= bombHalfPower;
            }
        }
    }
}
}
