using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{

    public string methodName = "";
    // public int sx = 25;
    // public int sz = 25;

    private float endTime;
    private float startTime;

    private GameObject[,] _tiles;
    private GameObject[] nodes;

    private List<BfsDfsNode> _path = new List<BfsDfsNode>();
    private BfsDfsNode _start;
    private BfsDfsNode _goal;
    public Transform startNode;
    public Transform endNode;

    private bool _loading = true;
    private bool _canDestroy = false;
    WriteToCSVFile writeToCsv;
    public GameObject csvObject;
    public GameObject gridGenerator;
    private DFS depth = new DFS();
    private BFS breadth = new BFS();

    private GenerateGrid gridDj;
    // Use this for initialization
    private void Awake()//When the program starts
    {
        writeToCsv = csvObject.GetComponent<WriteToCSVFile>();
        //use the same GenerateGrid instance as that of Unity Scene, instead of creating a new Object
        gridDj = gridGenerator.GetComponent<GenerateGrid>();

    }


    public List<Transform> findBFSPath(Transform start, Transform end, int method)
    {
        startTime = Time.realtimeSinceStartup;

        nodes = GameObject.FindGameObjectsWithTag("Node");

        List<Transform> result = new List<Transform>();

        // Transform node = (start, end);
        int _method = method;
        startNode = start;
        endNode = end;

        foreach (GameObject obj in nodes)
        {
            DijkstraNode n = obj.GetComponent<DijkstraNode>();
            if (n.isWalkable())
            {
                n.resetNode();


                n.Visited = false;
                n.G = 0;
                n.H = 0;
                n.F = 0;
                n.Path = false;
            }

        }
        for (int x = 0; x < gridDj.row; x++)
        {
            for (int z = 0; z < gridDj.column; z++)
            {
                //Use the formula Given to convert row and column to 1D index
                //https://stackoverflow.com/questions/1730961/convert-a-2d-array-index-into-a-1d-index
                DijkstraNode n = gridDj.grid[(x * gridDj.column) + z].GetComponent<DijkstraNode>();
                //use gridDj.row instead of sx and gridDj.column instead of sz
                if ((x - 1) >= 0 && (z + 1) < gridDj.column) n.adjacent.Add(gridDj.grid[((x - 1) * gridDj.column) + (z + 1)].GetComponent<DijkstraNode>());    //1
                if ((z + 1) < gridDj.column) n.adjacent.Add(gridDj.grid[(x * gridDj.column) + (z + 1)].GetComponent("DijkstraNode") as DijkstraNode);                        //2
                if ((x + 1) < gridDj.row && (z + 1) < gridDj.column) n.adjacent.Add(gridDj.grid[((x + 1) * gridDj.column) + (z + 1)].GetComponent<DijkstraNode>());    //3
                if ((x - 1) >= 0) n.adjacent.Add(gridDj.grid[((x - 1) * gridDj.column) + z].GetComponent<DijkstraNode>());                        //4
                if ((x + 1) < gridDj.row) n.adjacent.Add(gridDj.grid[((x + 1) * gridDj.column) + z].GetComponent<DijkstraNode>());                        //5
                if ((x - 1) >= 0 && (z - 1) >= 0) n.adjacent.Add(gridDj.grid[((x - 1) * gridDj.column) + (z - 1)].GetComponent<DijkstraNode>());    //6
                if ((z - 1) >= 0) n.adjacent.Add(gridDj.grid[x * gridDj.column + (z - 1)].GetComponent("DijkstraNode") as DijkstraNode);                        //7
                if ((x + 1) < gridDj.row && (z - 1) >= 0) n.adjacent.Add(gridDj.grid[((x + 1) * gridDj.column) + (z - 1)].GetComponent<DijkstraNode>());    //8
            }
        }



        switch (_method)
        {
            case 0: //DFS
                result = depth.FindDfs(startNode, endNode);
                methodName = "DFS";
                //  _path = depth.GetPath();
                break;
            case 1: //BFS
                result = breadth.Find(startNode, endNode);
                methodName = "BFS";
                // _path = breadth.GetPath();
                break;

        }

        // GameObject t = GameObject.Find("GUI");

        //Search for end-node.
        // if (_found)
        // {
        //     t.SendMessage("SetMessage", "A path was found!");

        //     if (Debug.isDebugBuild)
        //     {
        //         string _out = "";

        //         foreach (Node n in _path)
        //         {
        //             _out += n.name + " ";
        //         }
        //         Debug.Log(_out);
        //     }


        // }
        // else
        // {
        //     t.SendMessage("SetMessage", "A path could not be found!");

        //     if (Debug.isDebugBuild)
        //     {
        //         Debug.Log("Search: Path Could not be found!");
        //     }
        // }

        endTime = (Time.realtimeSinceStartup - startTime);
        print("Compute time: " + endTime);

        print("Path completed!");
        //Triggers the Search
        //  writeToCsv.WriteCSV(methodName, endTime, totalCost, result.Count);

        return result;


    }
}


