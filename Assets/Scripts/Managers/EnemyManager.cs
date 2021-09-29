using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    // unga bunga factory stuff
    /*[SerializeField]
    EnemyFactory factory;
    IFactory Factory { get { return factory as IFactory; } }
    */
    void Start ()
    {
        // jalankan fungsi spawn setiap beberapa detik (spawntime)
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        // jika player telah mati
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        // spawn di random tempat
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        int spawnEnemy = Random.Range(0, 3);

        // spawn random
        // ??????
        // this thing doesnt even work
        //Factory.FactoryMethod(spawnEnemy);

        // spawn dari sarangnya
        // jangan lupa di instantiate
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

    }
}
