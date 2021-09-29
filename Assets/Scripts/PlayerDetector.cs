using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    // untuk show warning
    public GameOverManager gameOverManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && !other.isTrigger)
        {
            float enemyDistance = Vector3.Distance(transform.position, other.transform.position);
            // jika yang masuk collider merupakan object dengan tag enemy, show warning
            gameOverManager.ShowWarning(enemyDistance);
        }
    }
}
