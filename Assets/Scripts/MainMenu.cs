using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    DataSaver dataSaver;
    public void ContinueGame()
    {
        SceneManager.LoadScene("Sietse");
        dataSaver.LoadGame();
    }
    public void NewGame()
    {

    }
    public void Settings()
    {
        //turn on settings
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Awake()
    {
        dataSaver = FindObjectOfType<DataSaver>();
    }
}
