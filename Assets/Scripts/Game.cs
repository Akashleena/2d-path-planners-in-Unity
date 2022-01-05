using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{

    public int sx = 25;
    public int sz = 25;

    private GameObject[,] _tiles;
    private GameObject[] nodes;

    private List<BfsDfsNode> _path = new List<BfsDfsNode>();
    private BfsDfsNode _start;
    private BfsDfsNode _goal;
    public Transform startNode;
    public Transform endNode;

    private bool _loading = true;
    private bool _canDestroy = false;

    private DFS depth = new DFS();
    private BFS breadth = new BFS();


    // Use this for initialization


    public List<Transform> findBFSPath(Transform start, Transform end, int method)
    {
        startTime = Time.realtimeSinceStartup;

        nodes = GameObject.FindGameObjectsWithTag("Node");

        List<Transform> result = new List<Transform>();

        Transform node = (start, end);
       int  _method = method;
        startNode = start;
        endNode = end;
   
        foreach (GameObject obj in nodes)
        {
            DijkstraNode n = obj.GetComponent<DijkstraNode>();
            if (n.isWalkable())
            {
                n.resetNode();
                unexplored.Add(obj.transform);
            }
        }
        n.Visited = false;
        n.G = 0;
        n.H = 0;
        n.F = 0;
        n.Path = false;

        bool _found = false;
  
        switch (_method)
        {
            case 0: //DFS
                _found = depth.Find(startNode);
                _path = depth.GetPath();
                break;
            case 1: //BFS
                _found = breadth.Find(startNode);
                _path = breadth.GetPath();
                break;

        }

        GameObject t = GameObject.Find("GUI");

        //Search for end-node.
        if (_found)
        {
            t.SendMessage("SetMessage", "A path was found!");

            if (Debug.isDebugBuild)
            {
                string _out = "";

                foreach (Node n in _path)
                {
                    _out += n.name + " ";
                }
                Debug.Log(_out);
            }


        }
        else
        {
            t.SendMessage("SetMessage", "A path could not be found!");

            if (Debug.isDebugBuild)
            {
                Debug.Log("Search: Path Could not be found!");
            }
        }
    

    //Triggers the Search



}

