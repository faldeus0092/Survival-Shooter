using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    {
        // get gameobject player
        player = GameObject.FindGameObjectWithTag ("Player");
        // get playerhealth
        playerHealth = player.GetComponent <PlayerHealth> ();
        // get enemy health component
        enemyHealth = GetComponent<EnemyHealth>();
        // get animator
        anim = GetComponent <Animator> ();
    }

    // callback jika ada yang masuk trigger
    void OnTriggerEnter (Collider other)
    {
        // set player in range of attack
        if(other.gameObject == player && other.isTrigger == false)
        //if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    // jika object keluar trigger
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
