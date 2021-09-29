using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;

    void Awake()
    {
        // get gameobject player
        player = GameObject.FindGameObjectWithTag("Player");
        // get playerhealth
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // callback jika ada yang masuk trigger
    void OnTriggerEnter(Collider other)
    {
        // heal player 20hp
        if (other.gameObject == player && other.isTrigger == false)
        {
            playerHealth.Heal(20);
            Destroy(this.gameObject);
        }
    }
}

