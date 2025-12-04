using System;
using System.Numerics;

// Test 1, Rand 1
// 1. Implementati o metoda ConvertTemperature care primeste ca parametru temperatura in Kelvin si
// returneaza true daca temperatura este mai mare decat 0K. In plus, metoda returneaza temperatura in
// grade Celsius prin intermediul unui alt parametru. (2p)
// C = K – 273.15
// 2. Implementati o metoda All care primeste ca parametru o lista de float si o conditie (o
//     funcție care primește un float și returnează un bool) si returneaza true daca toate elementele listei
// respecta conditia. In caz contrar, metoda returneaza false. (2p)
// 3. Creati urmatoare ierarhie de clase:
// O clasa abstracta Appliance, care contine: (1p)
// - Brand
//     - MaxPower
//     - CurrentPower
//     - un constructor care initializeaza cele 3 proprietati.
// - o metoda abstracta Activate
//     - o metoda abstracta Deactivate
//     O clasa WashingMachine, care mosteneste Appliance si contine: (1.5p)
// - override la metoda Activate, care seteaza nivelul de putere pe MaxPower
//     - override la metoda Deactivate, care seteaza nivelul de putere pe 0.
// - override la metoda ToString pentru a afisa toate proprietatile clasei.
//     O clasa Refrigerator, care mosteneste Appliance si contine: (1.5p)
// - IdlePower – initializat prin constructor
//     - override la metoda Activate, care seteaza nivelul de putere pe MaxPower / 2
//     - override la metoda Deactivate, care seteaza nivelul de putere pe IdlePower
//     - override la metoda ToString pentru a afisa toate proprietatile clasei.
//     Adaugati cel putin un obiect din fiecare clasa (WashingMachine si Refrigerator) intr-o lista de tip
//     Appliance, apoi apelati metodele implementate pentru fiecare obiect astfel incat sa se poata observa
// modul de functionare al acestora. Pentru afisarea starii obiectelor se va folosi metoda ToString. (1p)
// Oficiu: 1p

namespace modelTest;
    
    internal class Program { 
        
        static void Main(string[] args)
        {
            //1
            // double temperatureInC=0;
            // bool isTrueOrFalse = ConvertTemperature(330, ref temperatureInC);
            //
            
            // Console.WriteLine("Method status: "+isTrueOrFalse + "\nKelvin temp converted in Celsius: " + temperatureInC); 
            
            
            //2
            // List<float> numere = new List<float>{1.5f, 2.44f, 5.32f};
            //
            // bool conditie_1 = All(numere, x => x < 3);
            // bool conditie_2 = All(numere, x => x < 6 && x > 1);
            //
            // Console.WriteLine(conditie_1 + " " + conditie_2);
            
            
            //3
            WashingMachine w1 = new WashingMachine("Bjorn", 300, 150);
            Refrigerator r1 = new Refrigerator("Akula", 600, 350, 100);
            
            List<Appliance> chestii = new List<Appliance>();
            chestii.Add(w1);
            chestii.Add(r1);

            foreach (Appliance a in chestii)
            {
                Console.WriteLine(a.ToString());
            }
        }

        //1
        static bool ConvertTemperature(int tempInKelvin, ref double tempInCelsius)
        {
            if (tempInKelvin <= 0)
            {
                return false;
            }
            tempInCelsius = tempInKelvin-273.15;
            return true;
        }

        //2
        static bool All(List<float> list, Func<float, bool> func)
        {
            foreach(float elem in list)
            {
                if (!func(elem))
                {
                    return false;
                }
            }
            return true;
        }
    }

    abstract class Appliance
    {
        public string brand {get; set; }
        public double maxPower{get; set; }
        public double currentPower{get; set; }

        public Appliance(string b, double mp, double cp)
        {
            brand = b;
            maxPower = mp;
            currentPower = cp;
        }
        
        public abstract void Activate();
        public abstract void Deactivate();
    }

    class WashingMachine:Appliance
    {
        private String name = "WashingMachine";
        public WashingMachine(string b, double mp, double cp)
            : base(b, mp, cp)
        {
        }
        
        public override void Activate()
        {
            currentPower = maxPower;
        }
        
        public override void Deactivate()
        {
            currentPower = 0;
        }

        public override string ToString()
        {
            string text = "Name: "+this.name+
                          "\nBrand: "+brand+
                          "\nMax Power: "+maxPower+
                          "\nCurrent Power: "+currentPower+
                          "\n";
            return text;
        }
    }

    class Refrigerator : Appliance
    {
        private string name = "Refrigerator";
        public double idlePower{get; set;}
        public Refrigerator(string b, double mp, double cp, double ip)
            : base(b, mp, cp)
        {
            ip = idlePower;
        }
        
        
        public override void Activate()
        {
            currentPower = maxPower / 2;
        }

        public override void Deactivate()
        {
            currentPower = idlePower;
        }

        override public string ToString()
        {
            string text = "Name: "+this.name+
                          "\nBrand: "+brand+
                          "\nMax Power: "+maxPower+
                          "\nCurrent Power: "+currentPower+
                          "\nIdle Power: "+idlePower+
                          "\n";
            return text;
 }
}