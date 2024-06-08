using UnityEngine;
using UnityEngine.Tilemaps;

public class DataSaver : MonoBehaviour
{
    GridPainter gridPainter;
    Tilemap map;
    public void SaveGame()
    {
        gridPainter = FindObjectOfType<GridPainter>();
        map = gridPainter.minableMap;
    }
    [System.Serializable]
    public struct PlayerData
    {
        public Vector3 playerPosition;
        public Vector3Int tilePositionFG;
    }
}
