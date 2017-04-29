using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public Stats playerStats = new Stats();


    public float speed = 0;//Don't touch this
    public float maxSpeed = 10f;
    public float acceleration = 1;//How fast will object reach a maximum speed
    public float deceleration = 1;//How fast will object reach a speed of 0


    public Slider playerHp;
    public Slider enemyHp;

    public Text levelText;
    public Text playerHpText;
    public Text playerXP;

    public Text enemyHpText;

    //public Stats stats = new Stats();

    private int enemyDamageScaled = 1;

    // TODO setup proper scaling for xp and damage
    private int enemyXpScaled = 1;

    public int level = 1;
    public bool isEnemyAlive = false;

    bool facingRight = true;
    public Animator anim;

    public GameObject obj;

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
        uiScript = GetComponent<UiManager>();
        currentEnemy = new Enemy();
        //xOld = transform.position.x;
        playerStats.Damage = 50;
        playerStats.Health = 1000;
        playerStats.Experience = 0;
        playerStats.MaxHealth = 1000;

        playerHpText.text = string.Format("{0} / {1}", playerStats.Health, playerStats.MaxHealth);
        playerXP.text = string.Format("{0}", 0);

        playerHp.maxValue = playerStats.Health;
        playerHp.minValue = 0;
        playerHp.value = playerStats.Health;
        isEnemyAlive = false;
        enemyHp.gameObject.SetActive(false);
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
                enemyHpText.text = string.Format("{0} / {1}", currentEnemy.stats.Health, currentEnemy.stats.MaxHealth);

                enemyHp.maxValue = currentEnemy.stats.Health;
                enemyHp.minValue = 0;
                enemyHp.value = currentEnemy.stats.Health;
                enemyHp.gameObject.SetActive(true);
            }

            DecreaseSpeed();
        }
        else if (hit.collider == null)
        {
            enemyHp.gameObject.SetActive(false);
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

        //if (grounded && Input.GetButton("Jump"))
        //{
        //    anim.SetBool("Ground", false);
        //    rigidbody.AddForce(new Vector2(0, jumpForce));
        //}   
    }

    protected IEnumerator Attack(Enemy enemy)
    {
        playerStats.Health = playerStats.Health - enemy.stats.Damage + enemyDamageScaled;
        enemy.stats.Health = enemy.stats.Health - playerStats.Damage;


        if (enemy.stats.Health <= 0)
        {
            // Heal animation to not affect character animation, create a second animation to overlay the current one
            enemyDamageScaled = (int)Mathf.Log(level, 2f);
            playerStats.Experience += enemy.stats.Experience + (2 * level);
            isEnemyAlive = false;
            anim.SetBool("isAttacking", false);
        }
        UpdateUiText();

        yield return new WaitForSeconds(0.5f);
        coroutine = null;
    }

    public void UpdateUiText() {
        playerHpText.text = string.Format("{0} / {1}", playerStats.Health, playerStats.MaxHealth);
        enemyHpText.text = string.Format("{0} / {1}", currentEnemy.stats.Health, currentEnemy.stats.MaxHealth);

        playerHp.value = playerStats.Health;
        enemyHp.value = currentEnemy.stats.Health;

        if (currentEnemy.stats.Health <= 0) {
            playerXP.text = string.Format("Experience: {0}", playerStats.Experience);
            levelText.text = string.Format("Level: {0}", level);
        }
    }

   

    IEnumerator DisableHeal()
    {
        if ((playerStats.Health + 1000) > playerStats.MaxHealth)
            playerStats.Health = playerStats.MaxHealth;
        else
            playerStats.Health += 1000;

        playerHpText.text = string.Format("{0} / {1}", playerStats.Health, playerStats.MaxHealth);
        playerHp.value = playerStats.Health;

        anim.SetBool("Heal", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Heal", false);
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
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
