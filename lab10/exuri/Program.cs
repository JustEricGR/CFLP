using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        Magazin magazin = new Magazin();

        magazin.AdaugaJoc(new Joc(
            "The Witcher 3",
            "CD Projekt",
            new DateTime(2015, 5, 19),
            120,
            new List<string>{"RPG", "Open World"}
        ));

        magazin.AdaugaJoc(new Joc(
            "Cyberpunk 2077",
            "CD Projekt",
            new DateTime(2020, 12, 10),
            200,
            new List<string>{"RPG", "Sci-Fi"}
        ));

        magazin.AdaugaJoc(new Joc(
            "Minecraft",
            "Mojang",
            new DateTime(2011, 11, 18),
            100,
            new List<string>{"Sandbox", "Survival"}
        ));

        Console.WriteLine("Jocuri înainte de 2016:");
        magazin.JocuriInainteDe(new DateTime(2016, 1, 1))
            .ForEach(j => Console.WriteLine(j));

        Console.WriteLine("\nNumar jocuri între 2010 și 2020:");
        Console.WriteLine(magazin.JocuriIntre(2010, 2020));

        Console.WriteLine("\nPrimul joc al CD Projekt:");
        Console.WriteLine(magazin.PrimulJocAlDezvoltatorului("CD Projekt"));
    }
}


public class Joc
{
    public string Nume { get; set; }
    public string Dezvoltator { get; set; }
    public DateTime DataLansare { get; set; }
    public float Pret { get; set; }
    public List<string> Etichete { get; set; }

    public Joc(string nume, string dezvoltator, DateTime dataLansare, float pret, List<string> etichete)
    {
        Nume = nume;
        Dezvoltator = dezvoltator;
        DataLansare = dataLansare;
        Pret = pret;
        Etichete = etichete;
    }

    public override string ToString()
    {
        return $"{Nume} ({Dezvoltator}) - Lansat la {DataLansare.ToShortDateString()}, Pret: {Pret}, Etichete: [{string.Join(", ", Etichete)}]";
    }
}

public class Magazin
{
    private List<Joc> jocuri = new List<Joc>();

    public void AdaugaJoc(Joc joc)
    {
        jocuri.Add(joc);
    }

    public List<Joc> JocuriInainteDe(DateTime data)
    {
        return jocuri
            .Where(j => j.DataLansare < data)
            .ToList();
    }

    public int JocuriIntre(int an1, int an2)
    {
        return jocuri
            .Count(j => j.DataLansare.Year >= an1 && j.DataLansare.Year <= an2);
    }

    public List<Joc> OrdoneazaDupaDezvoltatorSiNume()
    {
        return jocuri
            .OrderBy(j => j.Dezvoltator)
            .ThenBy(j => j.Nume)
            .ToList();
    }

    public List<Joc> JocuriDupaDezvoltatorOrdonateDupaPret(string dezvoltator, bool descrescator = false)
    {
        return jocuri
            .Where(j => j.Dezvoltator == dezvoltator)
            .OrderBy(j => descrescator ? -j.Pret : j.Pret)
            .ToList();
    }

    public Joc PrimulJocAlDezvoltatorului(string dezvoltator)
    {
        return jocuri
            .Where(j => j.Dezvoltator == dezvoltator)
            .OrderBy(j => j.DataLansare)
            .FirstOrDefault();
    }

    public bool JocMaiScumpDecat(float pret)
    {
        return jocuri.Any(j => j.Pret > pret);
    }

    public float SumaPreturilorPentruJocurileCuEticheta(string eticheta)
    {
        return jocuri
            .Where(j => j.Etichete.Contains(eticheta))
            .Sum(j => j.Pret);
    }

    public List<Joc> JocuriCareContinCelPutinOEticheta(List<string> etichete)
    {
        return jocuri
            .Where(j => j.Etichete.Intersect(etichete).Any())
            .ToList();
    }
}

