using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;                  
    public float timeBetweenBullets = 0.15f;        
    public float range = 100f;                      

    float timer;
    float damageUpTimer;
    float damageUpDuration = 6f;
    Ray shootRay = new Ray();                                   
    RaycastHit shootHit;                            
    int shootableMask;                             
    ParticleSystem gunParticles;                    
    LineRenderer gunLine;                           
    AudioSource gunAudio;                           
    Light gunLight;                                 
    float effectsDisplayTime = 0.2f;
    bool isDamageUp = false;

    void Awake()
    {
        // get mask
        shootableMask = LayerMask.GetMask("Shootable");

        // reference component
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        if (isDamageUp)
        {
            timeBetweenBullets = 0.1f;
            damageUpTimer += Time.deltaTime;
            if (damageUpTimer >= damageUpDuration)
            {
                isDamageUp = false;
                timeBetweenBullets = 0.15f;
                damageUpTimer = 0f;
            }
        }
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void damageUp()
    {
        Debug.Log("Revving Up! Fire rate increased greatly!");
        isDamageUp = true;
    }

    // untuk disable trace dan light
    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    // fungsi dipanggil ketika menembak
    public void Shoot()
    {
        // reset timer
        timer = 0f;

        // play audio
        gunAudio.Play();

        // enable muzzle flash
        gunLight.enabled = true;

        // play gun particle
        gunParticles.Stop();
        gunParticles.Play();

        // enable line renderer
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // set posisi ray
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // lakukan raycast jika terkena object
        //Debug.Log(shootableMask);
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //Debug.Log("Hitting");
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            // set line end position ke hit pos (enemy)
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            // jika tidak kena apa apa
            //Debug.Log("Missing");
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}