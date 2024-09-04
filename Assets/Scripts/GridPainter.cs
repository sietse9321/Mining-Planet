using UnityEngine;
using UnityEngine.Tilemaps;

public class GridPainter : MonoBehaviour
{
    //tile wich is placed
    [SerializeField] TileBase tile;
    [SerializeField] TileBase[] background;
    //tilemap
    public Tilemap minableMap;
    [SerializeField] Tilemap backgroundMap;
    //width and height of the grid hight
    int gridWidth = 150;
    int gridHeight = 40;
    private void Start()
    {
        //generate the background using width and height
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                //sets the pos
                Vector3Int backgroundPos = new Vector3Int(x - 50, y - 40, 0);
                //the first row
                if (y == (gridHeight - 1))
                {
                    backgroundMap.SetTile(backgroundPos, background[1]);
                }
                else
                {
                    backgroundMap.SetTile(backgroundPos, background[0]);
                }
            }
        }
    }
    public void CreateMap()
    {
        //generate grid by loops
            //generates loop from left to right and down to up
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3Int posistion = new Vector3Int(x - 50, y - 40, 0);
                minableMap.SetTile(posistion, tile);
            }
        }
    }
}