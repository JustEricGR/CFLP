using System;
using System.Collections.Generic;


static void ex1()
{
    // 1️⃣ Creăm banca
    Banca banca = new Banca("Banca Nationala GPT", "BNGPT01");

    // 2️⃣ Deschidem conturi
    banca.deschideCont("Ion Popescu", TipCont.persoana, "RO01BNGPT001", 1000.0);
    banca.deschideCont("TechCorp SRL", TipCont.companie, "RO02BNGPT002", 5000.0);

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("=== Detalii conturi inițiale ===");
    Console.ResetColor();
    banca.afisareDetaliiCont("RO01BNGPT001");
    banca.afisareDetaliiCont("RO02BNGPT002");

    // 3️⃣ Depunere bani
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n=== Depunem 500 lei în contul lui Ion Popescu ===");
    Console.ResetColor();
    banca.depunereBaniInCont("RO01BNGPT001", 500.0);
    banca.afisareDetaliiCont("RO01BNGPT001");

    // 4️⃣ Retragere validă
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n=== Retragem 300 lei din contul lui Ion Popescu ===");
    Console.ResetColor();
    banca.retragereBaniDinCont("RO01BNGPT001", 300.0);
    banca.afisareDetaliiCont("RO01BNGPT001");

    // 5️⃣ Retragere invalidă (fonduri insuficiente)
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n=== Încercăm retragerea unei sume prea mari ===");
    Console.ResetColor();
    try
    {
        banca.retragereBaniDinCont("RO01BNGPT001", 5000.0);
    }
    catch (SumaInsuficienta e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Eroare: " + e.Message);
        Console.ResetColor();
    }
    banca.afisareDetaliiCont("RO01BNGPT001");

    // 6️⃣ Transfer valid între conturi
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n=== Transferăm 200 lei de la Ion Popescu la TechCorp ===");
    Console.ResetColor();
    banca.transfer("RO01BNGPT001", "RO02BNGPT002", 200.0);
    banca.afisareDetaliiCont("RO01BNGPT001");
    banca.afisareDetaliiCont("RO02BNGPT002");

    // 7️⃣ Transfer invalid (fonduri insuficiente)
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n=== Încercăm un transfer prea mare ===");
    Console.ResetColor();
    try
    {
        banca.transfer("RO01BNGPT001", "RO02BNGPT002", 10000.0);
    }
    catch (SumaInsuficienta e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Eroare la transfer: " + e.Message);
        Console.ResetColor();
    }
    banca.afisareDetaliiCont("RO01BNGPT001");
    banca.afisareDetaliiCont("RO02BNGPT002");

    // 8️⃣ Cont inexistent
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\n=== Încercăm o operațiune pe un cont inexistent ===");
    Console.ResetColor();
    banca.afisareDetaliiCont("RO00BNGPT999");

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n=== Test completat cu succes ===");
    Console.ResetColor();
}

ex1();

enum TipCont
{
    persoana,
    companie
}

class SumaInsuficienta : Exception {
    public SumaInsuficienta(string message) : base(message)
    {
        
    }
}


class Cont
{
    public readonly string titularCont;
    public readonly TipCont tipCont;
    public readonly string iban;
    public double suma;

    public Cont(string titularCont, TipCont tipCont, string iban, double suma)
    {
        this.titularCont = titularCont;
        this.tipCont = tipCont;
        this.iban = iban;
        this.suma = suma;
    }

    public void depunereBani(double suma)
    {
        this.suma += suma;
    }

    public void retragereBani(double suma)
    {
        this.suma -= suma;
    }

    public override string ToString()
    {
        return "Titular cont: " + this.titularCont + " tip cont: " + this.tipCont
        + " iban: " + this.iban + " suma: " + this.suma + "\n";
    }
}

class Banca
{
    public readonly string nume;
    public readonly string swift;
    public List<Cont> conturi;

    public Banca(string nume, string swift)
    {
        this.nume = nume;
        this.swift = swift;
        this.conturi = new List<Cont>();
    }

    public void deschideCont(string titularCont, TipCont tipCont, string iban, double suma)
    {
        Cont cont = new Cont(titularCont, tipCont, iban, suma);
        this.conturi.Add(cont);
    }

    public Cont GetCont(string iban)
    {
        foreach (Cont cont in conturi)
        {
            if (cont.iban.Equals(iban))
            {
                return cont;
            }
        }
        return null;
    }

    public void afisareDetaliiCont(string iban)
    {
        Cont cont = GetCont(iban);
        if (cont != null)
        {
            Console.WriteLine(cont);
        }
        else
        {
            Console.WriteLine("Nu exista contul\n");
        }
    }

    public void depunereBaniInCont(string iban, double suma)
    {
        Cont cont = GetCont(iban);
        if (cont != null)
        {
            cont.depunereBani(suma);
        }
        else
        {
            Console.WriteLine("Nu exista contul\n");
        }
    }

    public void retragereBaniDinCont(string iban, double suma)
    {
        Cont cont = GetCont(iban);
        if (cont != null)
        {
            if (cont.suma < suma) throw new SumaInsuficienta("nu sunt suficienti bani in cont");
            else
            {
                cont.suma -= suma;
            }
        }
        else
        {
            Console.WriteLine("Nu exista contul\n");
        }
    }

    public void transfer(string iban1, string iban2, double suma)
    {
        Cont cont1 = GetCont(iban1);
        Cont cont2 = GetCont(iban2);
        if (cont1 == null || cont2 == null)
        {
            Console.WriteLine("Nu exista unul din conturi\n");
        }
        else
        {
            if (cont1.suma < suma)
            {
                throw new SumaInsuficienta("Nu sunt suficienti bani in cont pt transfer\n");
            }
            else
            {
                cont1.retragereBani(suma);
                cont2.depunereBani(suma);
            }
        }
    }

}

