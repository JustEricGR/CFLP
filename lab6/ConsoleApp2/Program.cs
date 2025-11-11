namespace exuriLab6;

class Program
{
    static void Main(string[] args)
    {
        //ex1
        // int size = 5;
        // int []arr = new int[size];
        // for (int i = 0; i < arr.Length; i++)
        // {
        //     arr[i] = i;
        // }
        //
        // afisareArray(arr, size);
        // RemoveAt(arr, ref size, 2);
        // afisareArray(arr, size);

        // List<int> list1 = new List<int>();
        // List<int> list2 = new List<int>();
        //
        // for (int i = 0; i < 10; i++)
        // {
        //     list1.Add(i);
        //     list2.Add(i+5);
        // }
        //
        // List<int> rez = new List<int>();
        // rez = Intersection(list1, list2);
        // foreach (var item in rez)
        // {
        //     Console.Write(item + " ");
        // }
        
        Stack<int> stiva = new Stack<int>();

        // Test Push și Peek
        stiva.Push(10);
        stiva.Push(20);
        Console.WriteLine("Peek: " + stiva.Peek()); // 20

        // Test Pop
        int x = stiva.Pop();
        Console.WriteLine("Popped element: " + x);           // 20
        Console.WriteLine("New peek: " + stiva.Peek()); // 10

        // Test TryPop
        if (stiva.TryPop(out int y))
            Console.WriteLine("Scos: " + y); // 10
        else
            Console.WriteLine("Stiva goala");

        // Test TryPop pe stiva goala
        if (stiva.TryPop(out int z))
            Console.WriteLine("Scos: " + z);
        else
            Console.WriteLine("Stiva goala"); // Stiva goala

        // Test Pop pe stiva goala → trebuie sa arunce exceptie
        try
        {
            stiva.Pop();
        }
        catch (EmptyStack e)
        {
            Console.WriteLine("Exceptie prinsa: " + e.Message); // Stiva goala
        }
    }

    public static void RemoveAt(int[] array, ref int length, int n)
    {
        for (int i = 0; i < length; i++)
        {
            if (i == n)
            {
                
                for (int j = i; j < length - 1; j++)
                {
                    array[j] = array[j + 1];
                }
                length--;
                break;
            }
        }
    }

    public static void afisareArray(int[] array, int length)
    {
        for (int i = 0; i<length; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
    }

    public static List<int> Intersection(List<int> l1, List<int> l2)
    {
        List<int> finalList = new List<int>();
        for (int i=0; i<l1.Count; i++)
        {
            int x = l1[i];
            if (l2.Contains(x))
            {
                finalList.Add(x);
            }
        }

        return finalList;
    }
}

public class EmptyStack : Exception
{
    public EmptyStack(string message) : base(message)
    {
        
    }
}

public class Stack<T>
{
    private List<T> lista;

    public Stack()
    {
        this.lista = new List<T>();
    }

    public void Push(T item)
    {
        this.lista.Add(item);
    }

    public T Pop()
    {
        if (lista.Count == 0)
        {
            throw new EmptyStack("Stiva goala");
        }
        
        T element = lista[lista.Count - 1];
        lista.RemoveAt(lista.Count - 1);
        return element;
        
    }

    public bool TryPop(out T elem)
    {
        try
        {
            elem = Pop();
            return true;
        }
        catch (EmptyStack)
        {
            elem = default(T);
            return false;
        }
    }

    public T Peek()
    {
        return lista[lista.Count - 1];
    }
}