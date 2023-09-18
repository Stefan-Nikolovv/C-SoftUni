using System.Net.Cache;
using System.Text;

namespace SoftUniKindergarten;

public class Child
{
    public Child(string firstName, string lastName, int age, string parantName, string contactNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        ParentName = parantName;
        ContactNumber = contactNumber;
    }

    public override string ToString()
    {
        return $"Child: {FirstName} {LastName}, Age: {Age}, Contact info: {ParentName} - {ContactNumber}";

    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string ParentName { get; set; }
    public string ContactNumber { get; set; }

    
}
