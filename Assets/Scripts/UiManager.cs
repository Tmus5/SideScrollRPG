using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
	
	public int level = 1;
    public Text levelText;
    public Text playerHpText;
    public Text enemyHpText;
	public Text playerXP;

    private Player playerScript;
    private Enemy enemyScript;

    // Use this for initialization
    void Start () {
        //healthPoints = GameManager.instance;
        levelText.text = string.Format("Level: {0}", level);
        playerHpText.text = string.Format("Level: {0}", level);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //enemyScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void FixedUpdate()
    {
        playerHpText.text = string.Format("HP: {0}", playerScript.playerStats.Health);
		playerXP.text = string.Format ("Experience: {0}", playerScript.playerStats.Experience);

        if (playerScript.isEnemyAlive)
            enemyHpText.text = string.Format("Enemy: {0}", playerScript.currentEnemy.enemyHealth);

        if (playerScript.currentEnemy.enemyHealth <= 0 && playerScript.speed == 0)
            enemyHpText.text = string.Format("Enemy: {0}", "0");


        if (playerScript.isEnemyDestroyed && playerScript.currentEnemy.enemyHealth <= 0) {
            level++;
            playerScript.level = level;
            levelText.text = string.Format("Level: {0}", level);
            enemyHpText.text = string.Format("{0}", "");
            playerScript.isEnemyDestroyed = false;
        }
    }
}
