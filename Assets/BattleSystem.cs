using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, SHORTPANTSTURN, LONGPANTSTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject shortPantsPrefab;
    public GameObject longPantsPrefab;
    public GameObject enemyPrefab;

    public Transform shortPantsPlace;
    public Transform longPantsPlace;
    public Transform bossPlace;

    Unit shortPantsUnit;
    Unit longPantsUnit;
    Unit bossUnit;

    public Text dialogueText;
    public Image dialoguePane;

    public BattleHud shortPantsHud;
    public BattleHud longPantsHud;
    public BattleHud enemyHud;

    public BattleState state;
    private Queue<BattleState> turn = new Queue<BattleState>();

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        
    }

    IEnumerator SetupBattle()
    {
        GameObject shortPantsGO = Instantiate(shortPantsPrefab, shortPantsPlace);
        shortPantsUnit = shortPantsGO.GetComponent<Unit>();

        GameObject longPantsGO = Instantiate(longPantsPrefab, longPantsPlace);
        longPantsUnit = longPantsGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, bossPlace);
        bossUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = " The " + bossUnit.unitName + " approaches";

        yield return new WaitForSeconds(2f);

        dialogueText.text = "Can I go home ?";

        yield return new WaitForSeconds(2f);

        dialoguePane.enabled = false;
        dialogueText.enabled = false;

        shortPantsHud.setHUD(shortPantsUnit);
        longPantsHud.setHUD(longPantsUnit);
        enemyHud.setHUD(bossUnit);

        TurnScheduller();

        //yield return new WaitForSeconds(2f);

        changeTurns();

        yield return new WaitForSeconds(2f);

        ExcecuteTurn();
       
    }

    void ExcecuteTurn()
    {
        if (state == BattleState.SHORTPANTSTURN)
            StartCoroutine(ShortPantsTurn());
        else if (state == BattleState.LONGPANTSTURN)
            StartCoroutine(LongPantsTurn());
        else
            StartCoroutine(EnemyTurn());
    }

    IEnumerator ShortPantsAttack()
    {
        //Damage the boss.
        bool isDead= bossUnit.TakeDamage(shortPantsUnit.damage);

        enemyHud.SetHP(bossUnit.currentLife);
        dialoguePane.enabled = true;
        dialogueText.enabled = true;
        dialogueText.text = "You deal " + shortPantsUnit.damage + " of damage";


        yield return new WaitForSeconds(2f);

        dialoguePane.enabled = false;
        dialogueText.enabled = false;

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {

            changeTurns();

            ExcecuteTurn();
        }
    }

    IEnumerator LongPantsAttack()
    {
        //Damage the boss.
        bool isDead = bossUnit.TakeDamage(longPantsUnit.damage);

        enemyHud.SetHP(bossUnit.currentLife);
        dialoguePane.enabled = true;
        dialogueText.enabled = true;
        dialogueText.text = "You deal " + shortPantsUnit.damage + " of damage";


        yield return new WaitForSeconds(2f);

        dialoguePane.enabled = false;
        dialogueText.enabled = false;

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {

            changeTurns();

            ExcecuteTurn();
        }
    }

    void EndBattle()
    {
        dialoguePane.enabled = true;
        dialogueText.enabled = true;
        if (state == BattleState.WON)
            dialogueText.text = "You are an excelent worker short pants and now you can go home";
        else if (state == BattleState.LOST)
            dialogueText.text = "You can not even do this right? You are FIRED!";
    }

    public void OnAttackButton()
    {
        if (state == BattleState.ENEMYTURN)
            return;
        if (state == BattleState.SHORTPANTSTURN)
            StartCoroutine(ShortPantsAttack());
        if (state == BattleState.LONGPANTSTURN)
            StartCoroutine(LongPantsAttack());
    }

    void changeTurns()
    {
        state = turn.Dequeue();
        turn.Enqueue(state);
    }

    void TurnScheduller()
    {
        if (shortPantsUnit.speed >= longPantsUnit.speed && shortPantsUnit.speed >= bossUnit.speed)
        {
            turn.Enqueue(BattleState.SHORTPANTSTURN);
            if (longPantsUnit.speed >= bossUnit.speed)
            {
                turn.Enqueue(BattleState.LONGPANTSTURN);
                turn.Enqueue(BattleState.ENEMYTURN);
            }
            else
            {
                turn.Enqueue(BattleState.ENEMYTURN);
                turn.Enqueue(BattleState.LONGPANTSTURN);
            }
        }
        else if (longPantsUnit.speed >= shortPantsUnit.speed && longPantsUnit.speed >= bossUnit.speed)
        {
            turn.Enqueue(BattleState.LONGPANTSTURN);
            if (shortPantsUnit.speed >= bossUnit.speed)
            {
                turn.Enqueue(BattleState.SHORTPANTSTURN);
                turn.Enqueue(BattleState.ENEMYTURN);
            }
            else
            {
                turn.Enqueue(BattleState.ENEMYTURN);
                turn.Enqueue(BattleState.SHORTPANTSTURN);
            }
        }
        else
        {
            turn.Enqueue(BattleState.ENEMYTURN);
            if (shortPantsUnit.speed >= longPantsUnit.speed)
            {
                turn.Enqueue(BattleState.SHORTPANTSTURN);
                turn.Enqueue(BattleState.LONGPANTSTURN);
            }
            else
            {
                turn.Enqueue(BattleState.LONGPANTSTURN);
                turn.Enqueue(BattleState.SHORTPANTSTURN);
            }
        }
    }

    IEnumerator ShortPantsTurn()
    {
        dialoguePane.enabled = true;
        dialogueText.enabled = true;
        dialogueText.text = "Choose an action";
        yield return new WaitForSeconds(2f);
        dialoguePane.enabled = false;
        dialogueText.enabled = false;

    }

    IEnumerator LongPantsTurn() {

        dialoguePane.enabled = true;
        dialogueText.enabled = true;
        dialogueText.text = "Choose an action";
        yield return new WaitForSeconds(2f);
        dialoguePane.enabled = false;
        dialogueText.enabled = false;
    }

    IEnumerator EnemyTurn()
    {
        //IA, dont know who to target and how it attacks!
        dialoguePane.enabled = true;
        dialogueText.enabled = true;
        dialogueText.text = "That thing is attacking!!!";
        yield return new WaitForSeconds(1f);
        dialoguePane.enabled = false;
        dialogueText.enabled = false;

        bool isDead = shortPantsUnit.TakeDamage(bossUnit.damage);

        shortPantsHud.SetHP(shortPantsUnit.currentLife);
        yield return new WaitForSeconds(1f);
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            changeTurns();
            ExcecuteTurn();
        }


    }

}
