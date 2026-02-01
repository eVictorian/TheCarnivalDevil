using UnityEngine;
using UnityEngine.SceneManagement;

public enum Levels
{
    Menu = 0,
    Main = 1,
    Win = 2
}

public class SceneLoader : MonoBehaviour
{
    // Update is called once per frame
    static public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    static public void LoadSceneMode(Levels levelIndex)
    {
        SceneManager.LoadScene((int)levelIndex);
    }

    static public void Quit(){
        Application.Quit();
    }
}
