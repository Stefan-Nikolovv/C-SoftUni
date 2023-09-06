

Action<string[], Predicate<string>> printNames = (givenNames, match) =>
{
    foreach (var givenName in givenNames)
    {
        if (match(givenName))
        {
            Console.WriteLine(givenName);
        }
    }
};



int lengthOfName = int.Parse(Console.ReadLine());

string[] givenNames = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .ToArray();

Predicate<string> validNames = givenNames => givenNames.Length <= lengthOfName;

printNames(givenNames, validNames);