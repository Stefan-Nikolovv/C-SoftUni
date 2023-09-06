
SortedDictionary<char, int> words = new SortedDictionary<char, int>();

string input = Console.ReadLine();

foreach (char word in input)
{
    if (!words.ContainsKey(word))
    {
        words.Add(word, 0);
    }
    words[word]++;
}


foreach (var element in words )
{
    Console.WriteLine($"{element.Key}: {element.Value} time/s");
}