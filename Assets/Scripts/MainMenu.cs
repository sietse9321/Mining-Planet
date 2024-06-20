using UnityEngine;

public class MainMenu : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void ContinueGame()
    {
        gameManager.ContinueGame();
    }
    public void NewGame()
    {
        gameManager.NewGame();
    }
    public void Settings()
    {
        gameManager.Settings();
    }
    public void QuitGame()
    {
        gameManager.QuitGame();
    }
}