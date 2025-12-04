using System;
using System.Collections.Generic;
using System.Linq;


public class CititorIntrare
{
public event EventHandler<string> OnKeyPressed;

public void ReadKeys()
{
    Console.WriteLine("=== CititorIntrare: Pornire citire ===");
    String input;

    while (!String.IsNullOrWhiteSpace(input = Console.ReadLine()))
    {
        OnKeyPressed?.Invoke(this, input);
    }

    Console.WriteLine("=== CititorIntrare: Oprire citire ===");
}
}

public class AscultatorCaracter
{
private readonly char _caracter;

public AscultatorCaracter(char caracter, CititorIntrare cititor)
{
    _caracter = caracter;
    cititor.OnKeyPressed += HandleKeyPressed;
}

public void HandleKeyPressed(object sender, String text)
{
    if(text=="afiseaza toate" || text=="afiseaza ultimul"){
        return;
    }
else if (text.Contains(_caracter))
{
Console.WriteLine(
$"[AscultatorCaracter '{_caracter}'] Textul '{text}' conține caracterul '{_caracter}'."
);
}
}
}

public class AscultatorComplex
{
public int _capacitateIstoric;
public List<String> _istoric;

public AscultatorComplex(int capacitate, CititorIntrare cititor)
{
_capacitateIstoric = capacitate;
_istoric = new List<String>(capacitate);
cititor.OnKeyPressed += HandleKeyPressed;
}

public void HandleKeyPressed(object sender, String input)
{
Console.WriteLine($"[AscultatorComplex | Istoric: {_istoric.Count}/{_capacitateIstoric}] Primit: '{input}'");

switch (input.Trim().ToLower())
{
case "afiseaza ultimul":
    AfiseazaUltimul();
    return;

case "afiseaza toate":
    AfiseazaToate();
    return;

default:
AdaugaInIstoric(input);
break;
}
}

private void AdaugaInIstoric(String input)
{
_istoric.Add(input);

if (_istoric.Count > _capacitateIstoric)
    _istoric.RemoveAt(0);
}

private void AfiseazaUltimul()
{
    if (_istoric.Any())
        Console.WriteLine($"[AscultatorComplex] Ultimul șir păstrat: '{_istoric.Last()}'");
    else
        Console.WriteLine("[AscultatorComplex] Istoricul este gol.");
}

    private void AfiseazaToate()
    {
        if (_istoric.Any())
        {
            Console.WriteLine("[AscultatorComplex] Istoric complet (de la vechi la nou):");
            for (int i = 0; i < _istoric.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {_istoric[i]}");
            }
        }
        else
        {
            Console.WriteLine("[AscultatorComplex] Istoricul este gol.");
        }
    }
}

public class Program
{
    public static void Main(String[] args)
    {
        var cititor = new CititorIntrare();

        var ascultatorA = new AscultatorCaracter('a', cititor);
        var ascultatorZ = new AscultatorCaracter('z', cititor);

        var ascultatorComplex = new AscultatorComplex(3, cititor);

        Console.WriteLine("Comenzi: 'afiseaza ultimul', 'afiseaza toate'");
        Console.WriteLine("---------------------------------------------\n");

    cititor.ReadKeys();
    }
}