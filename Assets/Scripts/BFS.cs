using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BFS
{
    private List<BfsDfsNode> _path = new List<BfsDfsNode>();
    private List<BfsDfsNode> _order = new List<BfsDfsNode>();
    private Queue<BfsDfsNode> _queue = new Queue<BfsDfsNode>();

     public List<Transform> Find(BfsDfsNode _start)
    {
        _start.Visited = true;
        _order.Add(_start);
        _queue.Enqueue(_start);

        while (_queue.Count > 0)
        {
            BfsDfsNode n = _queue.Dequeue();

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