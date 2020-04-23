using UnityEngine;
using System.Collections;
using System.IO.Ports;


public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    SerialPort sp = new SerialPort("/dev/cu.usbserial", 115200);

    public Transform playerPrefab;
    public Transform spawnPoint;
    // spawnDelay to add dramatic punishment effect
    public float spawnDelay = 1.5f;

    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>() ;
        }
    }

    //function to respawn invoked by player being killed below      
    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    // KillPlayer invoked by PlayerCollision DamagePlayer function
    public static void KillPlayer(PlayerCollision player)
    {
        Destroy(player.gameObject); //destroy player on collision, see PLayerCollision.cs
        gm.StartCoroutine(gm.RespawnPlayer());
    }

}
