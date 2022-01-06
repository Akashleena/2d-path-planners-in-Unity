using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BFS
{
    // private List<BfsDfsNode> _path = new List<BfsDfsNode>();

    private List<DijkstraNode> _path = new List<DijkstraNode>();

    // private List<BfsDfsNode> _order = new List<BfsDfsNode>();

    private List<Transform> _order = new List<Transform>();
    //private Queue<BfsDfsNode> _queue = new Queue<BfsDfsNode>();
    private Queue<Transform> _queue = new Queue<Transform>();
    //public List<BfsDfsNode> result = new List<BfsDfsNode>();

    public List<Transform> result = new List<Transform>();



    public List<Transform> Find(Game Transform _start, Game Transform _end)
    {

        DijkstraNode currentNode = _start.GetComponent<DijkstraNode>();
        // List<Transform> neighbours = currentNode.getNeighbourNode();
        // foreach (Transform neighNode in neighbours)
        // {
        //     DijkstraNode node = neighNode.GetComponent<DijkstraNode>();

        // We want to avoid those that had been explored and is not walkable.
        if (unexplored.Contains(neighNode) && node.isWalkable())

            currentNode.Visited = true;
        _order.Add(_start);
        _queue.Enqueue(_start);

        while (_queue.Count > 0)
        {
            // BfsDfs n = _queue.Dequeue();
            Transform n = _queue.Dequeue();

            if (n.transform.position == end.position)
            {
                DijkstraNode t = n.GetComponent<DijkstraNode>();// n is tranform and t is the corresponding node for that position
                while (t != null)
                {
                    _path.Add(t);
                    t.Path = true;
                    t = t.parent;
                    result.Add(n);
                }
                _path.Reverse();
                result.Reverse();
                return true;
            }
            for (int i = 0; i < t.adjacent.Count; i++)
            {
                if (t.adjacent[i].IsValid())
                {
                    t.adjacent[i].parent = t;
                    t.adjacent[i].Visited = true;
                    //how do i store the transform of the adjacent node in oder and enqueue  
                    _order.Add(t.adjacent[i].transform);
                    _queue.Enqueue(t.adjacent[i].transform);
                }
            }

        }

        return false;
    }

    public List<BfsDfsNode> GetPath() { return _path; }
    public List<BfsDfsNode> GetOrder() { return _order; }
}