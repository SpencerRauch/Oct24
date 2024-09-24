class Ostrich : Bird, IRun
{
    public Ostrich(string name) : base(name){}

    public int LandSpeed { get ; set ; } = 40;

    public void Run()
    {
        Console.WriteLine($"{Name} running off at {LandSpeed} mph");
    }
}