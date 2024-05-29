using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //? ************************** dont use****************************
    [SerializeField] GameObject tilePrefab;
    SpriteRenderer tileImage;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Sprite[] topSprite;
    int gridWidth = 10;
    int gridHeight = 10;
    float tileSize = -32f / 100f;

    [SerializeField] Dictionary<Vector2, GameObject> gridTiles = new Dictionary<Vector2, GameObject>();

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        //x
        for (int x = 0; x < gridWidth; x++)
        {
            //y
            for (int y = 0; y < gridHeight; y++)
            {
                //new position for tile
                Vector2 pos = new Vector2(x * tileSize, y * tileSize);
                //creates new object using object the pos and base rotation
                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tile.name = $"{x}, {y}";
                tileImage = tile.GetComponent<SpriteRenderer>();
                if (y == 0)
                {
                    tileImage.sprite = topSprite[Random.Range(0, sprites.Length)];
                    tileImage.flipY = true;
                }
                else
                {
                    tileImage.sprite = sprites[Random.Range(0, sprites.Length)];
                }
                //add to dictionary using grid coordinates
                Vector2 gridPos = new Vector2(x, y);
                gridTiles.Add(gridPos, tile);

                // Set grid position on the tile script
                Tile tileScript = tile.GetComponent<Tile>();
                if (tileScript != null)
                {
                    //tileScript.SetGridPosition(gridPos);
                }
            }
        }
    }

    public void DeleteTileAtPos(Vector2 _pos)
    {
        if (gridTiles.ContainsKey(_pos))
        {
            Destroy(gridTiles[_pos]);
            gridTiles.Remove(_pos);
            ChangeSprites(_pos);
        }
    }

    void ChangeSprites(Vector2 _pos)
    {
        Vector2[] directions = {
            new Vector2(1, 0),  // Right
            new Vector2(-1, 0), // Left
            new Vector2(0, 1),  // Up
            new Vector2(0, -1)  // Down
        };

        foreach (Vector2 dir in directions)
        {
            Vector2 neighborPos = _pos + dir;

            if (gridTiles.ContainsKey(neighborPos))
            {
                GameObject neighborTile = gridTiles[neighborPos];
                print(neighborPos);
                SpriteRenderer neighborTileImage = neighborTile.GetComponent<SpriteRenderer>();
                neighborTileImage.sprite = topSprite[Random.Range(0, sprites.Length)];
                if(dir == new Vector2(1, 0))
                {
                    neighborTileImage.flipY = true;
                }
            }
        }
    }
}
