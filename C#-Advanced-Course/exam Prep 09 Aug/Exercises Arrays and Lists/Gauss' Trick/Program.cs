List<int> input = Console.ReadLine().Split(" ").Select(int.Parse).ToList();
List <int> output = new List<int>();

for (int i = 0; i < input.Count / 2; i++)
{
    int sum = input[0 + i] + input[input.Count - 1 - i]; 
    output.Add(sum);
}
if (input.Count % 2 != 0)
{
    output.Add(input[input.Count / 2]);
}
Console.WriteLine(string.Join(" ", output));
