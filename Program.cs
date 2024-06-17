using System.Data;

namespace csharp_disjoint_set;

public interface DisjointSets {
    // Connects items p and q
    public void Union(int p, int q);

    // Checks to see if two items are connected
    public bool Connected(int p, int q);
}

public class UnionFind : DisjointSets {
    private int[] parents;

    UnionFind(int Size) {
        // Constructor goes here
        parents = new int[Size];
        for (int i = 0; i < Size; i++) {
            parents[i] = -1;
        }
    }

    public bool Connected(int vertex1, int vertex2) {

        return true;
    }

    public int SizeOf(int vertex) {
        return Math.Abs(Find(vertex));
    }

    public int Parent(int vertex) {
        return parents[vertex];
    }

    public void Union(int vertex1, int vertex2) {

    }

    public int Find(int vertex) {
        if (parents[vertex] < 0) {
            return vertex;
        }

        return Find(parents[vertex]);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
