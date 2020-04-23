using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    // serial PORT, when loading to main screen, pause serial comm
    SerialPort sp = new SerialPort("/dev/cu.usbserial", 115200);

    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1; //timeout to stabilize serial readout
    }

    // Update is called once per frame
    // gameIsOver checks if game state is over, if true, then we shouldn't load
    // PauseMenu. Hence, return null
    void Update()
    {
        if (GameOverMenu.gameIsOver == true)
            return;
        else
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }   
    }

    // Resume Gaming Runtime, setting pauseMenuUI disabled and gameIsPausde false,
    // then opens up the serial line with '1'
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

        gameIsPaused = false;
        Debug.Log("Resuming ...  ");
        sp.WriteLine("1"); //resume comm
    }
    //Pause Gaming Runtime
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        
        gameIsPaused = true;
        sp.WriteLine("2"); //turn off comm while paused
    }
    //load main menu when button menu pressed
    public void loadMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Load menu ...  ");
        SceneManager.LoadScene("Main Menu");

        gameIsPaused = true;
        sp.WriteLine("2");
    }
    //restart game
    public void Restart()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        //sp.WriteLine("1"); //resume comm

        gameIsPaused = false;
        Debug.Log("Restarting ...  ");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //quit game
    public void quitGame()
    {
        Application.Quit(1);
    }
}
