using System;
using System.Collections.Generic;

static void ex1()
{
    int x = int.Parse(Console.ReadLine());
    int suma = 0;
    for (int i = 0; i < x; i++)
    {
        suma += i;
    }

    Console.WriteLine("Suma este: " + suma);
}

static void ex2()
{


    double a = double.Parse(Console.ReadLine());
    double b = double.Parse(Console.ReadLine());
    double c = double.Parse(Console.ReadLine());
    double delta = Math.Pow(b, 2) - 4 * a * c;
    if (Math.Pow(b, 2) < 4 * a * c) return;
    else if (a == 0 && b != 0)
    {
        double x = -c / b;
        Console.WriteLine("x=" + x);
    }
    else if (a == 0 && b == 0)
    {

        Console.WriteLine("x=0");
    }
    else
    {
        double x1 = (-b + Math.Sqrt(delta)) / 2 * a;
        double x2 = (-b - Math.Sqrt(delta)) / 2 * a;
        Console.WriteLine("x1=" + x1);
        Console.WriteLine("x1=" + x1);
    }

}

static void ex3()
{
    List<Student> students = new List<Student>();
    students.Add(new Student("Alice", 2003, 8, 9, 7));
    students.Add(new Student("Alexandru", 2001, 5, 10, 10));
    students.Add(new Student("Magda", 1999, 6, 10, 9));
    Student max = students[0];
    foreach (Student student in students)
    {
        if (student.calcMedie() > max.calcMedie())
        {
            max = student;
        }
    }
    Console.WriteLine(max);
}

ex3();

class Student
{
    private readonly string name;
    private readonly int year;
    private readonly int grade1;
    private readonly int grade2;
    private readonly int grade3;

    public Student(string n, int y, int g1, int g2, int g3)
    {
        this.name = n;
        this.year = y;
        this.grade1 = g1;
        this.grade2 = g2;
        this.grade3 = g3;

    }

    public string Name => name;
    public int Year => year;
    public double Grade1 => grade1;
    public double Grade2 => grade2;
    public double Grade3 => grade3;

    public double calcMedie()
    {
        return (grade1 + grade2 + grade3) / 3.0;
    }

    public override string ToString()
    {
        return "Studentul: " + this.name +
            "\nAn: " + this.year +
            "\nNota 1: " + this.grade1 +
            "\nNota 2: " + this.grade2 +
            "\nNota 3: " + this.grade3;
    }
}





