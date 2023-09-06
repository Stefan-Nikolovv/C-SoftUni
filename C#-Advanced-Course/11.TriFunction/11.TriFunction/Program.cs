

Func<string, int, bool> checkSum = (name, sum) => name.Sum(ch => ch) >= sum;

Func<string[], int, Func<string, int, bool>, string> matchedName = 
    (names, sum, match) => names.FirstOrDefault(name => match(name, sum));

int sum =  int.Parse(Console.ReadLine());

string[] names = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

Console.WriteLine(matchedName(names, sum, checkSum));