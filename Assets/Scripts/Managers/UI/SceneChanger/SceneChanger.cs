using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static string mainMenuScene = "MainMenuScene";
    private  static string playScene = "PlayScene";

    public static void LoadMainMenuScene()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public static void LoadPlayScene()
    {
        SceneManager.LoadScene(playScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
