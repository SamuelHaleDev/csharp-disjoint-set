using System.Data;

namespace csharp_disjoint_set;

public interface DisjointSets {
    // Connects items p and q
    public void Connect(int p, int q);

    // Checks to see if two items are connected
    public void IsConnected(int p, int q);
}



class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
