using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private string mainMenuScene = "MainMenuScene";
    private string playScene = "PlayScene";

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene(playScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
