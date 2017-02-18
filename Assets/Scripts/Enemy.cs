using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    Animator anim;
    private Rigidbody2D rigidbody;
    public int enemyDamageBase;
    public int enemyHealth = 1000;
    private Player playerScript;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    protected override void Attack()
    {

    }

    private void FixedUpdate()
    {
        if (enemyHealth <= 0) {
            enemyHealth = 0;
            Destroy(gameObject);
            playerScript.isEnemyVisible = false;

        }

    }

}
