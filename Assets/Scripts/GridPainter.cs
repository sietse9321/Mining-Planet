using UnityEngine;
using UnityEngine.Tilemaps;

public class GridPainter : MonoBehaviour
{
    //tile wich is placed
    [SerializeField] TileBase tile;
    //tilemap
    Tilemap tilemap;
    //width and height of the grid hight
    int gridWidth = 10;
    int gridHight = 10;
    float tileSize = -32f / 100f;


    void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHight; y++)
            {
                Vector3Int posistion = new Vector3Int(x-10, y-5, 0);
                tilemap.SetTile(posistion, tile);
            }
        }
    }
}
