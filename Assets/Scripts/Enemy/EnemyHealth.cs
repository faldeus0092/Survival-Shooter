using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public GameObject powerUp;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        // ref komponen: animator, audio source, particle, collider
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        
        currentHealth = startingHealth;
    }


    void Update ()
    {
        // jika tenggelam
        if (isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        // cek jika mati
        if (isDead)
            return;

        // take damage
        enemyAudio.Play ();
        // decrease health by amount
        currentHealth -= amount;

        // play hit particles
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        // change to death if health less than/equal 0
        if (currentHealth <= 0)
        {
            Death ();
        }
    }

    // death
    void Death ()
    {
        // change status
        isDead = true;

        // set capcollider ke trigger
        capsuleCollider.isTrigger = true;

        // play animation
        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();

        Vector3 temp = this.transform.position;
        temp.y += 1.0f;
        Instantiate(powerUp, temp, Quaternion.identity);
    }


    public void StartSinking ()
    {
        //disable navmesh
        GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;
        // set kinematic
        GetComponent<Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
