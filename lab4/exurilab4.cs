int a = 3;
int b = 5;
ex1(ref a, ref b);
//Console.WriteLine("a = " + a + " b = " + b);
//Console.WriteLine(ex2(new Coords(1, 1), new Coords(3, 3)));
ex3();

static void ex1(ref int x, ref int y)
{
    int a = x;
    x = y;
    y = a;
}

static Coords ex2(Coords p1, Coords p2)
{
    return new Coords((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
}

static void ex3()
{
    Cont cont = new Cont("Ion Popescu", 500.0);

    Console.WriteLine($"Sold inițial: {cont.Sold} lei");

    // ✅ Caz 1: retragere reușită
    if (cont.Retragere(200, out double soldRamas1))
        Console.WriteLine($"Retragere reușită! Sold rămas: {soldRamas1} lei");
    else
        Console.WriteLine($"Fonduri insuficiente! Sold curent: {soldRamas1} lei");

    // ❌ Caz 2: retragere eșuată
    if (cont.Retragere(400, out double soldRamas2))
        Console.WriteLine($"Retragere reușită! Sold rămas: {soldRamas2} lei");
    else
        Console.WriteLine($"Fonduri insuficiente! Sold curent: {soldRamas2} lei");
}

public struct Coords
{
    public float X { get; set; }
    public float Y { get; set; }

    public Coords(float x, float y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}



class Cont
{
    private readonly string nume;
    private double sold;

    public Cont(string nume, double sold)
    {
        this.nume = nume;
        this.sold = sold;
    }

    public string Nume { get; set; }
    public double Sold { get; set; }

    public bool Retragere(double suma, out double soldRamas)
    {
        if (suma > sold)
        {
            soldRamas = sold;
            return false;
        }
        else
        {
            sold -= suma;
            soldRamas = sold;
            return true;
        }
    }
}