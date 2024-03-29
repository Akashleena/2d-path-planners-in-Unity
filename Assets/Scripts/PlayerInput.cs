﻿using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class PlayerInput : MonoBehaviour
{

    private Transform node;
    private Transform startNode;
    private Transform endNode;
    private List<Transform> blockPath = new List<Transform>();
    private List<Transform> currentPath = new List<Transform>();

    private Vector3 finalbfsPath;

    // Update is called once per frame
    void Update()
    {
        mouseInput();
    }

    void Start()
    {
        Debug.Log("Test");
    }

    /// <summary>
    /// Mouse click.
    /// </summary>
    private void mouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {

            // Update colors for every mouse clicked.
            this.colorBlockPath();
            this.updateNodeColor();

            // Get the raycast from the mouse position from screen.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Node")
            {
                //unmark previous
                Renderer rend;
                if (node != null)
                {
                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.white;
                }

                // We now update the selected node.
                node = hit.transform;

                // Mark it
                rend = node.GetComponent<Renderer>();
                rend.material.color = Color.green;

            }
        }
    }

    /// <summary>
    /// Button for Set Starting node.
    /// </summary>
    public void btnStartNode()
    {
        if (node != null)
        {
            DijkstraNode n = node.GetComponent<DijkstraNode>();

            // Making sure only walkable node are able to set as start.
            if (n.isWalkable())
            {
                // If this is a new start node, we will just set it to blue.
                if (startNode == null)
                {
                    Renderer rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.blue;
                }
                else
                {
                    // Reverse the color of the previous node
                    Renderer rend = startNode.GetComponent<Renderer>();
                    rend.material.color = Color.white;

                    // Set the new node as blue.
                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.blue;
                }

                startNode = node;
                node = null;
            }
        }
    }

    /// <summary>
    /// Button for Set End node.
    /// </summary>
    public void btnEndNode()
    {
        if (node != null)
        {
            DijkstraNode n = node.GetComponent<DijkstraNode>();

            // Making sure only walkable node are able to set as end.
            if (n.isWalkable())
            {
                // If this is a new end node, we will just set it to cyan.
                if (endNode == null)
                {
                    Renderer rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.cyan;
                }
                else
                {
                    // Reverse the color of the previous node
                    Renderer rend = endNode.GetComponent<Renderer>();
                    rend.material.color = Color.white;

                    // Set the new node as cyan.
                    rend = node.GetComponent<Renderer>();
                    rend.material.color = Color.cyan;
                }

                endNode = node;
                node = null;
            }
        }
    }

    /// <summary>
    /// Button for find path.
    /// </summary>
    public void btnFindAStarPath()
    {
        // Only find if there are start and end node.
        if (startNode != null && endNode != null)
        {
            // Execute Shortest Path.
            ShortestPath finder = gameObject.GetComponent<ShortestPath>();


            // List<Transform> paths = finder.findShortestPath(startNode, endNode);

            // // Colour the node red.
            // foreach (Transform path in paths)
            // {
            //     Renderer rend = path.GetComponent<Renderer>();
            //     rend.material.color = Color.red;
            // }
            clearPreviousPath();
            AStarPath pathFinder = gameObject.GetComponent<AStarPath>();
            currentPath = pathFinder.FindAStartPath(startNode, endNode);

            foreach (Transform path in currentPath)
            {
                Renderer rend = path.GetComponent<Renderer>();
                rend.material.color = Color.red;
            }
        }
    }

    public void btnFindDijkstraPath()
    {
        if (startNode != null && endNode != null)
        {
            clearPreviousPath();
            // Execute Shortest Path.
            ShortestPath finder = gameObject.GetComponent<ShortestPath>();
            currentPath = finder.findShortestPath(startNode, endNode);

            // Colour the node red.
            foreach (Transform path in currentPath)
            {
                Renderer rend = path.GetComponent<Renderer>();
                rend.material.color = Color.red;
            }
        }
    }

    public void btnFindTrrtPath()
    {
        // Only find if there are start and end node.
        if (startNode != null && endNode != null)
        {

            clearPreviousPath();
            TrrtComscene trrtPath = gameObject.GetComponent<TrrtComscene>();
            trrtPath.BeginSolving(10, startNode, endNode);
            trrtPath.ContinueSolving();

        }
    }


    public void btnFindBFSPath()
    {



        if (startNode != null && endNode != null)
        {
            clearPreviousPath();

            Game bfs = gameObject.GetComponent<Game>();
            currentPath = bfs.findBFSPath(startNode, endNode, 1);
            Debug.Log(currentPath.Count);
            // Colour the node red.
            foreach (Transform path in currentPath)
            {
                Renderer rend = path.GetComponent<Renderer>();
                rend.material.color = Color.red;
            }
        }

    }

    public void btnFindDFSPath()
    {



        if (startNode != null && endNode != null)
        {
            clearPreviousPath();

            Game dfs = gameObject.GetComponent<Game>();
            currentPath = dfs.findBFSPath(startNode, endNode, 0);

            // Colour the node red.
            foreach (Transform path in currentPath)
            {
                Renderer rend = path.GetComponent<Renderer>();
                rend.material.color = Color.red;
            }
        }

    }

     public void btnFindRRTPath()
    {
        // Only find if there are start and end node.
        if (startNode != null && endNode != null)
        {

            clearPreviousPath();
            RRT rrtPath = gameObject.GetComponent<RRT>();
            rrtPath.BeginSolving(10, startNode, endNode);
            rrtPath.ContinueSolving();

        }
    }







    //     // Execute Shortest Path.
    //     PathFindingBfs.TileGrid bfsFinder = gameObject.GetComponent<PathFindingBfs.TileGrid>();
    //     // PathFindingBfs.Tile tile;

    //     // PathFindingBfs.TileGrid tg = new PathFindingBfs.TileGrid();
    //     // PathFindingBfs.PathFinder pf = new PathFindingBfs.PathFinder();
    //     int start = startNode.GetComponent<DijkstraNode>().iGridX * 25 + startNode.GetComponent<DijkstraNode>().iGridY; // convert co-ordinate  to node number
    //     int end = endNode.GetComponent<DijkstraNode>().iGridX * 25 + endNode.GetComponent<DijkstraNode>().iGridY;
    //     Debug.Log(start);
    //     Debug.Log(end);
    //     List<PathFindingBfs.Tile> bfsPath = bfsFinder.SendStartGoal(start, end);
    //     //tg.GetTile(start, end);

    //     // List<Tile> bfsPath = bfsFinder.FindPath(tg.start, tg.end, pf.FindPath_BFS);

    //     Debug.Log("bfsPath no of nodes " + bfsPath.Count);
    //     for (int i = 0; i < bfsPath.Count; i++)
    //     {
    //         //converts node numbers to co-ordinates
    //         finalbfsPath = bfsPath[i].transform.position;
    //         Debug.Log(finalbfsPath);
    //         // currentPath[i].position = new Vector3(((int)(bfsPath[i]).ToVector2().x) % bfsFinder.Rows, -((bfsPath[i].ToVector2().y / bfsFinder.Rows) + 1), currentPath[i].position.z);
    //         // Debug.Log(currentPath[i].position);

    //     }
    //     foreach (Transform path in finalbfsPath)
    //     {
    //         Renderer rend = path.GetComponent<Renderer>();
    //         rend.material.color = Color.red;
    //     }


    /// <summary>
    /// Resets the previous generated path, if any.
    /// </summary>
    public void clearPreviousPath()
    {
        if (currentPath.Count > 0)
        {
            foreach (Transform path in currentPath)
            {
                Renderer rend = path.GetComponent<Renderer>();
                rend.material.color = Color.white;
            }
        }

        currentPath.Clear();
    }

    /// <summary>
    /// Button for blocking a path.
    /// </summary>
    public void btnBlockPath()
    {
        if (node != null)
        {
            // Render the selected node to black.
            Renderer rend = node.GetComponent<Renderer>();
            rend.material.color = Color.black;

            // Set selected node to not walkable
            DijkstraNode n = node.GetComponent<DijkstraNode>();
            n.setWalkable(false);

            // Add the node to the block path list.
            blockPath.Add(node);

            // If the block path is start node, we remove start node.
            if (node == startNode)
            {
                startNode = null;
            }

            // If the block path is end node, we remove end node.
            if (node == endNode)
            {
                endNode = null;
            }

            node = null;
        }

        // For selection grid system.
        UnitSelectionComponent selection = gameObject.GetComponent<UnitSelectionComponent>();
        List<Transform> selected = selection.getSelectedObjects();

        foreach (Transform nd in selected)
        {
            // Render the selected node to black.
            Renderer rend = nd.GetComponent<Renderer>();
            rend.material.color = Color.black;

            // Set selected node to not walkable
            DijkstraNode n = nd.GetComponent<DijkstraNode>();
            n.setWalkable(false);

            // Add the node to the block path list.
            blockPath.Add(nd);

            // If the block path is start node, we remove start node.
            if (nd == startNode)
            {
                startNode = null;
            }

            // If the block path is end node, we remove end node.
            if (nd == endNode)
            {
                endNode = null;
            }
        }

        selection.clearSelections();
    }

    /// <summary>
    /// Button to unblock a path.
    /// </summary>
    public void btnUnblockPath()
    {
        if (node != null)
        {
            // Set selected node to white.
            Renderer rend = node.GetComponent<Renderer>();
            rend.material.color = Color.white;

            // Set selected not to walkable.
            DijkstraNode n = node.GetComponent<DijkstraNode>();
            n.setWalkable(true);

            // Remove selected node from the block path list.
            blockPath.Remove(node);

            node = null;
        }

        // For selection grid system.
        UnitSelectionComponent selection = gameObject.GetComponent<UnitSelectionComponent>();
        List<Transform> selected = selection.getSelectedObjects();

        foreach (Transform nd in selected)
        {
            // Set selected node to white.
            Renderer rend = nd.GetComponent<Renderer>();
            rend.material.color = Color.white;

            // Set selected not to walkable.
            DijkstraNode n = nd.GetComponent<DijkstraNode>();
            n.setWalkable(true);

            // Remove selected node from the block path list.
            blockPath.Remove(nd);
        }

        selection.clearSelections();
    }

    /// <summary>
    /// Clear all the block path.
    /// </summary>
    public void btnClearBlock()
    {
        // For each blocked path in the list
        foreach (Transform path in blockPath)
        {
            // Set walkable to true.
            DijkstraNode n = path.GetComponent<DijkstraNode>();
            n.setWalkable(true);

            // Set their color to white.
            Renderer rend = path.GetComponent<Renderer>();
            rend.material.color = Color.white;

        }
        // Clear the block path list and 
        blockPath.Clear();
    }

    /// <summary>
    /// Button to restart level.
    /// </summary>
    public void btnRestart()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }

    /// <summary>
    /// Coloured unwalkable path to black.
    /// </summary>
    private void colorBlockPath()
    {
        foreach (Transform block in blockPath)
        {
            Renderer rend = block.GetComponent<Renderer>();
            rend.material.color = Color.black;
        }
    }

    /// <summary>
    /// Refresh Update Nodes Color.
    /// </summary>
    private void updateNodeColor()
    {
        if (startNode != null)
        {
            Renderer rend = startNode.GetComponent<Renderer>();
            rend.material.color = Color.blue;
        }

        if (endNode != null)
        {
            Renderer rend = endNode.GetComponent<Renderer>();
            rend.material.color = Color.cyan;
        }
    }

}
