using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FindingRespawn : MonoBehaviour
{
    private CinemachineVirtualCamera Vcam;

    private Transform Player;

    float nextTimeToSearch = 0;

    // Start is called before the first frame update
    void Start()
    {
        Vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player == null)
        {
            FindPlayer();
            return;
        }
    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                Player = searchResult.transform;
            nextTimeToSearch = Time.time + 0.5f;
        }
        Vcam.Follow = Player;
    }

}
