using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraNode : MonoBehaviour {

    public int iGridX;//X Position in the Node Array
    public int iGridY;//Y Position in the Node Array
    [SerializeField] private float weight = int.MaxValue;
    [SerializeField] private Transform parentNode = null;
    [SerializeField] private List<Transform> neighbourNode;
    [SerializeField] private List<DijkstraNode> dNeighbourNode;
    [SerializeField] private bool walkable = true;
    
    public int igCost;//The cost of moving to the next square.
    public int ihCost;//The distance to the goal from this node.

    public int FCost { get { return igCost + ihCost; } }//Quick get function to add G cost and H Cost, and since we'll never need to edit FCost, we dont need a set function.


	// Use this for initialization
	void Start () {
        this.resetNode();
    }

    /// <summary>
    /// Reset all the values in the nodes.
    /// </summary>
    public void resetNode()
    {
        weight = int.MaxValue;
        parentNode = null;
    }

    // -------------------------------- Setters --------------------------------

    /// <summary>
    /// Set the parent node.
    /// </summary>
    /// <param name="node">Set the node for parent node.</param>
    public void setParentNode(Transform node)
    {
        this.parentNode = node;
    }

    /// <summary>.
    /// Set the weight value
    /// </summary>
    /// <param name="value">weight value</param>
    public void setWeight(float value)
    {
        this.weight = value;
    }

    /// <summary>
    /// Set is node is walkable.
    /// </summary>
    /// <param name="value">boolean</param>
    public void setWalkable(bool value)
    {
        this.walkable = value;
    }

    /// <summary>
    /// Adding neighbour node object.
    /// </summary>
    /// <param name="node">Node transform</param>
    public void addNeighbourNode(Transform node)
    {
        this.neighbourNode.Add(node);
        this.dNeighbourNode.Add(node.gameObject.GetComponent<DijkstraNode>());
    }

    // -------------------------------- Getters --------------------------------

    /// <summary>
    /// Get neighbour node.
    /// </summary>
    /// <returns>All the nodes.</returns>
    public List<Transform> getNeighbourNode()
    {
        List<Transform> result = this.neighbourNode;
        return result;
    }

    public List<DijkstraNode> getDNeighbourNode() {
        List<DijkstraNode> result = this.dNeighbourNode;
        return result;
    }

    /// <summary>
    /// Get weight
    /// </summary>
    /// <returns>get weight in float.</returns>
    public float getWeight()
    {
        float result = this.weight;
        return result;

    }

    /// <summary>
    /// Get the parent Node.
    /// </summary>
    /// <returns>Return the parent node.</returns>
    public Transform getParentNode()
    {
        Transform result = this.parentNode;
        return result;
    }


    // -------------------------------- Checkers --------------------------------

    /// <summary>
    /// Get if the node is walkable.
    /// </summary>
    /// <returns>boolean</returns>
    public bool isWalkable()
    {
        bool result = walkable;
        return result;
    }
}
