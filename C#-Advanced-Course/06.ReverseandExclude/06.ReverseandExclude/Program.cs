



Func<List<int>, List<int>> reversedNumber = numbers =>
{
    List<int> result = new List<int>();

    for (int i = numbers.Count - 1; i >= 0; i--)
    {
        result.Add(numbers[i]);
    }
    return result;
};

Func<List<int>, Predicate<int>, List<int>> exludeNumbers = (numbers, match) =>
{
    List<int> result = new List<int>();

    foreach (int number in numbers)
    {
        if (!match(number))
        {
            result.Add(number);

        }
    }
    return result;
};


List<int> numbers = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToList();

int dividfer = int.Parse(Console.ReadLine());


numbers = exludeNumbers(numbers, n => n % dividfer == 0);
numbers = reversedNumber(numbers);

 Console.WriteLine(string.Join(" ", numbers));

