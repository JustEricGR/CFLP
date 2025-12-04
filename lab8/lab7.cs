using System;
using System.Collections.Generic;
using System.Linq;

public class CititorIntrare
{
    public event EventHandler<string> OnKeyPressed;

    public void ReadKeys()
    {
        Console.WriteLine("--- CititorIntrare: incepe citirea ---");
        string input;

        while (!string.IsNullOrEmpty(input = Console.ReadLine()))
            OnKeyPressed?.Invoke(this, input);
        Console.WriteLine("--- CititorIntrare: citire oprita ---");
    }
}

public class AscultatorCaracter
{
    private readonly char _caracterDeVerificat;

    public AscultatorCaracter(char caracter, CititorIntrare cititor)
    {
        _caracterDeVerificat = caracter;

        cititor.OnKeyPressed += HandleKeyPressed;
    }

    public void HandleKeyPressed(object sender, string keyString)
    {
        if (keyString.Contains(_caracterDeVerificat))
            Console.WriteLine($"[AscultatorCaracter '{_caracterDeVerificat}']Sirul '{keyString}' contine caracterul '{_caracterDeVerificat}'.");
    }
}

public class AscultatorComplex
{
    private readonly int _strNo;
    private readonly List<string> _istoricSiruri;

    public AscultatorComplex(int strNo, CititorIntrare cititor)
    {
        _strNo = strNo;
        _istoricSiruri = new List<string>(strNo);

        cititor.OnKeyPressed += HandleKeyPressed;
    }

    
    public void HandleKeyPressed(object sender, string keyString)
    {
        Console.WriteLine($"[AscultatorComplex | Istoric: {_istoricSiruri.Count}/{_strNo}] Primit: '{keyString}'");
        
        switch (keyString.ToLower())
        {
            case "afiseaza ultimul":
                if (_istoricSiruri.Any())
                    Console.WriteLine($"[AscultatorComplex] Ultimul sir reținut: '{_istoricSiruri.Last()}'");
                else
                    Console.WriteLine("[AscultatorComplex] Lista de siruri retinute este goala.");
                return; 
            case "afiseaza toate":
                if (_istoricSiruri.Any())
                {
                    Console.WriteLine("[AscultatorComplex] Toate sirurile retinute (de la cel mai vechi la cel mai nou):");
                    for (int i = 0; i < _istoricSiruri.Count; i++)
                        Console.WriteLine($"    [{i + 1}] {_istoricSiruri[i]}");
                }
                else
                    Console.WriteLine("[AscultatorComplex] Lista de siruri retinute este goala.");
                return;
            default:
                _istoricSiruri.Add(keyString);
                if (_istoricSiruri.Count > _strNo)
                    _istoricSiruri.RemoveAt(0);
                break;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        
        CititorIntrare cititor = new CititorIntrare();

        AscultatorCaracter ascultatorA = new AscultatorCaracter('a', cititor);
        AscultatorCaracter ascultatorZ = new AscultatorCaracter('z', cititor);
        
        AscultatorComplex ascultatorComplex = new AscultatorComplex(3, cititor); 

        
        Console.WriteLine("comenzi: 'afiseaza ultimul' , 'afiseaza toate'");
        Console.WriteLine("---------------------------------\n");

        // 3. Pornirea cititorului
        cititor.ReadKeys();
    }
}