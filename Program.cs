using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using NUnit.Framework;

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
        return Math.Abs(parents[Find(vertex)]);
    }

    public int Parent(int vertex) {
        if (vertex > parents.Length || vertex < 0) {
            throw new Exception("Cannot access parent of an out of range vertex!");
        }

        return parents[vertex];
    }

    public void Union(int vertex1, int vertex2) {
        if (vertex1 > parents.Length || vertex2 > parents.Length || vertex1 < 0 || vertex2 < 0) {
            throw new Exception("Cannot Union 1 or 2 out of range vertices!");
        }
        if (vertex1 == vertex2) {
            Console.WriteLine("Non breaking warning: can't union the same vertex");
            return;
        }

        int vertex1Size = SizeOf(vertex1);
        int vertex2Size = SizeOf(vertex2);

        int vertex1Root = Find(vertex1);
        int vertex2Root = Find(vertex2);

        if (vertex1Size <= vertex2Size) {
            parents[vertex1Root] = vertex2Root;
            parents[vertex2Root] -= vertex1Size;
            return;
        }

        parents[vertex2Root] = vertex1Root;
        parents[vertex1Root] -= vertex2Size;
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
        Console.WriteLine("Starting InitialStateTest...");
        InitialStateTest();
        Console.WriteLine("InitialStateTest completed...");

        Console.WriteLine("Starting IllegalFindTest...");
        IllegalFindTest();
        Console.WriteLine("IllegalFindTest completed...");

        Console.WriteLine("Starting BasicUnionTest...");
        BasicUnionTest();
        Console.WriteLine("BasicUnionTest completed...");

        Console.WriteLine("Starting SameUnionTest...");
        SameUnionTest();
        Console.WriteLine("SameUnionTest completed...");
    }

    public static void InitialStateTest() {
        UnionFind uf = new UnionFind(4);
        Debug.Assert(!uf.Connected(0,1), "0 and 1 should not be connected");
        Debug.Assert(!uf.Connected(0,2), "0 and 2 should not be connected");
        Debug.Assert(!uf.Connected(0,3), "0 and 3 should not be connected");
        Debug.Assert(!uf.Connected(1,2), "1 and 2 should not be connected");
        Debug.Assert(!uf.Connected(1,3), "1 and 3 should not be connected");
        Debug.Assert(!uf.Connected(2,3), "2 and 3 should not be connected");
    }

    public static void IllegalFindTest() {
        UnionFind uf = new UnionFind(4);
        try {
            uf.Find(10);
            Debug.Fail("Cannot find an out of range vertex!");
        } catch (Exception e) {
            return;
        }
        try {
            uf.Union(1, 10);
            Debug.Fail("Cannout Union with an out of range vertex!");
        } catch (Exception e) {
            return;
        }
    }

    public static void BasicUnionTest() {
        UnionFind uf = new UnionFind(10);
        uf.Union(0, 1);
        Debug.Assert(uf.Find(0) == 1);
        uf.Union(2, 3);
        Debug.Assert(uf.Find(2) == 3);
        uf.Union(0, 2);
        Debug.Assert(uf.Find(1) == 3);

        uf.Union(4, 5);
        uf.Union(6, 7);
        uf.Union(8, 9);
        uf.Union(4, 8);
        uf.Union(4, 6);

        Debug.Assert(uf.Find(5) == 9);
        Debug.Assert(uf.Find(7) == 9);
        Debug.Assert(uf.Find(8) == 9);

        uf.Union(9, 2);
        Debug.Assert(uf.Find(3) == 9);
    }

    public static void SameUnionTest() {
        UnionFind uf = new UnionFind(4);
        uf.Union(1, 1);

        for (int i = 0; i < 4; i++) {
            Debug.Assert(uf.Find(i) == i);
        }
    }
}
