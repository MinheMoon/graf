﻿class Graph
{
    private Dictionary<int, HashSet<int>> adjList = new Dictionary<int, HashSet<int>>();

    public void AddEdge(int src, int dest)
    {
        if (!adjList.ContainsKey(src))
            adjList[src] = new HashSet<int>();

        if (!adjList.ContainsKey(dest))
            adjList[dest] = new HashSet<int>();

        adjList[src].Add(dest);
        adjList[dest].Add(src); 
    }

 public List<int> GetUnreachableVertices(int startVertex)
{
    var visited = new HashSet<int>();
    var unreachable = new List<int>();

    DFS(startVertex, visited);

    foreach (var vertex in adjList.Keys)
    {
        if (!visited.Contains(vertex))
        {
            unreachable.Add(vertex);
        }
    }

    return unreachable;
}

     public void DisplayReachability(int startVertex)
    {
        var visited = new HashSet<int>();
        DFS(startVertex, visited);
        Console.WriteLine($"Вершина {startVertex} може дістатися до {visited.Count - 1} інших вершин");
        Console.WriteLine("Вершини, до яких можна дістатися:");
        foreach (var vertex in visited.Where(v => v != startVertex))
        {
            Console.WriteLine(vertex);
        }
    }

    private void DFS(int vertex, HashSet<int> visited)
    {
        visited.Add(vertex);

        foreach (var neighbor in adjList[vertex])
        {
            if (!visited.Contains(neighbor))
            {
                DFS(neighbor, visited);
            }
        }
    }
    public IEnumerable<int> GetAllVertices()
    {
        return adjList.Keys;
    }

}

class Program
{
    static void Main()
    {
        var graph = new Graph();
        graph.AddEdge(1, 2);
        graph.AddEdge(1, 4);
        graph.AddEdge(2, 3);
        graph.AddEdge(2, 4);
        graph.AddEdge(4, 3);
        graph.AddEdge(5, 5);

        Console.WriteLine("Недосяжні вершини:");
        foreach (var vertex in graph.GetAllVertices())
        {
            var unreachableVertices = graph.GetUnreachableVertices(vertex);
            Console.WriteLine($"З вершини {vertex}:");
            foreach (var unreachable in unreachableVertices)
            {
                Console.WriteLine(unreachable);
            }
        }

        Console.WriteLine("\nДосяжність вершин:");
        foreach (var vertex in graph.GetAllVertices())
        {
            graph.DisplayReachability(vertex);
        }
    }
}
