using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreshAir : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Using the timeLeft variable from the LifeOrDeath script:
        LifeOrDeath.timeLeft += 0.2f; // increase the air supply by 20%

        if (LifeOrDeath.timeLeft > 1f)
        {
            LifeOrDeath.timeLeft = 1f; // prevent the air supply from exceeding its maximum capacity
        }

        LifeOrDeath.renderedAirBar.localScale = new Vector3(LifeOrDeath.timeLeft, 1f); // update the Air Bar

        Destroy(gameObject); // remove the air capsule from the game
    }
}
