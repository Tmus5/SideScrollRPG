using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 0;//Don't touch this
    public float maxSpeed = 10f;
    public float acceleration = 1;//How fast will object reach a maximum speed
    public float  deceleration = 1;//How fast will object reach a speed of 0


    bool facingRight = true;
    Animator anim;
    float xOld;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private Rigidbody2D rigidbody;
    private BoardManager boardScript;

    public float jumpForce = 700;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        boardScript = GetComponent<BoardManager>();
        xOld = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //float move = Input.GetAxis("Horizontal");
        //float move = 1;
        //Debug.Log(move);

        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(speed));

        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);


        //Debug.Log(rigidbody.velocity);


        // flipping character used for manually changing sprite look direction when controlling sprite manually
        //if (speed > 0 && !facingRight)
        //    Flip();
        //else if (speed < 0 && facingRight)
        //    Flip();


        // if store old x if new x > old x + 1 do code

        //if (transform.position.x > xOld + .9)
        //{

        // stop creating if collided with enemy / object
            boardScript.CreateOneFloor();
            xOld = transform.position.x;


       


        //}

    }

    private void Update()
    {
        // change collision addition from + 15 to more approrpiate based on distance away, changed to that to show on initial start
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.position.x + 15, 1));

        if (hit.collider != null && speed != 0)
        {
            DecreaseSpeed();
        }
        else if (hit.collider == null)
        {
            IncreaseSpeed();
        }


        if (grounded && Input.GetButton("Jump"))
        {
            anim.SetBool("Ground", false);
            rigidbody.AddForce(new Vector2(0, jumpForce));
        }



        if (Input.GetMouseButtonDown(0))
        {
            boardScript.CreateFloorOnClick();
        }


    }

    void DecreaseSpeed()
    {
        if (speed > 0)
            speed = speed - acceleration * Time.deltaTime;
        else
            speed = 0;
    }

    void IncreaseSpeed()
    {
        if (speed < maxSpeed)
            speed = speed + acceleration * Time.deltaTime;
        //else



        //Debug.Log(speed);

        //else if (speed > - maxSpeed)
        //    speed = speed + acceleration * Time.deltaTime;
        //else
        //{
        //    if (speed > deceleration * Time.deltaTime) 
        // speed = speed - deceleration * Time.deltaTime;
        // else if (speed < -deceleration * Time.deltaTime) 
        //     speed = speed + deceleration * Time.deltaTime;
        // else speed = 0;
        //}
        //transform.position.x = transform.position.x + speed * Time.deltaTime;
    }

    void Flip() {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //transform.LookAt(0, 0, rigidbody.transform.position.z);



        //transform.position = new Vector3(transform.position.x, 1, 1);
    }
}
