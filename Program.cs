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

    public UnionFind(int Size) {
        if (Size < 0 || Size == 0) {
            throw new Exception("Cannot initialize a Disjoint Set with 0 or less size");
        }

        parents = new int[Size];
        for (int i = 0; i < Size; i++) {
            parents[i] = -1;
        }
    }

    public bool Connected(int vertex1, int vertex2) {
        return (Find(vertex1) == Find(vertex2));
    }

    public int SizeOf(int vertex) {
        return Math.Abs(Find(vertex));
    }

    public int Parent(int vertex) {
        if (vertex > parents.Length || vertex < 0) {
            throw new Exception("Cannot access parent of an out of range vertex!");
        }

        return parents[vertex];
    }

    public void Union(int vertex1, int vertex2) {
        if (vertex1 > parents.Length || vertex2 > parents.Length || vertex1 < 0 || vertex2 < 0) {
            throw new Exception("Cannot union 1 or 2 out of range vertices!");
        }

        int vertex1Size = SizeOf(vertex1);
        int vertex2Size = SizeOf(vertex2);

        int vertex1Root = Find(vertex1);
        int vertex2Root = Find(vertex2);

        if (vertex1Size <= vertex2Size) {
            parents[vertex2Root] = vertex1Root;
            parents[vertex1] += vertex2Size;
            return;
        }

        parents[vertex1Root] = vertex2Root;
        parents[vertex2] += vertex1Size;
    }

    public int Find(int vertex) {
        if (vertex > parents.Length || vertex < 0) {
            throw new Exception("Cannot find an out of range vertex!");
        }

        if (parents[vertex] < 0) {
            return vertex;
        }

        return parents[vertex] = Find(parents[vertex]);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }

    public UnionFind InitialState() {
        UnionFind uf = new UnionFind(4);
        if (uf.Connected(0,1) != false) throw new Exception("Error Constructing");
        if (uf.Connected(0,2) != false) throw new Exception("Error Constructing");
        if (uf.Connected(0,3) != false) throw new Exception("Error Constructing");
        if (uf.Connected(1,2) != false) throw new Exception("Error Constructing");
        if (uf.Connected(1,3) != false) throw new Exception("Error Constructing");
        if (uf.Connected(2,3) != false) throw new Exception("Error Constructing");

        return uf;
    }
}
