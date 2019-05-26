using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeOrDeath : MonoBehaviour
{
    public static Transform renderedAirBar;

    public static float timeLeft = 1f; // at 1f, Air Bar is full
    float deltaTime = (1f / 1800f);

    public static float lives = 3; // number of lives of player
    public static bool isAlive = true; // True while player is alive, False when he loses a life
    public static bool lostLife; // Used to handle the lostLife trigger in the Animator (for Dying animation)

    // Start is called before the first frame update
    void Start()
    {
        renderedAirBar = transform.Find("renderedAirBar");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeLeft > 0.000001f && isAlive) // if there is remaining time and the player is alive
        {
            timeLeft -= deltaTime; // subtract some time
            renderedAirBar.localScale = new Vector3(timeLeft, 1f); // update Air Bar using the value of timeLeft
        }

        if (timeLeft < 0.000001f && lives > 0) // if Air Bar is empty and there are remaining lives
        {
            lives -= 1; // remove a life
            isAlive = false; // set isAlive boolean to false

            if (lives > 0) // if there still are remaining lives
            {
                StartCoroutine(AnotherChance()); // start the coroutine AnotherChance() found below
            }
        }
    }

    public static IEnumerator AnotherChance()
    {
        lostLife = true; // set lostLife to True
        timeLeft = 1f; // reset the time left

        yield return new WaitForSeconds(1); // wait for 1 second

        isAlive = true; // reset isAlive boolean
        lostLife = false; // set lostLife boolean back to False

        yield break;
    }

    // Note: this coroutine exists so that a delay can be set (needed to let the death animation play before the Air Bar refills itself)
    // Even though timeLeft is refilled before the delay, the actual Air Bar will only be displayed as full after isAlive has been set back to True

}
