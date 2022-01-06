/**
 * Psuedo-Code for DFS
 * 
 * Start up 
 * 	Set all nodes to not visited 
 * 	Set start node coordinates 
 * 	Set start node’s parent to null 
 * 	Set goal node coordinates 
 * 	Call Traverse(start node) 
 * 
 * Traverse (Node n) 
 * 	Mark node n as visited 
 * 	If node n is the goal node 
 * 		Print all nodes starting from n and traversing through all ancestors (parents) 
 * 		Halt entire process 
 * 	For each node t that is an 8-connected neighbor node of n 
 * 		If node t has not been visited and node t is not a wall node 
 * 			Set parent of node t to node n 
 * 			Call Traverse(node t) 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DFS
{


    private List<DijkstraNode> _path = new List<DijkstraNode>();

    // private List<BfsDfsNode> _order = new List<BfsDfsNode>();

    private List<Transform> _order = new List<Transform>();
    //private Queue<BfsDfsNode> _queue = new Queue<BfsDfsNode>();
    private Queue<Transform> _queue = new Queue<Transform>();
    //public List<BfsDfsNode> result = new List<BfsDfsNode>();

    public List<Transform> result = new List<Transform>();
    //private List<BfsDfsNode> _path = new List<BfsDfsNode>();
    //private List<BfsDfsNode> _order = new List<BfsDfsNode>();

    //public List<Transform> result = new List<Transform>();

    public bool Find(Transform _start, Transform end)
    {
        DijkstraNode currentNode = _start.gameObject.GetComponent<DijkstraNode>();


        currentNode.Visited = true;

        _order.Add(_start);

        if (_start.position == end.position && (currentNode.isWalkable()))
        {
            _path.Add(currentNode);
            result.Add(_start);
            //currentNode.Path = true;
            return true;
        }
        else
        {
            DijkstraNode t = _start.gameObject.GetComponent<DijkstraNode>();
            for (int i = 0; i < t.adjacent.Count; i++)
            {
                if (t.adjacent[i].IsValid())
                {
                    if (t.isWalkable())
                    {
                        if (Find(t.adjacent[i].transform, end))
                        {
                            _path.Insert(0, t);
                            result.Insert(0, _start);
                            // _start.Path = true;
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
    public List<Transform> FindDfs(Transform _start, Transform _end)
    {
        Find(_start, _end);
        return result;
    }

    // public List<BfsDfsNode> GetPath() { return _path; }
    // public List<BfsDfsNode> GetOrder() { return _order; }
}