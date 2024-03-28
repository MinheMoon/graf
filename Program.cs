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

    public List<int> GetUnreachableVertices()
    {
        var visited = new HashSet<int>();
        var unreachable = new List<int>();

        foreach (var vertex in adjList.Keys)
        {
            if (!visited.Contains(vertex))
            {
                DFS(vertex, visited);
            }
        }

        foreach (var vertex in adjList.Keys)
        {
            if (!visited.Contains(vertex))
            {
                unreachable.Add(vertex);
            }
        }

        return unreachable;
    }

    public void DisplayReachability()
    {
        foreach (var vertex in adjList.Keys)
        {
            var visited = new HashSet<int>();
            DFS(vertex, visited);
            Console.WriteLine($"Вершина {vertex} може дістатися до {visited.Count - 1} інших вершин");
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

        var unreachableVertices = graph.GetUnreachableVertices();
        Console.WriteLine("Недосяжні вершини:");
        foreach (var vertex in unreachableVertices)
        {
            Console.WriteLine(vertex);
        }

        Console.WriteLine("\nДосяжність вершин:");
        graph.DisplayReachability();
    }
}
