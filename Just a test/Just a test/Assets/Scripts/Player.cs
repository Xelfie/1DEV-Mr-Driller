using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    [SerializeField] private float movementSpeed;
    private bool facingRight;

    [SerializeField] private Transform[] groundPoints; //will basically indicate if the player is on the ground or not based on 3 "groundPoints" that are empty child objects of player
    [SerializeField] private float groundRadius; //to indicate how close  player needs to be to ground
    [SerializeField] private LayerMask thisIsGround; //this is to see what is ground and what isn't ground
    private bool isGrounded; //see in fixedUptade
    private bool jump;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool airControl; //if i want the player not being able to move in the air i can hehe

    private bool drillSide;
    private bool drillDown;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        facingRight = false;
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

        isGrounded = playerIsGrounded();

        movements(horizontal); //handles all of the player movements

        flipPlayer(horizontal); //says where the player is facing(if it's right or left)

        drill(); // handles drilling actions

        handleLayers(); // allows us to switch between layers in the Animator

        resetValues(); //reset trigger value of trigger parameter in animator
    }

    private void movements(float horizontal) //handles all of the player movements
    {
        if ((isGrounded || airControl) && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Drilling"))
        {
            playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, playerRigidbody.velocity.y); //vector of x value = -1 and y = y
        }

        if (isGrounded && jump) //if he is grounded and he pressed space bar:
        {
            isGrounded = false; //he will not be grounded anymore
            playerRigidbody.AddForce(new Vector2(0, jumpForce)); //will JUMP
            playerAnimator.SetTrigger("jump"); //trigger the jump parameter
        }

        playerAnimator.SetFloat("speed",Mathf.Abs(horizontal)); //interact with the parameter speed in the animator (linked to the link between idle and run)
    }

    private void drill() // handles drilling actions
    {
        if (drillSide) // if we want to drill to the left or right
        {
            playerAnimator.SetTrigger("drillSide"); // trigger the drillSide parameter in the Animator
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y); // set player's velocity to 0 so that he stops moving as soon as he starts drilling
        }

        if (drillDown) // if we want to drill down
        {
            playerAnimator.SetTrigger("drillDown"); // trigger the drillDown parameter in the Animator
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y); // set player's velocity to 0 so that he stops moving as soon as he starts drilling
        }
    }

    private void inputHandler() //handle all of our input
    {
        if (Input.GetKeyDown(KeyCode.Space)) //if space bar is pressed...
        {
            jump = true; 
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) //if Left Shift is pressed...
        {
            drillSide = true;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) //if the S key or the down arrow key is pressed...
        {
            drillDown = true;
        }
    }

    private void flipPlayer(float horizontal) //says where the player is facing (if it's right or left)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 playerScale = transform.localScale; //takes player's local scale (click on player and in the inspector it's in transform)

            playerScale.x *= -1; //change value of the calculated scale on the line up
            transform.localScale = playerScale; //change player's scale value
        }
    }

    private void resetValues() //used to reset values used in other functions
    {
        jump = false;
        drillSide = false;
        drillDown = false;
    }

    private bool playerIsGrounded()
    {
        if (playerRigidbody.velocity.y <= 0) //if player is falling
        {
            foreach (Transform point in groundPoints) //will see for all points in the groundPoints array
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, thisIsGround); //the colliders array will contain all the collider that these ground points are colliding with

                for (int i = 0; i < colliders.Length; i++) //will see for all colliders
                {
                    if (colliders[i].gameObject != gameObject) //if CURRENT collider we are looking at is different from player
                    {
                        playerAnimator.ResetTrigger("jump"); //reset the jump trigger parameter in the Animator
                        playerAnimator.SetBool("landing", false); //sets the landing parameter to false in the Animator
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void handleLayers() //this function allows us to switch between layers in the Animator
    {
        if (!isGrounded)
        {
            playerAnimator.SetLayerWeight(1, 1); //set the weight to 1 on the AirLayer (Animator switches to AirLayer)
        }
        else
        {
            playerAnimator.SetLayerWeight(1, 0); //set the weight to 0 on the AirLayer (Animator goes back to GroundLayer)
        }

        // Note: GroundLayer handles all animations for when the player is on the ground, AirLayer handles animations for when player is in the air
    }

}
