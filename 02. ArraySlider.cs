using System;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

class ArraySlider
{
    static void Main()
    {
        const string whitespace = (@"\s+");
        string userInput = Console.ReadLine();
        userInput = Regex.Replace(userInput, whitespace, " ");
        BigInteger[] input = userInput
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => BigInteger.Parse(x)).ToArray();
        string command = Console.ReadLine();
        int currentIndex = 0;
        while (command != "stop")
        {
            ExecuteCommand(command, input, ref currentIndex);
            command = Console.ReadLine();
        }

        StringBuilder output = new StringBuilder();
        output.Append(string.Format("[{0}]", string.Join(", ", input)));
        Console.WriteLine(output.ToString());
    }

    private static void ExecuteCommand(string command, BigInteger[] input, ref int index)
    {
        string[] commandParameters = command
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .ToArray();
        int offset = int.Parse(commandParameters[0]);
        int operand = int.Parse(commandParameters[2]);
        char operation = char.Parse(commandParameters[1]);
        index = CalculateIndex(index, offset, input.Length);
        ChooseOperation(index, operation, operand, input);
    }

    private static int CalculateIndex(int index, int offset, int length)
    {
        if ((index + offset) >= 0)
        {
            index = (index + offset) % length;
        }
        else
        {
            int tempIndex = ((index + offset) % length);
            index = length + tempIndex;
            index %= length;
        }
        return index;
    }

    private static void ChooseOperation(int index, char operation, int operand, BigInteger[] input)
    {
        switch (operation)
        {
            case '&':
                input[index] = input[index] & operand;
                break;
            case '|':
                input[index] = input[index] | (long)operand;
                break;
            case '^':
                input[index] = input[index] ^ operand;
                break;
            case '+':
                input[index] = input[index] + operand;
                break;
            case '-':
                input[index] = input[index] - operand;
                if (input[index] < 0)
                {
                    input[index] = 0;
                }
                break;
            case '*':
                input[index] = input[index] * operand;
                break;
            case '/':
                input[index] = input[index] / operand;
                break;
            default: throw new NotImplementedException();
        }
    }
}

