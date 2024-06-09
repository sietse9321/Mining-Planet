using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataSaver : MonoBehaviour
{
    GridPainter gridPainter;
    Tilemap map;
    PlayerData playerData = new PlayerData();
    string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/GameData.json";
        Debug.Log(filePath);
        SaveGame();
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
                    tileName = tile.name // Assumes that tile names are unique identifiers
                });
            }
        }

        //Transform playerTransform = FindObjectOfType<Player>().transform;
        //playerData.playerPosition = playerTransform.position;

        // Serialize to JSON and save
        string jsonData = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(filePath, jsonData);
    }
    public void LoadGame()
    {
        string jsonData = File.ReadAllText(filePath);
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);

        map.ClearAllTiles();
        //foreach tile data in playerdate.tiles
        TileBase tile;
        //if tilebase.tileName == "mine"
        //{
        //  map.setTile(tileData.Position, tile);
        //}
        
        //set player transform
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
