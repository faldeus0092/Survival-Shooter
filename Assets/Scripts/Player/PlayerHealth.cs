using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    InputHandler inputHandler;
    bool isDead;                                                
    bool damaged;                                               


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        // input handler
        inputHandler = GetComponent<InputHandler>();

        currentHealth = startingHealth;
    }


    void Update()
    {
        if (damaged)
        {
            // ubah warna jadi flash colour
            damageImage.color = flashColour; 
        }
        else
        {
            // fade out?
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    // fungsi untuk mendapat damage
    public void TakeDamage(int amount)
    {
        damaged = true;
        
        // amount damage
        currentHealth -= amount;
        
        // change slider value
        healthSlider.value = currentHealth;
        
        // sound effect
        playerAudio.Play();

        // jika player mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Heal(int amount)
    {
        Debug.Log("Healing! HP healed by 20!");
        damaged = true;

        // amount damage
        currentHealth += amount;

        if (currentHealth > 100)
            currentHealth = 100;

        // change slider value
        healthSlider.value = currentHealth;

        // sound effect
        playerAudio.Play();

        // jika player mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        // disable input handler
        inputHandler.enabled = false;
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        // load scene 01
        SceneManager.LoadScene("Level 01");
    }
}
