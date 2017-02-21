using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{

    //public class Stats
    //{
    //    public int playerHp;
    //    public int playerDmg;

    //    public Stats()
    //    {

    //    }
    //}

    /// <summary>
    /// create class for player stats
    /// </summary>
    /// 
    public Stats playerStats = new Stats();


    public float speed = 0;//Don't touch this
    public float maxSpeed = 10f;
    public float acceleration = 1;//How fast will object reach a maximum speed
    public float deceleration = 1;//How fast will object reach a speed of 0


    //public Stats stats = new Stats();

    private int enemyDamageScaled = 1;

    // TODO setup proper scaling for xp and damage
    private int enemyXpScaled = 1;

    public int level = 1;
    public bool isEnemyAlive = false;

    bool facingRight = true;
    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private new Rigidbody2D rigidbody;

    private BoardManager boardScript;
    private UiManager uiScript;

    public float jumpForce = 700;

    public Enemy currentEnemy;
    private Coroutine coroutine;
    internal bool isEnemyDestroyed;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        //boardScript = GetComponent<BoardManager>();
        //uiScript = GetComponent<UiManager>();
        currentEnemy = new Enemy();
        //xOld = transform.position.x;
        playerStats.Damage = 50;
        playerStats.Health = 1000;
        playerStats.Experience = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

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
        //boardScript.CreateOneFloor();
        //xOld = transform.position.x;


        //}

    }

    private void Update()
    {
        // change collision addition from + 15 to more approrpiate based on distance away, changed to that to show on initial start
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 3f);
        //Debug.DrawRay(transform.position, transform.right, Color.red);


        if (hit.collider != null && speed != 0)
        {
            if (!isEnemyAlive)
            {
                currentEnemy = hit.collider.gameObject.GetComponent<Enemy>();
                isEnemyDestroyed = false;
                isEnemyAlive = true;
            }


            DecreaseSpeed();
        }
        else if (hit.collider == null)
        {
            IncreaseSpeed();
        }

        if (hit.collider != null && speed == 0)
        {
            currentEnemy = hit.collider.gameObject.GetComponent<Enemy>();

            if (coroutine == null && isEnemyAlive)
            {
                anim.SetBool("isAttacking", true);
                coroutine = StartCoroutine(Attack(currentEnemy));
            }
        }


        if (grounded && Input.GetButton("Jump"))
        {
            anim.SetBool("Ground", false);
            rigidbody.AddForce(new Vector2(0, jumpForce));
        }



    }

    protected IEnumerator Attack(Enemy enemy)
    {
        playerStats.Health = playerStats.Health - enemy.stats.Damage + enemyDamageScaled;
        enemy.stats.Health = enemy.stats.Health - playerStats.Damage;

        if (enemy.stats.Health <= 0)
        {
            // Heal animation to not affect character animation, create a second animation to overlay the current one
            // TODO use this to access any animations associated with the enemy, for example a death animation
            enemyDamageScaled = (int)Mathf.Log(level, 2f);
            playerStats.Experience += enemy.stats.Experience + (2 * level);
            isEnemyAlive = false;
            anim.SetBool("isAttacking", false);

        }

        yield return new WaitForSeconds(0.5f);
        coroutine = null;

    }

    public void HealInit()
    {
        StartCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        playerStats.Health += 1000;
        anim.SetBool("Heal", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Heal", false);
    }



    private void OnMouseDown()
    {
        // not working for some reason
    }




    void DecreaseSpeed()
    {
        if (speed > 0)
            speed = speed - deceleration * Time.deltaTime;
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

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //transform.LookAt(0, 0, rigidbody.transform.position.z);



        //transform.position = new Vector3(transform.position.x, 1, 1);
    }
}
