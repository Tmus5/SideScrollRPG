using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    Animator anim;
    private Rigidbody2D rigidbody;

    // Use this for initialization
    //void Start () {
    //    anim = GetComponent<Animator>();
    //    rigidbody = GetComponent<Rigidbody2D>();

    //}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    protected override void Attack()
    {

    }

}
