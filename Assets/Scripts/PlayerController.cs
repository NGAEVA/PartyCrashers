using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool stopCamera = false;

    public float speed = 5.0f;
    public float turnSpeed = 5.0f;
    public float gravity = 100f;
    public float jump = 30.0f;

    public string jumpButton = "Jump_P1";
    public string horizontalButton = "Horizontal_P1";
    public string verticalButton = "Vertical_P1";

    private Vector3 moveDir = Vector3.zero;

    //private Rigidbody rigidBody;
    CharacterController controller;

    // Use this for initialization
    void Start()
    {
        //rigidBody = gameObject.GetComponent<Rigidbody>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis(horizontalButton), 0, Input.GetAxis(verticalButton));
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

        if(Camera.main.WorldToViewportPoint(transform.position).x < .05 || Camera.main.WorldToViewportPoint(transform.position).x > .95
            || Camera.main.WorldToViewportPoint(transform.position).y < .05 || Camera.main.WorldToViewportPoint(transform.position).y > .95)
        {
            stopCamera = true;
        }
        else
        {
            stopCamera = false;
        }
    }

    void FixedUpdate()
    {
    }
}
