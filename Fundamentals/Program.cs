// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int Age = 36;

string Name = "Spencer";

char Character = 'A';

// Console.WriteLine($"My name is {Name} and I am {Age} years old. Char is {Character}");

if (Age >= 30)
{
    // Console.WriteLine("I'm older than 30!");
}

//PascalCase starts capitalized and every word is capitalized

//arrays are of fixed size in C#
//and of a uniform type
//zero indexed
string[] Strings = new string[3];
Strings[0] = "Bob";
Strings[1] = "Alice";
Strings[2] = "Alex";

int[] Integers = new int[4];

int[] SuppliedInts = [1,2,3,4];

Console.WriteLine(Strings[1]);
Console.WriteLine(Integers[1]);


//use this loop when you need to alter the array / list 
for (int i = 0; i < Strings.Length; i++)
{
    Strings[i] = "Steve";
    Console.WriteLine($"{i} - {Strings[i]}");
    
}

//use this one if you only need to read the values 
foreach (string name in Strings)
{
    Console.WriteLine(name);
    
}


List<string> Names = new List<string>();
Names.Add("Bob");
Names.Add("Alice");
Names.Add("Aex");

Names.ForEach(Console.WriteLine);

Dictionary<string,int> PetAges = new()
{
    {"Wiley",5},
    {"Honey",5}
};

// PetAges.Remove("Wiley");

foreach (KeyValuePair<string,int> entry in PetAges)
{
    Console.WriteLine($"{entry.Key} - {entry.Value}");
    
}



