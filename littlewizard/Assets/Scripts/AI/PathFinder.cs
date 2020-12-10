using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PathFinder : MonoBehaviour
{

    public Transform target;
    private Vector3Int targetCell;


    private Stack<Vector3> path;

    public Grid grid;
    public Tilemap walkCollision;
    public Tilemap wallCollision;

    // Start is called before the first frame update

    private void Awake() {
        path = new Stack<Vector3>();

    }
    void Start()
    {
        walkCollision.CompressBounds();
        wallCollision.CompressBounds();
       
    }

    // Update is called once per frame
    void Update()
    {
        targetCell = grid.WorldToCell(target.position);
        Vector3Int currentCell = grid.WorldToCell(transform.position);

        if (Input.GetKeyDown(KeyCode.Z)) {


            if (isWalkable(currentCell) && isWalkable(targetCell)) {
               
                StopAllCoroutines();
                StartCoroutine(FollowTargetCo(currentCell,targetCell));
            }
            else
                Debug.Log("Targt or destination on non walkable cell");

        }
        
    }


    public List<ANode> getReachableNodes(ANode node,Vector3Int dest) {
        List<ANode> res = new List<ANode>();


        Vector3Int left = node.cell; left.x -= 1;
        Vector3Int right = node.cell; right.x += 1;
        Vector3Int up = node.cell; up.y += 1;
        Vector3Int down = node.cell; down.y -= 1;

        Vector3Int upleft = node.cell; upleft.x -= 1; upleft.y += 1;
        Vector3Int upright = node.cell; upright.x += 1; upright.y += 1;
        Vector3Int downleft = node.cell; downleft.x -= 1; downleft.y -= 1;
        Vector3Int downright = node.cell; downright.x += 1; downright.y -= 1;

     
        if (isWalkable(left)) {
            res.Add(new ANode(left, node,dest));
        }

        if (isWalkable(right)) {
            res.Add(new ANode(right, node,dest));
        }

        if (isWalkable(up)) {
            res.Add(new ANode(up, node,dest));
        }

        if (isWalkable(down)) {
            res.Add(new ANode(down, node,dest));
        }

        if (isWalkable(upleft)) {
            res.Add(new ANode(upleft, node,dest));
        }

        if (isWalkable(upright)) {
            res.Add(new ANode(upright, node,dest));
        }

        if (isWalkable(downleft)) {
            res.Add(new ANode(downleft, node,dest));
        }

        if (isWalkable(downright)) {
            res.Add(new ANode(downright, node,dest));
        }

        return res;

    }


    public bool isWalkable(Vector3Int cellPos) {

        if (walkCollision.GetTile(cellPos) != null || wallCollision.GetTile(cellPos) != null)
            return false;

        return true;

    }

    private void OnDrawGizmos() {
       

        if (!Application.isPlaying) return;

        
        if (path.Count > 0) {
            Vector3[] array = path.ToArray();
            for (int i = 1; i < array.Length;i++) {

                Gizmos.DrawLine(array[i-1], array[i]);
            }
        }   
    }

    private IEnumerator FollowTargetCo(Vector3Int current,Vector3Int target) {

        yield return StartCoroutine(AStar(current, target));
        float maxStep = 15f;
        Vector3[] array = path.ToArray();
        for (int i = 0; i < array.Length; i++) {

            while(Vector3.Distance(transform.position,array[i]) > 0.05f){
                  transform.position = Vector3.MoveTowards(transform.position, array[i], maxStep*Time.deltaTime);
                  yield return null;
            }    
        }

        yield return null;
    }


    public IEnumerator AStar(Vector3Int start, Vector3Int goal) {

        Debug.Log("AStar...");
        float startTime = Time.time;
        MinHeap<ANode> openSet = new MinHeap<ANode>();
        Dictionary<Vector3Int, ANode> closedSet = new Dictionary<Vector3Int, ANode>();
        int maxNodes = 25;
        int nodesVisited = 0;
        openSet.Add(new ANode(start,goal));
        
        ANode current = null;
        while (openSet.GetSize() > 0) {

            if(nodesVisited >= maxNodes) {

                nodesVisited = 0;
                yield return null;
            }

            current = openSet.PopMin();
            if (current.cell.Equals(goal)) {
                Debug.Log("AStar reached goal");
                break;
            }
             
            List<ANode> neighbours = getReachableNodes(current,goal);

            nodesVisited++;

            foreach(ANode n in neighbours) {

                ANode setNode = openSet.Get(n);
                if (setNode != null) {
                    
                    if (setNode.g <= n.g) {
                        continue;
                    }else {

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

            if(!closedSet.ContainsKey(current.cell))
                closedSet.Add(current.cell,current);

        }

        if(current!= null && !current.cell.Equals(goal)) {
            Debug.Log("Path not found. OpenList is Empty");

        } else {

            updatePath(current);
        }

        Debug.Log("AStar End" + (Time.time-startTime));
    }

    public void updatePath(ANode node) {

        path.Clear();
        while (!node.parent.cell.Equals(node.cell)){

            path.Push(grid.CellToWorld(node.parent.cell));
            //Debug.Log(node.cell);
            node = node.parent;

        }
    }

    public class ANode : IComparable, IEquatable<ANode>{


        public ANode parent;
        public Vector3Int cell;
        
        //Cost so far to reach this node
        public float g;

        //Estimated cost from n to goal
        public float h;

        public ANode(Vector3Int cell,Vector3Int dest) {
            
            this.cell = cell;
            parent = this;
            g = 0;
            h = computeH(dest);
        }

        public ANode(Vector3Int cell,ANode parent,Vector3Int dest) {

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

                if(this.f() > otherNode.f()) {

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
