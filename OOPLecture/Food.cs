public class Food
{
    // private string _name;
    // public string Name {get{return _name;} set{if (value.Length > 1) _name = value; }}

    public string Name { get;set; }
    private bool IsHot;
    private int Calories;
    private double Price;

    /// <summary> Creates a food with supplied values</summary>
    public Food(string name, bool isHot, int calories, double price)
    {
        Name = name;
        IsHot = isHot;
        Calories = calories;
        Price = price;
    }

    /// <summary> Creates a food with 200 calories</summary>
    public Food(string name, bool isHot, double price)
    {
        Name = name;
        IsHot = isHot;
        Calories = 200;
        Price = price;
    }

    /// <summary> Creates a free food</summary>
    public Food(string name, bool isHot, int calories)
    {
        Name = name + " -- FREE!";
        IsHot = isHot;
        Calories = calories;
        Price = 0;
    }

    /// <summary> Creates a generic food</summary>
    public Food()
    {
        Name = "Generic";
        IsHot = false;
        Calories = 0;
        Price = 0;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine(Name);
        Console.WriteLine($"IsHot: {IsHot}");
        Console.WriteLine($"Calories: {Calories}");
        Console.WriteLine($"Price: {Price}");
    }

    public virtual void DisplayInfo(int times)
    {
        for (int i = 0; i < times; i++)
        {
            DisplayInfo();
        }
    }


}