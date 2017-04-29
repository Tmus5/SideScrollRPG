using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spells : MonoBehaviour {

    private Player player;
    public Button healButton;

    enum SpellTypes
    {
        Heal,
        Special
    }

    enum CoolDowns
    {
        Heal = 10,
        Special = 30
    }

    private SpellTypes? spell;
    private CoolDowns? coolDown;
    private int countDownValue;

    private void Start()
    {
        //healButton = healButton;
    }

    public void FixedUpdate()
    {

    }

    public void HealInit()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        spell = SpellTypes.Heal;
        healButton.enabled = false;
        healButton.GetComponentInChildren<Text>().text = string.Format("{0}", (int)CoolDowns.Heal);
        countDownValue = (int)CoolDowns.Heal;

        if ((player.playerStats.Health + 1000) > player.playerStats.MaxHealth)
            player.playerStats.Health = player.playerStats.MaxHealth;
        else
            player.playerStats.Health += 1000;

        player.playerHpText.text = string.Format("{0} / {1}", player.playerStats.Health, player.playerStats.MaxHealth);
        player.playerHp.value = player.playerStats.Health;

        player.anim.SetBool("Heal", true);
        yield return new WaitForSeconds(1);
        player.anim.SetBool("Heal", false);

        yield return InitSpellTimeout();


    }

    private IEnumerator InitSpellTimeout() {


        while (countDownValue != 1)
            yield return StartCoroutine(Timeout());

        spell = null;
        healButton.enabled = true;
        healButton.GetComponentInChildren<Text>().text = string.Empty;
    }

    IEnumerator Timeout() {
        countDownValue--;
        healButton.GetComponentInChildren<Text>().text = countDownValue.ToString();
        yield return new WaitForSeconds(1);        
    }

}
