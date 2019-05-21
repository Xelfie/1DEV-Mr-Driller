using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeOrDeath : MonoBehaviour
{
    public static Transform renderedAirBar;

    
    
    public static float timeLeft = 1f;
    float deltaTime = (1f / 1800f);


    // Start is called before the first frame update
    void Start()
    {
        renderedAirBar = transform.Find("renderedAirBar");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeLeft > 0.000001f)
        {
            timeLeft -= deltaTime;
            renderedAirBar.localScale = new Vector3(timeLeft, 1f);
        }
    }


}
