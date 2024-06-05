using UnityEngine;
using UnityEngine.Tilemaps;

public class GridPainter : MonoBehaviour
{
    //tile wich is placed
    [SerializeField] TileBase tile;
    //tilemap
    Tilemap tilemap;
    //width and height of the grid hight
    int gridWidth = 150;
    int gridHeight = 40;

    void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
        //generate grid by loops
        //generates loop from left to right and down to up
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3Int posistion = new Vector3Int(x-gridHeight, y-(gridWidth/4), 0);
                tilemap.SetTile(posistion, tile);
            }
        }
    }
}