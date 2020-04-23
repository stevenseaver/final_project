using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class GameOverMenu : MonoBehaviour
{
    public Transform rb;
    public Transform finishLine;

    public static bool gameIsOver = false;

    public GameObject gameOverUI;
    public GameObject scoreShow;

    public Text finalScoreShow;
    string finalScore;

    float nextTimeToSearch = 0;

    SerialPort sp = new SerialPort("/dev/cu.usbserial", 115200);

    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1; //timeout to stabilize serial readout
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //variables for rb.position.x
        if (rb == null)
        {
            FindPlayer();
            return;
        }

        float positionBody = rb.position.x;

        if (positionBody > finishLine.position.x)
        {
            //FindObjectOfType<GameMaster>().EndGame();
            if (gameIsOver == true)
            {
                return;
            }
            else
            {
                GameOver();
            }
        }
    }

    //finding player after respawn
    void FindPlayer()
    {
        if(nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                rb = searchResult.transform;
            nextTimeToSearch = Time.time + 0.5f; 
        }
    }  

    //Over Gaming Runtime
    void GameOver()
    {
         //when game over, stop serial comm
        gameOverUI.SetActive(true);
        scoreShow.SetActive(false);
        Time.timeScale = 0f;

        //Show last score
        finalScore = Score.score;
        finalScoreShow.text = finalScore;
        gameIsOver = true;
        sp.WriteLine("2");
    }
    //next level game
    public void NextLevel()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;

        gameIsOver = false;
        sp.WriteLine("1"); //turn off comm while paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    //Resume Gaming Runtime
    public void Restart()
    {
        gameOverUI.SetActive(false);
        scoreShow.SetActive(true);
        Time.timeScale = 1f;

        gameIsOver = false;
        Debug.Log("Restarting ...  ");
        sp.WriteLine("1"); //resume comm
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //load main menu when button menu pressed
    public void loadMenu()
    {
        Time.timeScale = 1f;
        sp.WriteLine("2");
        SceneManager.LoadScene("Main Menu");

        gameIsOver = false;
    }

    public void quitGame()
    {
        sp.Close();
        Application.Quit(1);
    }
}
