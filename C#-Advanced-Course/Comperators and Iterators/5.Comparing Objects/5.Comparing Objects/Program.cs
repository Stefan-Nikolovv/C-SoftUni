

using ComparingObjects;

List<Person> people = new();

string commmad = string.Empty;

while((commmad = Console.ReadLine()) != "END")
{
    string[] tokens = commmad
        .Split(" ", StringSplitOptions.RemoveEmptyEntries);

    string name = tokens[0];
    int age = int.Parse(tokens[1]);
    string town = tokens[2];

    Person person = new Person()
    {
        Name = name,
        Age = age,
        Town = town
    };
    people.Add(person);
}

int index = int.Parse(Console.ReadLine()) - 1;

Person personToCompare = people[index];

int equals = 0;
int diff = 0;

foreach(Person person in people)
{
    if(person.CompareTo(personToCompare) == 0)
    {
        equals++;
    }
    else
    {
        diff++;
    }

}

if(equals == 1)
{
    Console.WriteLine("No matches");
}
else
{
    Console.WriteLine($"{equals} {diff} {people.Count}");
}