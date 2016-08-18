using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    public float speed = 5.0f;
    public float turnSpeed = 5.0f;
    public float gravity = 100f;
    public float jump = 30.0f;

    public float maxMovementX = 14f;
    public float maxMovementZ = 18f;

    public string jumpButton = "Jump_P1";
    public string horizontalButton = "Horizontal_P1";
    public string verticalButton = "Vertical_P1";

    private Vector3 moveDir = Vector3.zero;

    private GameObject[] players;

    private bool stopMovementX = false;
    private bool stopMovementZ = false;

    //private Rigidbody rigidBody;
    CharacterController controller;

    // Use this for initialization
    void Start()
    {
        //rigidBody = gameObject.GetComponent<Rigidbody>();
        controller = gameObject.GetComponent<CharacterController>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        checkMovement();
        if (controller.isGrounded)
        {
            if (stopMovementX == false && stopMovementZ == false)
            {
                moveDir = new Vector3(Input.GetAxis(horizontalButton), 0, Input.GetAxis(verticalButton));
            }
            //cant move in x direction
            else if(stopMovementX == true && stopMovementZ == false)
            {
                moveDir = new Vector3(0, 0, Input.GetAxis(verticalButton));
            }
            //cant move in z direction
            else if(stopMovementX == false && stopMovementZ == true)
            {
                moveDir = new Vector3(Input.GetAxis(horizontalButton), 0, 0);
            }
            //cant move in either direction
            else if(stopMovementX == true && stopMovementZ == true)
            {
                moveDir = new Vector3(0, 0, 0);
            }
            //moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
            if (Input.GetButton(jumpButton))
                moveDir.y = jump;

        }
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void LateUpdate()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void FixedUpdate()
    {
    }

    void checkMovement()
    {
        float x = -1;
        float z = -1;

        GameObject otherPlayerX = null;
        GameObject otherPlayerZ = null;

        //Loop through players and set x to greatest x distance, and y to greatest y distance between current player and any other player
        for (int i = 0; i < players.Length; i++)
        {
            if (Mathf.Abs(transform.position.x - players[i].transform.position.x) > x) // if the distance is greater than current
            {
                x = Mathf.Abs(transform.position.x - players[i].transform.position.x); // update x to the new greatest distance
                otherPlayerX = players[i]; // set this gamobject to the other player that this player has the greatest distance with
            }

            if (Mathf.Abs(transform.position.z - players[i].transform.position.z) > z)
            {
                z = Mathf.Abs(transform.position.z - players[i].transform.position.z);
                otherPlayerZ = players[i];
            }
        }

        Debug.Log(gameObject.name + " X -> " + otherPlayerX.gameObject.name);
        Debug.Log(gameObject.name + " Z -> " + otherPlayerZ.gameObject.name);
        Debug.Log(" ");

        if (x >= maxMovementX) // if the greatest distance if greater than what is allowed, stop movement
        {
            stopMovementX = true;

            float playerXInput = Input.GetAxis(horizontalButton) * speed; // sets this variable to the current input the player is giving for horizontal movement

            if (Mathf.Abs((transform.position.x + playerXInput) - otherPlayerX.transform.position.x) < maxMovementX) // if the input the player is giving plus his current x position is less than max movement
            {
                stopMovementX = false; // the player is allowed to move again because they will be less than the max movement allowed
            }
        }
        else // if the greatest distance is still lower than what is allowed the player can move
        {
            stopMovementX = false;
        }

        if (z >= maxMovementZ)
        {
            stopMovementZ = true;

            float playerZInput = Input.GetAxis(verticalButton) * speed; //Players input for Z axis

            if (Mathf.Abs((transform.position.z + playerZInput) - otherPlayerZ.transform.position.z) < maxMovementZ)
            {
                stopMovementZ = false;
            }
        }
        else
        {
            stopMovementZ = false;
        }
    }
}
