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
        playerHpText.text = string.Format("{0}", playerScript.playerHp);
		playerXP.text = string.Format ("{0}", playerScript.playerXP);


        if (playerScript.isEnemyVisible) 
            enemyHpText.text = string.Format("{0}", playerScript.currentEnemy.enemyHealth);
                
        if (playerScript.isEnemyVisible && playerScript.currentEnemy.enemyHealth <= 0) {
            level++;
            playerScript.level = level;
            levelText.text = string.Format("Level: {0}", level);
            enemyHpText.text = string.Format("{0}", "");
        }
    }
}
