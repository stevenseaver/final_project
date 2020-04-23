using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int Health = 100;
    }

    private void Start()
    {
        playerStats.Health = 100;
    }

    // instance of playerStats class that includes player health that
    // we want to access on DamagePLayer function
    public PlayerStats playerStats = new PlayerStats();

    //This function runs when we hit another object, which invoke DamagePlayer function
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "Enemy")  //compare tag, if tage from collider.info is "Enemy",
        //do the following    
        {
            DamagePlayer(999);  //if hit enemy, reduce health to infinity to destroy player
        }
    }

    // if damage < 0  because of collision, we invoke KillPlayer on GameMaster script
    // this way, we could do the same with respawning by only referencing to
    // Game Master Script
    public void DamagePlayer (int damage)
    {
        playerStats.Health -= damage;
        if (playerStats.Health <= 0)
        {
            Debug.Log("Kill Player..");
            GameMaster.KillPlayer(this);
        }
    }

}
