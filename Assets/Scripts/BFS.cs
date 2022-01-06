using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BFS
{
    // private List<BfsDfsNode> _path = new List<BfsDfsNode>();

    private List<Transform> _path = new List<Transform>();
    
   // private List<BfsDfsNode> _order = new List<BfsDfsNode>();
    
    private List<Transform> _order = new List<Transform>();
    //private Queue<BfsDfsNode> _queue = new Queue<BfsDfsNode>();
    private Queue<Transform> _queue = new Queue<Transform>();
    //public List<BfsDfsNode> result = new List<BfsDfsNode>();

     public List<Transform> result = new List<Transform>();


    public     
     public List<Transform> Find(Game Transform _start, Game Transform _end)
    {
        _start.Visited = true;
        _order.Add(_start);
        _queue.Enqueue(_start);

        while (_queue.Count > 0)
        {
           // BfsDfs n = _queue.Dequeue();
           Transform n = _queue.Dequeue();

            if (n.Status == BfsDfsNode.END)
            {
                BfsDfsNode t = n;
                while (t != null)
                {
                    _path.Add(t);
                    t.Path = true;
                    t = t.parent;
                }
                _path.Reverse();
                return true;
            }
            for (int i = 0; i < n.adjacent.Count; i++)
            {
                if (n.adjacent[i].IsValid())
                {
                    n.adjacent[i].parent = n;
                    n.adjacent[i].Visited = true;
                    _order.Add(n.adjacent[i]);
                    _queue.Enqueue(n.adjacent[i]);
                }
            }

        }

        return false;
    }

    public List<BfsDfsNode> GetPath() { return _path; }
    public List<BfsDfsNode> GetOrder() { return _order; }
}