SortedSet<string> elements =new SortedSet<string> ();

int n = int.Parse(Console.ReadLine());

for (int i = 0; i < n; i++)
{
    string[] element = Console.ReadLine().Split(" ");


    elements.UnionWith(element);
}

Console.WriteLine(string.Join (" ", elements));