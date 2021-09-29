using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : MonoBehaviour
{
    GameObject player;
    PlayerShooting playerShooting;

    void Awake()
    {
        // get gameobject player
        player = GameObject.FindGameObjectWithTag("Player");
        // get playerhealth
        playerShooting = player.GetComponentInChildren<PlayerShooting>();
    }

    // callback jika ada yang masuk trigger
    void OnTriggerEnter(Collider other)
    {
        // heal player 20hp
        if (other.gameObject == player && other.isTrigger == false)
        {
            playerShooting.damageUp();
            Destroy(this.gameObject);
        }
    }
}
