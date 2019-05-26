using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsManagement : MonoBehaviour
{
    public GameObject hearteu1, hearteu2, hearteu3;

    // Update is called once per frame
    void Update()
    {
        switch (LifeOrDeath.lives) // update number of hearts on screen according to number of lives of player
        {
            case 2:
                hearteu3.gameObject.SetActive(false);
                break;
            case 1:
                hearteu3.gameObject.SetActive(false);
                hearteu2.gameObject.SetActive(false);
                break;
            case 0:
                hearteu3.gameObject.SetActive(false);
                hearteu2.gameObject.SetActive(false);
                hearteu1.gameObject.SetActive(false);
                break;
        }
    }
}
