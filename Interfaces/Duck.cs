class Duck : Bird, IFly, ISwim
{
    public Duck(string name) : base(name){}

    public int AirSpeed { get;  set;  } = 25;
    public int NauticalSpeed { get; set; } = 5;

    public void Fly()
    {
        Console.WriteLine($"{Name} flies away at {AirSpeed} mph");
    }

    public void Swim()
    {
        Console.WriteLine($"{Name} paddles away at {NauticalSpeed} mph");
    }
}