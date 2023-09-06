



HashSet<string> numbers = new HashSet<string>();




string command = Console.ReadLine();

while (command != "END")
{
    string[] input = command.Split(", ");
    string direction = input[0];
    string carNumber = input[1];

    if(direction == "IN")
    {
        if (!numbers.Contains(carNumber))
        {
            numbers.Add(carNumber);
        }
    }
    if(direction == "OUT")
    {
        if (numbers.Contains(carNumber))
        {
            numbers.Remove(carNumber);
        }
    }

    command = Console.ReadLine();
}


if (numbers.Count <= 0) 
{
    Console.WriteLine("Parking Lot is Empty");

}else
{
    foreach (var car in numbers)
    {

        Console.WriteLine(car);


    }
}


