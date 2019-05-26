using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsBlockFalling : MonoBehaviour
{
    private Rigidbody2D blockRigidbody;
    Vector3 blockPos;

    public static bool fallingBlock; // True if the block above the player is falling, else False

    // Start is called before the first frame update
    void Start()
    {
        blockRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        blockPos = gameObject.transform.position; // get position of block

        if (blockPos.y > Player.playerPos.y) // if block's y position is higher than player's y position
        {
            if ((Player.playerPos.x >= blockPos.x - 1.23f) && (Player.playerPos.x <= blockPos.x + 1.23f)) // and if player is located below the block
            {
                if (blockRigidbody.velocity.y < -1.5f) // check if that block is falling. If yes,
                {
                    if (!fallingBlock) // and if fallingBlock isn't True yet,
                    {
                        fallingBlock = true; // set it to True
                    }
                }

                else // else, if block isn't falling,
                {
                    if (fallingBlock) // and if fallingBlock is currently True,
                    {
                        fallingBlock = false; // set it to False
                    }
                }
            }
        }
    }    
}
