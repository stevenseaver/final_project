using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Scene sceneTOload; //Add Scene to object inspector

    public void Level1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    
}
