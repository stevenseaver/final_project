using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;

public class MenuManager : MonoBehaviour
{
    public Scene sceneTOload;
    //serial port initialization
    SerialPort sp = new SerialPort("/dev/cu.usbserial", 115200);

    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1; //timeout to stabilize serial readout
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Select Menu");
    }

    public void QuitGame()
    {
        sp.WriteLine("2");
        Application.Quit(1);     
    }
}
