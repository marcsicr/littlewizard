using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathsManager : MonoBehaviour {

    public void ComputePath(Vector3 start, Vector3 target, Action<Vector3[]> callbackFunc) {

        Debug.Log("PathsManager::ComputePath()");
        //Start and target must be of same zone;
        Zone startZone = LevelManager.Instance.getZone(start);
        Zone goalZone = LevelManager.Instance.getZone(target);
        if (startZone != goalZone) {

            Debug.Log("PathsManager::ComputePath() target is on another zone");
            return;

        }
            

        ZoneGrid grid = startZone.getZoneGrid();

        Vector3Int startCell = grid.getCellCoord(start);
        Vector3Int endCell = grid.getCellCoord(target);

        if (!grid.isWalkable(start)) {

            Vector3Int startNeighbour = grid.tryGetWalkableNeigbour(start, startCell);
            if(startNeighbour == startCell) {
                Debug.Log("PathsManager::ComputePath()->Start on non walkable cell");
                return;
            }  
        }

     

        if (!grid.isWalkable(target)) {

            Vector3Int neighbour = grid.tryGetWalkableNeigbour(target,endCell);
            if(neighbour == endCell) {

                Debug.Log("PathsManager End on non walkable cell");
                return;
            }

            Debug.Log("PathsManger to target neigbour cell");
            endCell = neighbour;

        }

        StartCoroutine(AStarCo(startCell, endCell,grid,callbackFunc));

    }

    private IEnumerator AStarCo(Vector3Int start, Vector3Int goal, ZoneGrid grid,Action<Vector3[]> callbackFunc) {

        Debug.Log("AStar...");
        float startTime = Time.time;
        MinHeap<ANode> openSet = new MinHeap<ANode>();
        Dictionary<Vector3Int, ANode> closedSet = new Dictionary<Vector3Int, ANode>();
        int maxNodes = 25;
        int nodesVisited = 0;
        openSet.Add(new ANode(start, goal));

        ANode current = null;
        while (openSet.GetSize() > 0) {

            if (nodesVisited >= maxNodes) {

                nodesVisited = 0;
                yield return null;
            }

            current = openSet.PopMin();
            if (current.cell.Equals(goal)) {
                Debug.Log("AStar reached goal");
                break;
            }

            List<ANode> neighbours = getReachableNodes(current,grid,goal);

            nodesVisited++;

            foreach (ANode n in neighbours) {

                ANode setNode = openSet.Get(n);
                if (setNode != null) {

                    if (setNode.g <= n.g) {
                        continue;
                    } else {

                        ANode closedNode;
                        closedSet.TryGetValue(n.cell, out closedNode);
                        if (closedNode != null) {

                            if (closedNode.g <= n.g) {
                                continue;
                            }

                            //Move successor from closed list to open list
                            closedSet.Remove(n.cell);
                            openSet.Add(n);
                        }

                    }
                } else {

                    openSet.Add(n);
                }
            }

            if (!closedSet.ContainsKey(current.cell))
                closedSet.Add(current.cell, current);

        }

        if (current == null || !current.cell.Equals(goal)) {
            Debug.Log("Path not found. OpenList is Empty");
            yield break;

        }

        Stack<Vector3> path = new Stack<Vector3>();
      

        while (!current.parent.cell.Equals(current.cell)) {

            path.Push(grid.cellToWorld(current.cell));
            //Debug.Log(node.cell);
            current = current.parent;

        }

        

        Debug.Log("AStar End" + (Time.time - startTime));
        callbackFunc.Invoke(path.ToArray());
    }


    private List<ANode> getReachableNodes(ANode node, ZoneGrid grid, Vector3Int dest) {
        List<ANode> res = new List<ANode>();


        Vector3Int left = node.cell; left.x -= 1;
        Vector3Int right = node.cell; right.x += 1;
        Vector3Int up = node.cell; up.y += 1;
        Vector3Int down = node.cell; down.y -= 1;

        Vector3Int upleft = node.cell; upleft.x -= 1; upleft.y += 1;
        Vector3Int upright = node.cell; upright.x += 1; upright.y += 1;
        Vector3Int downleft = node.cell; downleft.x -= 1; downleft.y -= 1;
        Vector3Int downright = node.cell; downright.x += 1; downright.y -= 1;


        if (grid.isWalkable(left)) {
            res.Add(new ANode(left, node, dest));
        }

        if (grid.isWalkable(right)) {
            res.Add(new ANode(right, node, dest));
        }

        if (grid.isWalkable(up)) {
            res.Add(new ANode(up, node, dest));
        }

        if (grid.isWalkable(down)) {
            res.Add(new ANode(down, node, dest));
        }

        if (grid.isWalkable(upleft)) {
            res.Add(new ANode(upleft, node, dest));
        }

        if (grid.isWalkable(upright)) {
            res.Add(new ANode(upright, node, dest));
        }

        if (grid.isWalkable(downleft)) {
            res.Add(new ANode(downleft, node, dest));
        }

        if (grid.isWalkable(downright)) {
            res.Add(new ANode(downright, node, dest));
        }

        return res;

    }

    private class ANode : IComparable, IEquatable<ANode> {


        public ANode parent;
        public Vector3Int cell;

        //Cost so far to reach this node
        public float g;

        //Estimated cost from n to goal
        public float h;

        public ANode(Vector3Int cell, Vector3Int dest) {

            this.cell = cell;
            parent = this;
            g = 0;
            h = computeH(dest);
        }

        public ANode(Vector3Int cell, ANode parent, Vector3Int dest) {

            this.cell = cell;
            this.parent = parent;
            g = parent.g + 1;
            h = computeH(dest);
        }

        private float computeH(Vector3Int dest) {
            return Vector3Int.Distance(cell, dest);
        }

        //Total estimated cost
        float f() { return g + h; }

        public int CompareTo(object obj) {

            if (obj == null) return 1;

            ANode otherNode = obj as ANode;
            if (otherNode != null) {

                if (this.f() > otherNode.f()) {

                    return 1;
                }

                if (this.f() == otherNode.f()) {
                    return 0;
                }


                if (this.f() < otherNode.f()) {

                    return -1;
                }

            }

            throw new ArgumentException("Object is not a ANode");
        }

        public bool Equals(ANode other) {

            if (cell == other.cell)
                return true;

            return false;
        }
    }
}
