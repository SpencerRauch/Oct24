class Hawk : Bird, IFly
{
    public Hawk(string name) : base(name){}

    public int AirSpeed { get;  set;  } = 120;

    public void Fly()
    {
        Console.WriteLine($"{Name} flies away at {AirSpeed} mph");
    }
}