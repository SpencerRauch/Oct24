class Fruit : Food
{
    private bool IsRipe;

    public Fruit(string name, bool isHot, int calories, double price, bool isRipe):base(name, isHot, calories, price)
    {
        IsRipe = isRipe;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"IsRipe: {IsRipe}");
    }
}