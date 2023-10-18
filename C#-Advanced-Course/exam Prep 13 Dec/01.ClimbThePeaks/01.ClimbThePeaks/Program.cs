Stack<int> foodPortion = new(Console.ReadLine()
    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse).ToArray());

Queue<int> stamina = new(Console.ReadLine()
    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse).ToArray());



Dictionary<string, int> peaks = new Dictionary<string, int>()
            {
                { "Vihren", 80},
                { "Kutelo", 90},
                { "Banski Suhodol", 100},
                { "Polezhan", 60},
                { "Kamenitza", 70}
            };

Queue<string> peaksNames = new Queue<string>();
foreach (var peak in peaks)
{
       peaksNames.Enqueue(peak.Key);
}

while (foodPortion.Any() && stamina.Any())
{
    
}