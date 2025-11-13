namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        List<int> lista = new List<int>();
        for(int i=0; i<10; i++)
        {
            lista.Add(i);
        }
        Console.WriteLine("Lista originala: ");
        afisareLista(lista);

        List<int> rez = new List<int>();
        rez = Filtru(cond, lista);
        afisareLista(rez);
        
        Console.WriteLine(FGX(f, g, 3));
        
        Func<int, int> y = z => z * 2;
        Func<int, int> h = z => z - 1;

        var name = Comp(y, h);
        Console.WriteLine(name(4));
    }

    static void afisareLista(List<int> lista)
    {
        foreach (int n in lista)
        {
            Console.Write(n + " ");
        }Console.WriteLine("");
    }

    static bool cond(int x)
    {
        return x % 2 == 0;
    }
    
    public static float f(float x)
    {
        return x * 3;
    }

    public static float g(float x)
    {
        return x - 2;
    }
    
    public static List<int> Filtru(Delegate condiție, List<int> lista)
    {
        List<int> rez = new List<int>();
        for (int i = 0; i < lista.Count; i++)
        {
            if (cond(lista[i]))
            {
                rez.Add(lista[i]);
            }
        }
        return rez;
    }

    public static float FGX(Delegate del1, Delegate del2, float x)
    {
        var f1 = (Func<float, float>)del1;
        var f2 = (Func<float, float>)del2;
        return f1(f2(x));
    }
    
    public static Func<int, int> Comp(Func<int, int> f, Func<int, int> g)
    {
        return x => f(g(x));
    }
}