using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    [SerializeField] private float movementSpeed;
    private bool jump;
    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        facingRight = true;
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        inputHandler(); //takes the input at each frames
    }
    /* FixedUpdate is called once every time steps
    time steps = 0.0166666666666667 (60fps)
    can be changed in edit > Project Settings... > VFX > Fixed Time Steps*/
    void FixedUpdate()
    {
        /*get the horizontal axis settings in:
        edit > Project Settings... > Input > Axes > Horizontal*/
        float horizontal = Input.GetAxis("Horizontal");
        movements(horizontal); //handles all of the player movements
        flipPlayer(horizontal); //says where the player is facing(if it's right or left)
        resetTriggerValues(); //reset trigger value of trigger parameter in animator
    }

    private void movements(float horizontal) //handles all of the player movements
    {
        playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, playerRigidbody.velocity.y); //vector of x value = -1 and y = y

        playerAnimator.SetFloat("speed",Mathf.Abs(horizontal)); //interact with the parameter speed in the animator (linked to the link between idle and run)
    }

    private void inputHandler() //handle all of our input
    {
        if (Input.GetKeyDown(KeyCode.Space)) //NOT FINISHED
        {
            jump = true; //NOT FINISHED
        }
    }

    private void flipPlayer(float horizontal) //says where the player is facing(if it's right or left)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 playerScale = transform.localScale; //takes player's local scale (click on player and in the inspector it's in transform)

            playerScale.x *= -1; //change value of the calculated scale on the line up
            transform.localScale = playerScale; //change player's scale value
        }
    }

    private void resetTriggerValues() //NOT FINISHED
    {
        jump = false;
    }
}
