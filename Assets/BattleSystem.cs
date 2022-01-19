using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public BattleHud shortPantsHud;
    public BattleHud longPantsHud;
    public BattleHud enemyHud;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
        
    }

    void SetupBattle()
    {
        GameObject shortPantsGO = Instantiate(shortPantsPrefab, shortPantsPlace);
        shortPantsUnit = shortPantsGO.GetComponent<Unit>();

        GameObject longPantsGO = Instantiate(longPantsPrefab, longPantsPlace);
        longPantsUnit = longPantsGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, bossPlace);
        bossUnit = enemyGO.GetComponent<Unit>();

        shortPantsHud.setHUD(shortPantsUnit);
        longPantsHud.setHUD(longPantsUnit);
        enemyHud.setHUD(bossUnit);
    }

}
