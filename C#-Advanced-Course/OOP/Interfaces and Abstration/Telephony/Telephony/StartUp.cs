

using Telephony.Models;


    string[] phoneNumbers = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

    string[] urls = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

    ICall phone;
    
    foreach (string phoneNumber in phoneNumbers)
    {
        if (phoneNumber.Length == 10)
        {
            phone = new Smartphone();
        }
        else
        {
            phone = new StationaryPhone();

        }
    try
    {
        Console.WriteLine(phone.Call(phoneNumber)); 

    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
    }
}
IBrowse browse = new Smartphone();

    foreach (string url in urls)
    {
    try
    {
        Console.WriteLine(browse.Browse(url));


    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
    }
}

    


