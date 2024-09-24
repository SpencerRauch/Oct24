// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
// Bird birb = new Bird(); // this won't work, can't instantiate abstract classes


Hawk RedTail = new("Redtail");
Ostrich Oscar = new("Oscar");
Duck Daffy = new("Daffy");

List<Bird> AllBirds = [RedTail, Oscar, Daffy];
List<IFly> FlyingBirds = [RedTail, Daffy];

foreach (Bird bird in AllBirds)
{
    if (bird is IFly f)
    {
        f.Fly();
    }
    else if (bird is Duck d)
    {
        d.Swim();
    }
    else if (bird is IRun r)
    {
        r.Run();
    }
}
// RedTail.Fly();