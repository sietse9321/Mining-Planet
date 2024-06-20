using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        //singleton
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
    /// load previous save game
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
    /// <summary>
    /// finds gridpaint component and calls a method
    /// </summary>
    void NewGameSetup()
    {
        gridPainter = FindObjectOfType<GridPainter>();
        gridPainter.CreateMap();
    }
    /// <summary>
    /// loads data from json for player pos and grid
    /// </summary>
    void LoadGame()
    {
        gridPainter = FindObjectOfType<GridPainter>();
        map = gridPainter.minableMap;
        print("LoadingGame");
        string jsonData = File.ReadAllText(filePath);
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);

        //clears all tiles in map
        map.ClearAllTiles();
        //foreach tiledata in playerdata tiles
        foreach (TileData tileData in playerData.tiles)
        {
            //creates new tile
            TileBase tile;
            //if tilename is lamp
            if (tileData.tileName == "Lamp")
            {
                //sets tile sprite
                tile = lampSprite;
            }
            else
            {
                //sets tile sprite
                tile = mine;
            }
            //sets the tile using the just created tile with the position
            map.SetTile(StringToVector3Int(tileData.positionString), tile);
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
        //foreach pos in map
        foreach (Vector3Int pos in map.cellBounds.allPositionsWithin)
        {
            //tile is met position
            TileBase tile = map.GetTile(pos);
            //if tile is not null
            if (tile != null)
            {
                //adds a new tile to tiledata
                playerData.tiles.Add(new TileData
                {
                    positionString = Vector3IntToString(pos),
                    tileName = tile.name
                });
            }
        }
        //gets and sets the player position in playerData
        Transform playerTransform = FindObjectOfType<Movement>().transform;
        playerData.playerPosition = playerTransform.position;
        //filepath to use
        filePath = Application.persistentDataPath + "/GameData.json";
        //to json
        string jsonData = JsonUtility.ToJson(playerData, true);
        print(filePath);
        //writes all text
        File.WriteAllText(filePath, jsonData);
    }
    public void Settings()
    {
        //turn on settings
    }
    public void Exit()
    {
        //cancel invoke to not save in main menu
        CancelInvoke();
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        //quits the application
        Application.Quit();
    }

    string Vector3IntToString(Vector3Int vector)
    {
        return $"{vector.x},{vector.y},{vector.z}";
    }

    Vector3Int StringToVector3Int(string vectorString)
    {
        string[] values = vectorString.Split(',');
        return new Vector3Int(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
    }
}

[System.Serializable]
public struct TileData
{
    public string positionString;
    public string tileName;
}

[System.Serializable]
public class PlayerData
{
    public Vector3 playerPosition;
    public List<TileData> tiles;
}
