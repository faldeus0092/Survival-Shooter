using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stim : MonoBehaviour
{

    GameObject player;
    PlayerMovement playerMovement;
    float timer;


    void Awake()
    {
        // get gameobject player
        player = GameObject.FindGameObjectWithTag("Player");
        // get playerhealth
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // callback jika ada yang masuk trigger
    void OnTriggerEnter(Collider other)
    {
        // set player in range of attack
        if (other.gameObject == player && other.isTrigger == false)
        //if (other.gameObject == player)
        {
            playerMovement.Stim();
            Destroy(this.gameObject);
        }
    }

}

