
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Food Apple = new Food("Apple",false,40,0.35);

Console.WriteLine(Apple.Name);
Apple.DisplayInfo();

Apple.Name = "Axcfsdf";
Apple.DisplayInfo();

Fruit Kiwi = new Fruit("Kiwi",false,40,.60,true);
Kiwi.DisplayInfo(5);

Food Pasta = new Food();