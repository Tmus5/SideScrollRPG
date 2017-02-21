using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    private Animator anim;
    private Rigidbody2D rigidbody;
    private Player playerScript;
    private  GameManager gameScript;
    private Coroutine coroutine;


    public Dictionary<string, Stats> enemyStats = new Dictionary<string, Stats>()
    {
        { "BronzeKnight", new Stats { AttackSpeed = 10, Damage = 10, Experience = 100, Health = 1000 } },
        { "SilverKnight", new Stats { AttackSpeed = 10, Damage = 15, Experience = 150, Health = 1200 } }
    };

    public Stats stats;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();


        string enemyName = gameObject.transform.name;
        enemyName = enemyName.Contains("(") == true ? enemyName.Substring(0, enemyName.IndexOf('(')) : enemyName;

        stats = enemyStats[enemyName];

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (stats.Health <= 0) {
            stats.Health = 0;
            anim.SetInteger("Health", stats.Health);


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
