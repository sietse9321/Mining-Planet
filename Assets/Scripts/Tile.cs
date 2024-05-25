using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GridManager gridManager;
    private Vector2 gridPosition;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void OnMouseDown()
    {
        gridManager.DeleteTileAtPos(gridPosition);
    }

    public void SetGridPosition(Vector2 pos)
    {
        gridPosition = pos;
    }
}
