using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataSaver : MonoBehaviour
{
    [SerializeField] GridPainter gridPainter;
    Tilemap map;
    [SerializeField] TileBase mine;
    [SerializeField] TileBase lampSprite;
    PlayerData playerData = new PlayerData();
    string filePath;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        filePath = Application.persistentDataPath + "/GameData.json";
        Debug.Log(filePath);
        //SaveGame();
        //LoadGame();
    }
    public void SaveGame()
    {
        gridPainter = FindObjectOfType<GridPainter>();
        map = gridPainter.minableMap;
        // Collect tile data
        playerData.tiles = new List<TileData>();
        foreach (var pos in map.cellBounds.allPositionsWithin)
        {
            TileBase tile = map.GetTile(pos);
            if (tile != null)
            {
                playerData.tiles.Add(new TileData
                {
                    position = new Vector3Int(pos.x, pos.y),
                    tileName = tile.name
                });
            }
        }

        Transform playerTransform = FindObjectOfType<Movement>().transform;
        playerData.playerPosition = playerTransform.position;

        string jsonData = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(filePath, jsonData);
    }
    public void LoadGame()
    {
        gridPainter = FindObjectOfType<GridPainter>();
        map = gridPainter.minableMap;
        print("LoadingGame");
        string jsonData = File.ReadAllText(filePath);
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);

        map.ClearAllTiles();
        foreach (TileData tileData in playerData.tiles)
        {
            TileBase tile;
            if (tileData.tileName == "LampSprite")
            {
                tile = lampSprite;
            }
            else
            {
                tile = mine;
            }
            map.SetTile(tileData.position, tile);
        }
    }

    [System.Serializable]
    public struct TileData
    {
        public Vector3Int position;
        public string tileName;
    }

    [System.Serializable]
    public class PlayerData
    {
        public Vector3 playerPosition;
        public List<TileData> tiles;
    }
}