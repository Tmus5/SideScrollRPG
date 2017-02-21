using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    private Animator anim;
    private Rigidbody2D rigidbody;
    public int enemyDamageBase;
    public int enemyHealth = 1000;
    public int enemyExperienceGain;
    private Player playerScript;
    private Coroutine coroutine;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //playerScript.isEnemyDestroyed = false;
        //playerScript.isEnemyAlive = true;


    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (enemyHealth <= 0) {
            enemyHealth = 0;
            anim.SetInteger("Health", enemyHealth);


            //yield new WaitForAnimation(anim);


            //yield WaitForAnimation(animation.PlayQueued("Intro"));
      
            if (coroutine == null)
                coroutine = StartCoroutine(Death());



        }      
    }

    IEnumerator Death()
    {
        var animationState = anim.GetCurrentAnimatorStateInfo(0);
        var animationClips = anim.GetCurrentAnimatorClipInfo(0);
        var animationClip = animationClips[0].clip;
        var animationTime = animationClip.length * animationState.normalizedTime;

        yield return new WaitForSeconds(animationTime);

        Destroy(gameObject);
        playerScript.isEnemyAlive = false;
        playerScript.isEnemyDestroyed = true;
        coroutine = null;
    }

}
