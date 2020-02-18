using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterScript : MonoBehaviour {

    public static int damage;
    public int score;
    public static bool dontMove;
    public bool detectImmunity, isZoomedOut;

    private Quaternion fixedRotate;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private bool timeSlow;

    private SpriteRenderer playerRend;
    private Rigidbody2D playerRigid;
    private Vector3 touchedPosition;
    private GameObject body;
    private Vector2 firstPoint;
    private Vector2 secondPoint;
    private Vector2 moveInput;
    private bool immune, started;
    private float immuneTimer;
    private int heldTime = 0;
   

    void Start ()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();
        playerRend = player.GetComponentInChildren<SpriteRenderer>();
        body = GameObject.FindGameObjectWithTag("Body");
        score = 0;
        fixedRotate = body.transform.rotation;
        damage = 0;
        immuneTimer = 0;
    }

    void CheckIfStart()
    {
        if (!started)
        {
            playerRigid.gravityScale = 0;
            playerRigid.velocity = Vector2.zero;
        }
        else
            playerRigid.gravityScale = 1;
    }
   
    public void Update()
    {
        Movement();
        DamageProtection();
        CheckIfStart();
    }

    void Movement()
    {
        if (!GameManager.isPaused)
        {
            if (Input.GetMouseButton(0)) // Player touches screen
            {
                started = true; //activate gravity and start moving

                if(timeSlow)
                    heldTime++; // timer for activating slow motion effect

                if (heldTime < 10) // if the tap is less than ten ticks (e.g. the player isn't holding the tap down)
                {
                    touchedPosition = Input.mousePosition;
                    PointToTouch(); //Rotate the player towards the touch position
                    playerRigid.velocity = Vector2.zero; //Zero the velocity so that the player doesn't have to fight their own force when moving
                    playerRigid.AddForce(-transform.up * 1000); //Add the force to the player character
                }
                else if (heldTime < 60)
                    Time.timeScale = 0.5f; //if the player is holding down touch then reduce time until 60 ticks have passed (Time slow mechanic, not to do with movement)
                else
                    Time.timeScale = 1; //Set time scale to original
            }
            else
            {
                heldTime = 0; //if the player is not tapping or holding, reset values
                Time.timeScale = 1;
            }

            playerRigid.velocity = Vector2.ClampMagnitude(playerRigid.velocity, 20f); //COMMENT OUT THIS LINE FOR ORIGINAL VELOCITY

            body.transform.rotation = fixedRotate; //Rotate the body of the model (Purely for visual aethetics)
        }
    }

    void PointToTouch()
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touchedPosition); //Find where the player touched in the world relative to screen space
        Vector2 lookDirection = (touchPosition - (Vector2)transform.position).normalized; // Make the look direction the opposite from where the player has touched

        transform.up = lookDirection; // set Transform.up to be in our new Direction
    }

    void DamageProtection()
    {
        if (detectImmunity == true)
        {
            immuneTimer += Time.deltaTime;

            if (immuneTimer <= 1f)
            {
                playerRend.color = Color.red;
                immune = true;

            }
            else
            {
                playerRend.color = Color.white;
                immune = false;
                detectImmunity = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Collide")
        {
            if (detectImmunity == false)
            {
                immuneTimer = 0;
                detectImmunity = true;
            }

            if (immune == false)
            {
                damage++;
            }
        }
    }


}
