

using LinkListCustom;

CustomList customList = new CustomList();

customList.Add(1);
customList.Add(2);
customList.Add(3);
customList.Add(4);
customList.Add(5);
customList.Add(6);
customList.Add(7); 
customList.Add(8);

Console.WriteLine(customList.Contains(8));

Console.WriteLine(customList.Find(9));

Console.WriteLine(customList.Reverse());

Console.WriteLine(customList.toString());