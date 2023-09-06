
Dictionary<string, Dictionary<string,double>> shops = new Dictionary<string, Dictionary<string,double>>();

string command = Console.ReadLine();

while (command != "Revision")
{
    string[] input = command.Split(", ");
    string magazine = input[0];
    string product = input[1];
    double price = double.Parse(input[2]);

    if(!shops.ContainsKey(magazine))
    {
        shops.Add(magazine, new Dictionary<string, double> ());
    }
    if (shops[magazine].ContainsKey(product))
    {
        shops[magazine].Add(product, price);
    }

    shops[magazine][product] = price;

    command = Console.ReadLine();

}
shops = shops.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);   
foreach (var (magazine, products) in shops)
{
    Console.WriteLine($"{magazine}->");
    foreach (var (product, price) in products)
    {
        Console.WriteLine($"Product: {product}, Price: {price}");

    }
}