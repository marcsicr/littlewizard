using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent (typeof(Grid))]
public class ZoneGrid : MonoBehaviour
{
    public Tilemap[] collisionTileMaps;
    private Grid grid;
    void Start() {

        grid = GetComponent<Grid>();
        foreach (Tilemap map in collisionTileMaps) {
            map.CompressBounds();
        }
    }

    public Vector3Int getTileCoord(Vector3 position) {
        return grid.WorldToCell(position);
    }

    public bool isWalkable(Vector3 position) {

        Vector3Int cellPos = grid.WorldToCell(position);
        return isWalkable(cellPos);
    }
    public bool isWalkable(Vector3Int cell) {

        foreach(Tilemap map in collisionTileMaps) {

            if (map.GetTile(cell)!= null)
                return false;
        }

        return true;
        
    }

    public Vector3Int tryGetWalkableNeigbour(Vector2 targetPosition,Vector3Int cell) {

        LayerMask mask = LayerMask.GetMask("Wall", "Background");
        int height = LevelManager.Instance.getTileLevel(grid.CellToWorld(cell));
        float rayLength = 1f;

        if (!Physics2D.Raycast(targetPosition, Vector2.up, rayLength, mask)) {

            if (isWalkable(cell + Vector3Int.up)) {

                if (height == LevelManager.Instance.getTileLevel(grid.CellToWorld(cell + Vector3Int.up)))
                    return cell + Vector3Int.up;
            }
        }
        
       
        if(!Physics2D.Raycast(targetPosition, Vector2.down, rayLength, mask)) {

            if (isWalkable(cell + Vector3Int.down)) {

                if (height == LevelManager.Instance.getTileLevel(grid.CellToWorld(cell + Vector3Int.down)))
                    return cell + Vector3Int.down;
            }
        }
       
         if(!Physics2D.Raycast(targetPosition, Vector2.left, rayLength, mask)){

            if (isWalkable(cell + Vector3Int.left)) {

                if (height == LevelManager.Instance.getTileLevel(grid.CellToWorld(cell + Vector3Int.left)))
                    return cell + Vector3Int.left;
            }

         }
       
         if(!Physics2D.Raycast(targetPosition, Vector2.right, rayLength, mask)) {

            if (isWalkable(cell + Vector3Int.right)) {

                if (height == LevelManager.Instance.getTileLevel(grid.CellToWorld(cell + Vector3Int.right)))
                    return cell + Vector3Int.right;
            }
         }

        if (!Physics2D.Raycast(targetPosition, Vector2.up + Vector2.right, rayLength, mask)) {

            if (isWalkable(cell + Vector3Int.up + Vector3Int.right)) {

                if (height == LevelManager.Instance.getTileLevel(grid.CellToWorld(cell + Vector3Int.up + Vector3Int.right)))
                    return cell + Vector3Int.up + Vector3Int.right;
            }
        }

        if (!Physics2D.Raycast(targetPosition, Vector2.up + Vector2.left, rayLength, mask)) {

            if (isWalkable(cell + Vector3Int.up + Vector3Int.left)) {

                if (height == LevelManager.Instance.getTileLevel(grid.CellToWorld(cell + Vector3Int.up + Vector3Int.left)))
                    return cell + Vector3Int.up + Vector3Int.left;
            }
        }

        if (!Physics2D.Raycast(targetPosition, Vector2.down + Vector2.right, rayLength, mask)) {

            if (isWalkable(cell + Vector3Int.down + Vector3Int.right)) {

                if (height == LevelManager.Instance.getTileLevel(grid.CellToWorld(cell + Vector3Int.down + Vector3Int.right)))
                    return cell + Vector3Int.down + Vector3Int.right;
            }
        }

        if (!Physics2D.Raycast(targetPosition, Vector2.down + Vector2.left, rayLength, mask)) {

            if (isWalkable(cell + Vector3Int.down + Vector3Int.left)) {

                if (height == LevelManager.Instance.getTileLevel(grid.CellToWorld(cell + Vector3Int.down + Vector3Int.left)))
                    return cell + Vector3Int.down + Vector3Int.left;
            }
        }

        return cell; 
    }
    
    public Vector3Int getCellCoord(Vector3 position ) {
        return grid.WorldToCell(position);
    }

    public Vector3 cellToWorld(Vector3Int cell) {

        return grid.CellToWorld(cell);
    }
}
