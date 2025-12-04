using System;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

class Program
{
    static void Main(string[] args)
    {
        // Specifică calea către fișierul Excel
        string filePath1 = @"C:\Users\eric1\Documents\poli\anul3\cflp\CFLP\proiect\GM_2021_Chirila Ciprian-Bogdan.xlsx";
        string filePath2 = @"C:\Users\eric1\Documents\poli\anul3\cflp\CFLP\proiect\GM_2021_Popescu Ion.xlsx";

        List<CartePublicata>[] liste = new List<CartePublicata>[15];
        for (int i = 0; i < 15; i++)
        {
            liste[i] = new List<CartePublicata>();
        }
        // Deschide fișierul Excel
        Console.Write("Introdu un numar de fisiere: ");
        int nrFisiere = int.Parse(Console.ReadLine());

        if (nrFisiere == 1)
        {
            citesteFisier(liste,  filePath1);
        }
        else if (nrFisiere == 2)
        {
            citesteFisier(liste,  filePath1);
            citesteFisier(liste,  filePath2);
        }
        GenerateExcel(liste, @"C:\Users\eric1\Documents\poli\anul3\cflp\CFLP\proiect\output.xlsx");

        AfiseazaListe(liste);

    }

    static void citesteFisier(List<CartePublicata>[] liste, string path)
    {
        using (var workbook = new XLWorkbook(path))
        {
            int nr = 0;
            string numeSheet = "I.2.1a";
            for (int i = 1; i <= workbook.Worksheets.Count(); i++)
            {
                if (workbook.Worksheets.Worksheet(i).Name.ToString() == numeSheet)
                {
                    nr = i;
                    break;
                }
            }
            // Selectează foaia de lucru (sheet-ul) dorită
           
            for (int i = 0; i < 15; i++)
            {
                
                var worksheet = workbook.Worksheet(nr); // sau numele foii: workbook.Worksheet("Sheet1")
                
                // Iterează prin rândurile și coloanele foii de lucru
                foreach (var row in worksheet.RowsUsed().Skip(9))
                {
                    if (Enumerable.Range(2, 6).All(c =>
                            string.IsNullOrWhiteSpace(row.Cell(c).GetString())
                        ))
                        break;

                    
                    int anul;
                    int.TryParse(row.Cell(6).GetValue<string>(), out anul);

                    int pagTot;
                    int.TryParse(row.Cell(7).GetValue<string>(), out pagTot);

                    int pagProp;
                    int.TryParse(row.Cell(8).GetValue<string>(), out pagProp);

                    var carte = new CartePublicata
                    {
                        Autori = row.Cell(2).GetValue<string>(),
                        Titlu = row.Cell(3).GetValue<string>(),
                        Editura = row.Cell(4).GetValue<string>(),
                        ISBN = row.Cell(5).GetValue<string>(),
                        Anul = anul,
                        PaginiTotale = pagTot,
                        PaginiProprii = pagProp
                    };
                    liste[i].Add(carte);
                    
                }

                nr++;
            }
            
        }
    }
    
    static void AfiseazaListe(List<CartePublicata>[] liste)
    {
        for (int i = 0; i < liste.Length; i++)
        {
            Console.WriteLine($"\n=== Lista #{i} (total {liste[i].Count} înregistrări) ===");

            foreach (var carte in liste[i])
            {
                Console.WriteLine(
                    $"Autori: {carte.Autori} | " +
                    $"Titlu: {carte.Titlu} | " +
                    $"Editura: {carte.Editura} | " +
                    $"ISBN: {carte.ISBN} | " +
                    $"Anul: {carte.Anul} | " +
                    $"Pagini Totale: {carte.PaginiTotale} | " +
                    $"Pagini Proprii: {carte.PaginiProprii}"
                );
            }
        }
    }
    
    static void GenerateExcel(List<CartePublicata>[] liste, string outputPath)
    {
        string[] sheetNames = new string[]
        {
            "I.2.1a", "I.2.1b", "I.2.1c", "I.2.1d", "I.2.1e",
            "I.2.2a", "I.2.2b", "I.2.2c", "I.2.2d", "I.2.2e",
            "I.2.3a", "I.2.3b", "I.2.3c", "I.2.3d", "I.2.3e"
        };

        using (var workbook = new XLWorkbook())
        {
            for (int i = 0; i < sheetNames.Length; i++)
            {
                string sheetName = sheetNames[i];
                var sheet = workbook.Worksheets.Add(sheetName);

                // HEADER
                sheet.Cell(1, 1).Value = "Autori";
                sheet.Cell(1, 2).Value = "Titlu";
                sheet.Cell(1, 3).Value = "Editura";
                sheet.Cell(1, 4).Value = "ISBN";
                sheet.Cell(1, 5).Value = "Anul";
                sheet.Cell(1, 6).Value = "Pagini Totale";
                sheet.Cell(1, 7).Value = "Pagini Proprii";

                int row = 2;

                foreach (var carte in liste[i])
                {
                    sheet.Cell(row, 1).Value = carte.Autori;
                    sheet.Cell(row, 2).Value = carte.Titlu;
                    sheet.Cell(row, 3).Value = carte.Editura;
                    sheet.Cell(row, 4).Value = carte.ISBN;
                    sheet.Cell(row, 5).Value = carte.Anul;
                    sheet.Cell(row, 6).Value = carte.PaginiTotale;
                    sheet.Cell(row, 7).Value = carte.PaginiProprii;

                    row++;
                }

                // ajustare coloane
                sheet.Columns().AdjustToContents();
            }

            workbook.SaveAs(outputPath);
        }

        Console.WriteLine("Fisierul Excel a fost generat la: " + outputPath);
    }


}

class CartePublicata
{
    public string Autori { get; set; }
    public string Titlu { get; set; }
    public string Editura { get; set; }
    public string ISBN { get; set; }
    public int Anul { get; set; }
    public int PaginiTotale { get; set; }
    public int PaginiProprii { get; set; }
}
