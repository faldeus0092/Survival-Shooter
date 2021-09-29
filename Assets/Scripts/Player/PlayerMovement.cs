using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;
    bool isStim = false;
    float stimDuration = 6f;
    float timer;

    private void Awake()
    {
        // get mask value from floor layer
        floorMask = LayerMask.GetMask("Floor");
        // get animator component
        anim = GetComponent<Animator>();
        // get rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isStim)
        {
            speed = 20f;
            timer += Time.deltaTime;
            if (timer >= stimDuration)
            {
                isStim = false;
                speed = 6f;
                timer = 0f;
            }
        }
        // get horizontal input value (-1,0,1)
        float h = Input.GetAxisRaw("Horizontal");
        // get vertical input value (-1,0,1)
        float v = Input.GetAxisRaw("Vertical");


        // saya disable dulu move sama animating karena yang jalanin input handler
        // untuk gerak
        //Move(h, v);
        // untuk rotasi
        Turning();
        // untuk animasi
        //Animating(h, v);
    }

    public void Stim()
    {
        Debug.Log("Stimming! Movement speed increased greatly!");
        isStim = true;
    }

    public void Animating(float h, float v)
    {
        // jika vektor horizontal dan vertikal tidak memiliki nilai, set iswalking menjadi true
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

    void Turning()
    {
        // buat ray dari posisi mouse di layar
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Buat raycast untuk floorhit
        RaycastHit floorHit;

        // do raycast
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // vector posisi player
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            // mendapatkan look rotation baru ke hit position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            //rotate player berdasarkan mouse
            playerRigidbody.MoveRotation(newRotation);

        }
    }

    public void Move(float h, float v)
    {
        // set nilai x dan y
        movement.Set(h, 0f, v);

        // normalisasi agar total panjang vektor = 1
        movement = movement.normalized * speed * Time.deltaTime;

        // move to position
        playerRigidbody.MovePosition(transform.position + movement);
    }
}
