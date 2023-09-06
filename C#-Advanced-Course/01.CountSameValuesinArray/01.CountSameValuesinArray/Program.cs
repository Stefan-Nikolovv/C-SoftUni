List<double> list = Console.ReadLine().Split().Select(double.Parse).ToList();
Dictionary<double, int> keyValuePairs = new Dictionary<double, int>();

foreach (var pair in list)
{
    if (!keyValuePairs.ContainsKey(pair)){
        keyValuePairs.Add(pair, 0 );
    }
    keyValuePairs[pair]++;

}

foreach (var pair in keyValuePairs)
{
    Console.WriteLine($"{pair.Key} - {pair.Value} times");
}