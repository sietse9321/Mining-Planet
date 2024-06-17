using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GridPainter gridPainter;
    [SerializeField] TileBase mine;
    [SerializeField] TileBase lampSprite;
    Tilemap map;
    PlayerData playerData = new PlayerData();
    string filePath;
    bool game = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Application.persistentDataPath + "/GameData.json";
            Debug.Log(filePath);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "MainMenu" && game == false)
        {
            game = true;
            InvokeRepeating("SaveGame", 5, 60);
        }
    }
    /// <summary>
    /// load previuous save game
    /// </summary>
    public void ContinueGame()
    {
        StartCoroutine(LoadSceneAndContinue("Sietse"));
    }
    private IEnumerator LoadSceneAndContinue(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        LoadGame();
    }

    /// <summary>
    /// Create new game
    /// </summary>
    public void NewGame()
    {
        StartCoroutine(LoadSceneAndStartNew("Sietse"));
    }
    private IEnumerator LoadSceneAndStartNew(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        NewGameSetup();
    }
    void NewGameSetup()
    {
        gridPainter = FindObjectOfType<GridPainter>();
        gridPainter.CreateMap();
    }
    void LoadGame()
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
            if (tileData.tileName == "Lamp")
            {
                tile = lampSprite;
            }
            else
            {
                tile = mine;
            }
            map.SetTile(tileData.position, tile);
        }
        Transform playerTransform = FindObjectOfType<Movement>().transform;
        playerTransform.position = playerData.playerPosition;
    }
    public void SaveGame()
    {
        Debug.Log($"<color={"#32a852"}>[SavingGame]</color>");
        gridPainter = FindObjectOfType<GridPainter>();
        map = gridPainter.minableMap;

        playerData.tiles = new List<TileData>();
        foreach (var pos in map.cellBounds.allPositionsWithin)
        {
            TileBase tile = map.GetTile(pos);
            if (tile != null)
            {
                playerData.tiles.Add(new TileData
                {
                    position = new Vector3Int(pos.x, pos.y, pos.z),
                    tileName = tile.name
                });
            }
        }

        Transform playerTransform = FindObjectOfType<Movement>().transform;
        playerData.playerPosition = playerTransform.position;

        filePath = Application.persistentDataPath + "/GameData.json";

        string jsonData = JsonUtility.ToJson(playerData, true);
        print(filePath);
        File.WriteAllText(filePath, jsonData);
    }
    public void Settings()
    {
        //turn on settings
    }
    public void SaveAndExit()
    {
        SaveGame();
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
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