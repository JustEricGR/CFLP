namespace Test;

// Student 6
//
// 1. Implementati o functie Oscar21() care primeste 2 numere ulong si le inverseaza daca ambele sunt mai mari decat 69. (2p)
//
// 2. Implementati o functie Juliet00() care primeste o lista de ulong si doua functii.
// Prima functie reprezinta o conditie, adica primeste un ulong si returneaza un boolean, iar a doua functie primeste un ulong si returneaza un ulong.
// Juliet00() va returna o noua lista in care toate elementele din lista initiala care indeplinesc conditia primita ca parametru vor fi transformate in alta valoare de a doua functie primita ca parametru. (2p)
//
// 3.Creati urmatoarea ierarhie de clase:
// O clasa abstracta Cetatean, care contine: (1p)
// - proprietatile: nume prenume data nasterii serie_ci (public get, protected set)
// - un constructor pentru initializarea proprietatilor
// - o metoda abstracta void Salut()
// - o metoda ToString() care sa returneze un string valorile concatenate ale membrilor
// O clasa Cetatean_roman, care mosteneste Cetatean si contine: (1p)
// - proprietatile: stie_alta_limba (boolean) (public get, protected set)
// - un constructor pentru initializarea membrilor
// - o metoda Salut() care sa afiseze 'Salut, sunt Cetatean_roman'
// - o metoda ToString() care sa returneze un string valorile concatenate ale membrilor
// O clasa Cetatean_de_etnie, care mosteneste Cetatean si contine: (1p)
// - proprietatile: limba_materna (public get, protected set)
// - un constructor pentru initializarea membrilor
// - o metoda Salut() care sa afiseze 'Salut, sunt Cetatean_de_etnie'
// - o metoda ToString() care sa returneze un string valorile concatenate ale membrilor
// In programul principal, creati o colectie de tip Cetatean si adaugati cel putin 4 obiecte.(1p)
// Parcurgeti colectia si apelati metodele Salut(). (0.5p)
// Parcurgeti colectia si afisati reprezentarile ToString() ale obiectelor.(0.5p)

class Program
{
    static void Main(string[] args)
    {
        ulong x = 80;
        ulong y = 90;
        Console.WriteLine("x=" + x);
        Console.WriteLine("y=" + y);
        
        
        Oscar21(ref x, ref y);
        Console.WriteLine("x=" + x);
        Console.WriteLine("y=" + y);

        List<ulong> testList = new List<ulong>();
        for (int i = 0; i < 11; i++)
        {
            testList.Add((ulong)i);
        }
        
        afisareLista(testList);
        
        afisareLista(Juliet00(testList, conditie, transformare));

        List<Cetatean> oameni = new List<Cetatean>()
        {
            new Cetatean_roman("Popescu", "Ion", "01/02/2004", "AB123456", true),
            new Cetatean_roman("Ionescu", "Maria", "12/02/2010", "CJ987654", false),
            new Cetatean_de_etnie("Kovacs", "Bela", "31/01/2004", "MS555444", "Maghiara"),
            new Cetatean_de_etnie("Said", "Omar", "20/10/2001", "B123789", "Araba")
        };

        Console.WriteLine("=== Salut() ===");
        
        foreach (var c in oameni)
        {
            c.Salut();
        }

        Console.WriteLine("\n=== ToString() ===");
        foreach (var c in oameni)
        {
            Console.WriteLine(c.ToString());
        }
        

    }

    public static void Oscar21(ref ulong x, ref ulong y)
    {
        if (x > 69 && y > 69)
        {
            ulong temp = x;
            x = y;
            y = temp;
        }
    }

    public static List<ulong> Juliet00(List<ulong> list, Func<ulong, bool> cond, Func<ulong, ulong> transform)
    {
        List<ulong> rez = new List<ulong>();
        foreach (ulong n in list)
        {
            if (cond(n))
            {
                rez.Add(transform(n));
            }
        }

        return rez;
    }

    public static bool conditie(ulong n)
    {
        return n % 2 == 0;
    }

    public static ulong transformare(ulong n)
    {
        return n * n;
    }

    public static void afisareLista(List<ulong> list)
    {
        foreach (ulong n in list)
        {
            Console.Write(n + " ");
        }Console.WriteLine();
    }
    
}


public abstract class Cetatean
{
    public string nume { get; protected set; }
    public string prenume { get; protected set; }
    public string dataNastere {get; protected set;}
    public string serieCI {get; protected set;}

    public Cetatean(string nume, string prenume, string dataNastere, string serieCI)
    {
        this.nume = nume;
        this.prenume = prenume;
        this.dataNastere = dataNastere;
        this.serieCI = serieCI;
    }

    public abstract void Salut();

    public override string ToString()
    {
        return nume + " " + prenume + " " + dataNastere + " " + serieCI + " ";
    }
}

public class Cetatean_roman : Cetatean
{
    public bool stie_alta_limba {get; protected set;}

    public Cetatean_roman(string nume, string prenume, string dataNastere, string serieCI, bool stie_alta_limba) : base(
        nume, prenume, dataNastere, serieCI)
    {
        this.stie_alta_limba = stie_alta_limba;
    }

    public override void Salut()
    {
        Console.WriteLine("Salut, sunt Cetatean_roman");
    }

    public override string ToString()
    {
        return base.ToString() + stie_alta_limba;
    }
}

public class Cetatean_de_etnie : Cetatean
{
    public string limba_materna {get; protected set;}

    public Cetatean_de_etnie(string nume, string prenume, string dataNastere, string serieCI, string limba_materna) :
        base(
            nume, prenume, dataNastere, serieCI)
    {
        this.limba_materna = limba_materna;
    }
    
    public override void Salut()
    {
        Console.WriteLine("Salut, sunt Cetatean_de_etnie");
    }

    public override string ToString()
    {
        return base.ToString() + limba_materna;
    }
}