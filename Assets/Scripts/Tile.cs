using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private GridManager gridManager;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void OnMouseDown()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        gridManager.DeleteTileAtPos(position);
    }
}
