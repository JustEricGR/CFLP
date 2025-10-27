namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        RentableApartment ap1 = new RentableApartment(false, 350, 3, true, "Strada Libertatii 12", 70, 70000);
        RentableApartment ap2 = new RentableApartment(false, 420, 7, false, "Bulevardul Unirii 45", 85, 82000);
        House house1 = new House(50, "Strada Florilor 10", 120, 150000);

        // Creăm agenția cu lista inițială
        RealEstateAgency agency = new RealEstateAgency(new List<Property> { ap1, ap2, house1 });

        Console.WriteLine("=== Lista proprietăți inițială ===");
        foreach (var p in new List<Property> { ap1, ap2, house1 })
            Console.WriteLine(p);

        Console.WriteLine("\n=== Încercăm să închiriem ap1 ===");
        agency.inchiriazaProprietate("Strada Libertatii 12");

        Console.WriteLine("\n=== Încercăm să închiriem ap1 din nou ===");
        agency.inchiriazaProprietate("Strada Libertatii 12");

        Console.WriteLine("\n=== Încercăm să închiriem house1 (nu se poate) ===");
        agency.inchiriazaProprietate("Strada Florilor 10");

        Console.WriteLine("\n=== Starea proprietăților după închirieri ===");
        foreach (var p in new List<Property> { ap1, ap2, house1 })
            Console.WriteLine(p);
    }
}

public abstract class Property
{
    private string adresa;
    private double indoorArea;
    private double price;

    public Property(string adresa,  double indoorArea, double price)
    {
        this.adresa = adresa;
        this.indoorArea = indoorArea;
        this.price = price;
    }

    public string Adresa {  get => adresa; set => adresa = value; }
    public double IndoorArea { get => indoorArea; set => indoorArea = value; }
    public double Price { get => price; set => price = value; }

    public override string ToString()
    {
        return "Adresa: " + adresa + ", indorArea " + indoorArea + ", price: " + price;
    }
}

public interface IRentable
{
    bool IsRented { get; set; }
    double MonthlyRent { get; set; }
}

public class Apartament : Property
{
    private int floor;
    private bool HasElevator;

    public Apartament(int floor, bool hasElevator, string adresa,  double indoorArea, double price) : base(adresa, indoorArea, price)
    {
        
        this.floor = floor;
        this.HasElevator = hasElevator;
        
    }
    
    public int Floor { get => floor; set => floor = value; }

    public bool HasElevator1
    {
        get => HasElevator;
        set => HasElevator = value;
    }

    public override string ToString()
    {
        return base.ToString() + ", floor: " + floor + ", hasElevator: " + HasElevator;
    }
}

public class House : Property
{
    private double OutdoorArea;
    private double TotalArea;

    public House(double outdoorArea, string adresa, double indoorArea, double price) : base(adresa, indoorArea, price)
    {
        this.OutdoorArea = outdoorArea;
        this.TotalArea = outdoorArea + indoorArea;
    }

    public double OutdoorArea1
    {
        get => OutdoorArea;
        set => OutdoorArea = value;
    }
    
    public double TotalArea1 
    {
        get => TotalArea;
    }

    public override string ToString()
    {
        return base.ToString() + ", outdoorArea: " + OutdoorArea1 + ", totalArea: " + TotalArea1;
    }
}

public class RentableApartment : Apartament, IRentable
{
    public RentableApartment(bool isRented, double monthlyRent, int floor, bool hasElevator, string adresa, double indoorArea, double price) : base(floor,
        hasElevator, adresa, indoorArea, price)
    {
        this.IsRented = isRented;
        this.MonthlyRent = monthlyRent;
    }
    public bool IsRented { get; set; }
    public double MonthlyRent { get; set; }

    public override string ToString()
    {
        return base.ToString() + ", isRented: " +IsRented + ", monthlyRent: " + MonthlyRent;
    }
}

public class RealEstateAgency
{
    private List<Property> properties;
    public RealEstateAgency(List<Property> properties)
    {
        this.properties = properties;
    }

    public void adaugaProprietate(Property p)
    {
        properties.Add(p);
    }

    public void inchiriazaProprietate(string adresa)
    {
        foreach (Property property in properties)
        {
            if (property.Adresa == adresa)
            {
                if (property is RentableApartment ra)
                {
                    if (!ra.IsRented)
                    {
                        ra.IsRented = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Proprietatea este inchiriata");
                    }
                }
                else
                {
                    Console.WriteLine("Proprietatea nu este disponibilă pentru închiriere.");
                }
                
            }
        }
    }
}




