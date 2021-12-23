using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using Debug = UnityEngine.Debug;
public class AStarPath : MonoBehaviour {
    WriteToCSVFile writeToCSVFile;
    public GameObject csvObject;
    public DijkstraNode[] nodes;
    private float endTime;
    private float levelTimer = 0.0f;
    private float startTime;
    private float totalCost = 0;
    
    public List<Transform> finalPath = new List<Transform>();

    private void Awake() {
        writeToCSVFile = csvObject.GetComponent<WriteToCSVFile>();

    }

    public List<Transform> FindAStartPath(Transform start, Transform end) {

        startTime = Time.realtimeSinceStartup;

        DijkstraNode startNode = start.gameObject.GetComponent<DijkstraNode>();
        DijkstraNode endNode = end.gameObject.GetComponent<DijkstraNode>();

        GameObject[] objNodes = GameObject.FindGameObjectsWithTag("Node");
        nodes = new DijkstraNode[objNodes.Length];
        for(int i = 0; i < nodes.Length; i++) {
            nodes[i] = objNodes[i].GetComponent<DijkstraNode>();
        }
        List<DijkstraNode> openList = new List<DijkstraNode>();
        List<DijkstraNode> closedList = new List<DijkstraNode>();

        openList.Add(startNode);

        while(openList.Count > 0) {
            DijkstraNode currentNode = openList[0];
            for(int i = 1; i < openList.Count; i++) {
                if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].ihCost < currentNode.ihCost)//If the f cost of that object is less than or equal to the f cost of the current node
                {
                    currentNode = openList[i];//Set the current node to that object
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if(currentNode == endNode) {
                GetFinalPath(startNode, endNode);
                
            }

            foreach (DijkstraNode neighbourNode in currentNode.getDNeighbourNode()) {
                if(!neighbourNode.isWalkable() || closedList.Contains(neighbourNode)) {
                    continue;
                }

                int moveCost = currentNode.igCost + GetManhattenDistance(currentNode, neighbourNode);
                if(moveCost < neighbourNode.igCost || !openList.Contains(neighbourNode)) {
                    neighbourNode.igCost = moveCost;
                    neighbourNode.ihCost = GetManhattenDistance(neighbourNode, endNode);
                    neighbourNode.setParentNode(currentNode.transform);

                    if(!openList.Contains(neighbourNode))//If the neighbor is not in the openlist
                    {
                        openList.Add(neighbourNode);//Add it to the list
                    }
                }
            }
            levelTimer += Time.deltaTime;
        }
        endTime = Time.realtimeSinceStartup - startTime;
        writeToCSVFile.WriteCSV("A Star", levelTimer, totalCost, finalPath.Count, endTime);
        return finalPath;
    }

    void GetFinalPath(DijkstraNode startNode, DijkstraNode endNode) {
        Transform currentNode = endNode.transform;//Node to store the current node being checked

        while(currentNode != startNode.transform)//While loop to work through each node going through the parents to the beginning of the path
        {
            finalPath.Add(currentNode);//Add that node to the final path
            totalCost += GetTotalCost(currentNode.gameObject.GetComponent<DijkstraNode>(), currentNode.gameObject.GetComponent<DijkstraNode>().getParentNode().gameObject.GetComponent<DijkstraNode>());
            currentNode = currentNode.gameObject.GetComponent<DijkstraNode>().getParentNode();//Move onto its parent node
            
        }
        
        finalPath.Reverse();//Reverse the path to get the correct order
    }
    float GetTotalCost(DijkstraNode end, DijkstraNode begin)
    {
        int dx = Mathf.Abs(end.iGridX - begin.iGridX);//x1-x2
        int dy = Mathf.Abs(end.iGridY - begin.iGridY);//y1-y2
        float dist = Mathf.Sqrt(dx*dx + dy*dy);
		float cost = dist;
        return cost;
    }
    int GetManhattenDistance(DijkstraNode a_nodeA, DijkstraNode a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.iGridX - a_nodeB.iGridX);//x1-x2
        int iy = Mathf.Abs(a_nodeA.iGridY - a_nodeB.iGridY);//y1-y2

        return ix + iy;//Return the sum
    }
}