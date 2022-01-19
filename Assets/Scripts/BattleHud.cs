using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{

    public Slider hpSlider;
    public Slider mpSlider;

    public void setHUD(Unit unit)
    {
        hpSlider.maxValue = unit.maxLife;
        hpSlider.value = unit.currentLife;

        mpSlider.maxValue = unit.maxMana;
        mpSlider.value = unit.currentMana;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetMP(int mp)
    {
        mpSlider.value = mp;
    }
}
