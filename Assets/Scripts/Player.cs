using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxSpeed = 10f;
    bool facingRight = true;
    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private Rigidbody2D rigidbody;
    public Camera camera;

    public float jumpForce = 700;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        float move = Input.GetAxis("Horizontal");

        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));

        rigidbody.velocity = new Vector2(move * maxSpeed, rigidbody.velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();            
	}

    private void Update()
    {
        if (grounded && Input.GetButton("Jump")) {
            anim.SetBool("Ground", false);
            rigidbody.AddForce(new Vector2(0, jumpForce));
        }
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
